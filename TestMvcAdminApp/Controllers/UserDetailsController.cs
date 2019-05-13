using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestMvcAdminApp;
using TestMvcAdminApp.Models;
using TestMvcAdminApp.Repositories;

namespace MvcCoreAdminApp.Controllers {

    [Authorize]
    public class UserDetailsController : Controller {

        private readonly UserManager<ApplicationUser> _userManager;

        public UserDetailsController(UserManager<ApplicationUser> userManager) {
            _userManager = userManager;
        }

        [ClaimRequirement("UserDetails-Index", "True")]
        //[claimrequirement]
        public IActionResult Index() {
            var model = AdminRepository.GetAllUsers();

            var allUsersWithRoles = new List<UserDetailsWithRoles>();

            allUsersWithRoles = model.GroupBy(x => new { x.ID, x.FirstName, x.LastName, x.CompanyName, x.Mobile }).Select(y =>
                new UserDetailsWithRoles {
                    ID = y.Key.ID,
                    CompanyName = y.Key.CompanyName,
                    FirstName = y.Key.FirstName,
                    LastName = y.Key.LastName,
                    Mobile = y.Key.Mobile,
                    Roles = y.Select(z => new Role { ID = z.RoleID, Name = z.RoleName, Description = z.RoleDescription }).ToList()
                }).ToList();

            return View(allUsersWithRoles);
        }

        #region Edit Roles
        [ClaimRequirement("UserDetails-EditRolesOfUser", "True")]
        [HttpGet, Route("UserDetails/{UserID}")]
        public IActionResult EditRolesOfUser(int userID) {
            var rolesList = AdminRepository.GetRolesByUserID(userID);
            var roles = rolesList.FirstOrDefault();
            return View(roles);
        }

        [HttpPost, Route("UserDetails/{UserID}")]
        public async Task<IActionResult> EditRolesOfUser(RolesForUserDTO model) {
            var userRoles = new List<AssignRolesToUser>();
            var roleIDs = new List<string>();
            var userName = AdminRepository.GetUserNameByUserID(model.UserID);
            ApplicationUser user = await _userManager.FindByNameAsync(userName);

            foreach (var role in model.RolesWithIsAssigned) {
                var roleName = AdminRepository.GetRoleNameByRoleID(role.ID);

                if (role.IsAssigned) {
                    roleIDs.Add(role.ID.ToString());
                    await _userManager.AddToRoleAsync(user, roleName);
                } else {
                    await _userManager.RemoveFromRoleAsync(user, roleName);
                }
            }

            var roleIDsToString = string.Join(",", roleIDs);

            var modelToList = new List<RolesForUserDTO> {
                model
            };

            userRoles = modelToList.GroupBy(x => new { x.UserID }).Select(y =>
            new AssignRolesToUser {
                UserID = y.Key.UserID,
                RoleIDs = roleIDsToString
            }).ToList();

            AdminRepository.AssignRolesToUser(userRoles);
            await HttpContext.RefreshLoginAsync();
            return RedirectToAction("Index");
        }

        #endregion
    }
}