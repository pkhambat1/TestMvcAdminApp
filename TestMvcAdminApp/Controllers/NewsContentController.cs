using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TestMvcAdminApp.Controllers {
    [Authorize]
    [Route("news-content")]
    public class NewsContentController : Controller {
        private readonly IServiceProvider _serviceProvider;

        public NewsContentController(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public IActionResult Index() {
            return View();
        }
    }
}