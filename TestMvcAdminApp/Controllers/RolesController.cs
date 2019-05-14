using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using TestMvcAdminApp;
using TestMvcAdminApp.Models;
using TestMvcAdminApp.Repositories;

namespace MvcCoreAdminApp.Controllers {

    [Authorize]
    public class RolesController : Controller {

        private readonly IServiceProvider _serviceProvider;

        public RolesController(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public IActionResult Index() {
            var model = AdminRepository.GetAllRoles();

            var allRolesWithPermissions = new List<RoleWithPermissions>();

            allRolesWithPermissions = model.GroupBy(x => new { x.ID, x.Name, x.Description }).Select(y =>
              new RoleWithPermissions {
                  ID = y.Key.ID,
                  Name = y.Key.Name,
                  Description = y.Key.Description,
                  Permissions = y.Select(z => new Permission { ID = z.PermissionID, Name = z.PermissionName, Description = z.PermissionDescription }).ToList()
              }).ToList();

            return View(allRolesWithPermissions);
        }

        [ClaimRequirement]
        [HttpGet, Route("Roles/CreateNew")]
        public IActionResult CreateRole() {
            return View();
        }

        [HttpPost, Route("Roles/CreateNew")]
        public async Task<IActionResult> CreateRole(Role model) {
            var roleManager = _serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            if (!ModelState.IsValid) {
                return View(model);
            }
            var roleID = AdminRepository.CreateRole(model);

            var applicationRole = new ApplicationRole(model.Name, roleID);
            var result = await roleManager.CreateAsync(applicationRole);

            return RedirectToAction("Index");
        }

        [ClaimRequirement]
        [HttpGet, Route("Roles/{RoleID}")]
        public IActionResult EditPermissionsOfRole(int roleID) {
            var permissionsList = AdminRepository.GetPermissionsByRoleID(roleID);
            var permissions = permissionsList.FirstOrDefault();
            return View(permissions);
        }

        [HttpPost, Route("Roles/{RoleID}")]
        public async Task<IActionResult> EditPermissionsOfRole(PermissionsForRoleDTO model) {
            var roleManager = _serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var rolePermissions = new List<AssignPermissionsToRole>();

            var permissionIDs = new List<string>();

            foreach (var permission in model.PermissionsWithIsAssigned) {
                if (permission.IsAssigned) {
                    permissionIDs.Add(permission.ID.ToString());
                }
            }

            var permissionIDsToString = string.Join(",", permissionIDs);

            var modelToList = new List<PermissionsForRoleDTO> {
                model
            };

            rolePermissions = modelToList.GroupBy(x => new { x.RoleID }).Select(y =>
            new AssignPermissionsToRole {
                RoleID = y.Key.RoleID,
                PermissionIDs = permissionIDsToString
            }).ToList();

            AdminRepository.AssignPermissionsToRole(rolePermissions);

            // Get all permissions affected by rights change
            var rightsHavingPermissions = AdminRepository.GetRightsHavingPermissions(permissionIDsToString);

            var rightIDsList = new List<string>();

            var applicationRoleItem = new ApplicationRole(model.RoleName, model.RoleID);

            foreach (var item in rightsHavingPermissions) {
                rightIDsList.Add(item.ID.ToString());
            }

            // Get all RightIDs
            var allRightIDs = AdminRepository.GetAllRightIDs();

            // Get Application Role
            var applicationRole = roleManager.FindByNameAsync(model.RoleName).Result;

            // modify identity table aspnetroleclaims
            foreach (var rightID in allRightIDs) {
                if (rightIDsList.Contains(rightID)) {
                    await roleManager.AddClaimAsync(applicationRole, new Claim(AdminRepository.GetRightNameByRightID(int.Parse(rightID)), "True"));
                } else {
                    await roleManager.RemoveClaimAsync(applicationRole, new Claim(AdminRepository.GetRightNameByRightID(int.Parse(rightID)), "True"));
                }

            }
            await HttpContext.RefreshLoginAsync();
            return RedirectToAction("Index");
        }
    }
}