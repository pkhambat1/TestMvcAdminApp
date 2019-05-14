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
    public class PermissionsController : Controller {

        private readonly IServiceProvider _serviceProvider;

        public PermissionsController(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        [ClaimRequirement]
        public IActionResult Index() {
            var model = AdminRepository.GetAllPermissions();

            var allPermissionsWithRights = new List<PermissionWithRights>();

            allPermissionsWithRights = model.GroupBy(x => new { x.ID, x.Name, x.Description }).Select(y =>
              new PermissionWithRights {
                  ID = y.Key.ID,
                  Name = y.Key.Name,
                  Description = y.Key.Description,
                  Rights = y.Select(z => new Right { ID = z.RightID, Name = z.RightName, Description = z.RightDescription }).ToList()
              }).ToList();

            return View(allPermissionsWithRights);
        }

        [ClaimRequirement]
        [HttpGet, Route("Permissions/CreateNew")]
        public IActionResult CreatePermission() {
            return View();
        }

        [HttpPost, Route("Permissions/CreateNew")]
        public IActionResult CreatePermission(Permission model) {
            if (!ModelState.IsValid) {
                return View(model);
            }
            AdminRepository.CreatePermission(model);
            return RedirectToAction("Index");
        }

        [ClaimRequirement]
        [HttpGet]
        [Route("Permissions/{PermissionID}")]
        public IActionResult EditRightsOfPermission(int permissionID) {
            var rightsList = AdminRepository.GetRightsByPermissionID(permissionID);
            var rights = rightsList.FirstOrDefault();
            return View(rights);
        }

        [HttpPost]
        [Route("Permissions/{PermissionID}")]
        public async Task<IActionResult> EditRightsOfPermission(RightsForPermissionDTO model) {
            var roleManager = _serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var permissionRights = new List<AssignRightsToPermission>();
            var permissionRightIDs = new List<string>();
            foreach (var right in model.RightsWithIsAssigned) {
                if (right.IsAssigned) {
                    permissionRightIDs.Add(right.ID.ToString());
                }
            }

            var modelToList = new List<RightsForPermissionDTO> {
                model
            };

            permissionRights = modelToList.GroupBy(x => new { x.PermissionID }).Select(y =>
            new AssignRightsToPermission {
                PermissionID = y.Key.PermissionID,
                RightIDs = string.Join(",", permissionRightIDs)
            }).ToList();
            AdminRepository.AssignRightsToPermission(permissionRights);

            // Get all roles affected by permissions change
            var rolesHavingPermission = AdminRepository.GetRolesHavingPermission(model.PermissionID);

            // Get all RightIDs
            var allRightIDs = AdminRepository.GetAllRightIDs();

            // Foreach role with Permission model.PermissionID
            foreach (var roleItem in rolesHavingPermission) {

                var assignRightsToRole = new AssignRightsToRole { RoleID = roleItem.ID, RightIDs = string.Join(",", permissionRightIDs) };
                var applicationRoleItem = new ApplicationRole(roleItem.Name, roleItem.ID);

                var listAssignRightsToRole = new List<AssignRightsToRole> {
                    assignRightsToRole
                };
                // Update RoleRights Table - Delete all values where Role ID is RoleID and Insert RoleRights model
                AdminRepository.AssignRightsToRole(listAssignRightsToRole);

                // Get Application Role
                var applicationRole = roleManager.FindByNameAsync(roleItem.Name).Result;

                // Modify Identity Table AspNetRoleClaims
                foreach (var rightID in allRightIDs) {
                    if (permissionRightIDs.Contains(rightID)) {
                        await roleManager.AddClaimAsync(applicationRole, new Claim(AdminRepository.GetRightNameByRightID(int.Parse(rightID)), "True"));
                    } else {
                        await roleManager.RemoveClaimAsync(applicationRole, new Claim(AdminRepository.GetRightNameByRightID(int.Parse(rightID)), "True"));
                    }
                }
            }
            await HttpContext.RefreshLoginAsync();
            return RedirectToAction("Index");
        }
    }
}