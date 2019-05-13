using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestMvcAdminApp.Models;
using TestMvcAdminApp.Repositories;

namespace MvcCoreAdminApp.Controllers {
    
    public class RightsController : Controller {

        [ClaimRequirement("Rights-Index", "True")]
        public IActionResult Index() {
            var allRights = AdminRepository.GetAllRights();
            return View(allRights);
        }

        [ClaimRequirement("Rights-CreateRight", "True")]
        [HttpGet, Route("Rights/CreateNew")]
        public IActionResult CreateRight() {
            return View();
        }

        [HttpPost, Route("Rights/CreateNew")]
        public IActionResult CreateRight(Right model) {
            if (!ModelState.IsValid) {
                return View(model);
            }
            AdminRepository.CreateRight(model);
            return RedirectToAction("Index");
        }
    }
}