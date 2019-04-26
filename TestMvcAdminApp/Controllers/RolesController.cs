using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using TestMvcAdminApp.Models;
using TestMvcAdminApp.Repositories;

namespace MvcCoreAdminApp.Controllers {
    public class RolesController : Controller {

        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IServiceProvider _serviceProvider;

        public RolesController(RoleManager<ApplicationRole> roleManager, IServiceProvider serviceProvider) {
            _roleManager = roleManager;
            _serviceProvider = serviceProvider;
        }

        [Authorize]
        public IActionResult Index() {
            var model = AdminRepository.GetAllRoles();

            var allRolesWithPermissions = new List<RolesWithPermissions>();

            allRolesWithPermissions = model.GroupBy(x => new { x.ID, x.Name, x.Description }).Select(y =>
              new RolesWithPermissions {
                  ID = y.Key.ID,
                  Name = y.Key.Name,
                  Description = y.Key.Description,
                  Permissions = y.Select(z => new Permission { ID = z.PermissionID, Name = z.PermissionName, Description = z.PermissionDescription }).ToList()
              }).ToList();

            return View(allRolesWithPermissions);
        }

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

        #region Edit Permissions
        [HttpGet, Route("Roles/{RoleID}")]
        public IActionResult EditPermissionsOfRole(int roleID) {
            var permissionsList = AdminRepository.GetPermissionsByRoleID(roleID);
            var permissions = permissionsList.FirstOrDefault();
            return View(permissions);
        }

        [HttpPost, Route("Roles/{RoleID}")]
        public IActionResult EditPermissionsOfRole(PermissionsForRoleDTO model) {
            var rolePermissions = new List<RolePermissions>();

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
            new RolePermissions {
                RoleID = y.Key.RoleID,
                PermissionIDs = permissionIDsToString
            }).ToList();

            AdminRepository.AssignPermissionsToRole(rolePermissions);
            return RedirectToAction("Index");
        }

        #endregion
    }
}