using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestMvcAdminApp.Models;
using TestMvcAdminApp.Repositories;

namespace MvcCoreAdminApp.Controllers {
    [Authorize(Roles = "Admin")]
    public class PermissionsController : Controller {
        public IActionResult Index() {
            var model = AdminRepository.GetAllPermissions();

            var allPermissionsWithRights = new List<PermissionsWithRights>();

            allPermissionsWithRights = model.GroupBy(x => new { x.ID, x.Name, x.Description }).Select(y =>
              new PermissionsWithRights {
                  ID = y.Key.ID,
                  Name = y.Key.Name,
                  Description = y.Key.Description,
                  Rights = y.Select(z => new Right { ID = z.RightID, Name = z.RightName, Description = z.RightDescription }).ToList()
              }).ToList();

            return View(allPermissionsWithRights);
        }

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

        #region Edit Rights
        [HttpGet,
            Route("Permissions/{PermissionID}")]
        public IActionResult EditRightsOfPermission(int permissionID) {
            var rightsList = AdminRepository.GetRightsByPermissionID(permissionID);
            var rights = rightsList.FirstOrDefault();
            return View(rights);
        }

        [HttpPost,
            Route("Permissions/{PermissionID}")]
        public IActionResult EditRightsOfPermission(RightsForPermissionDTO model) {
            var permissionRights = new List<PermissionRights>();

            var rightIDs = new List<string>();

            foreach (var right in model.RightsWithIsAssigned) {
                if (right.IsAssigned) {
                    rightIDs.Add(right.ID.ToString());
                }
            }

            var rightIDsToString = string.Join(",", rightIDs);

            var modelToList = new List<RightsForPermissionDTO> {
                model
            };

            permissionRights = modelToList.GroupBy(x => new { x.PermissionID }).Select(y =>
            new PermissionRights {
                PermissionID = y.Key.PermissionID,
                RightIDs = rightIDsToString
            }).ToList();

            AdminRepository.AssignRightsToPermission(permissionRights);
            return RedirectToAction("Index");
        }

        #endregion

    }
}