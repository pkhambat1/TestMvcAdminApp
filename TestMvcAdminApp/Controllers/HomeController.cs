using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TestMvcAdminApp;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using TestMvcAdminApp.Data;
using Microsoft.AspNetCore.Mvc.Routing;

namespace MvcCoreAdminApp.Controllers {
    // [Authorize]
    public class HomeController : Controller {

        private readonly ApplicationDbContext _context;
        private readonly IActionContextAccessor _actionContextAccessor;

        public HomeController(ApplicationDbContext context,
                              IActionContextAccessor actionContextAccessor) {
            _context = context;
            _actionContextAccessor = actionContextAccessor;

        }

        public IActionResult Index() {
            UrlHelper url = new UrlHelper(_actionContextAccessor.ActionContext);
            string urlEdit = url.RouteUrl("Default");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
