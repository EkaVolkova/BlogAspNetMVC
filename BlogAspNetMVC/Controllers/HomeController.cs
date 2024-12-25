using BlogAspNetMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAspNetMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogTrace("Открыта домашняя страница");
            return View();
        }

        //[Route("Home/Error/Default")]
        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

        [Route("Home/Error")]
        [HttpGet]
        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {

                if (statusCode == 401)
                    return View("Unauthorized");

                if (statusCode == 403)

                    return View("AccessDenied");

                if (statusCode == 404)
                    return View("NotFound");
            }
            return View("InternalServerError");
        }

        [Route("Home/Error/403")]
        public IActionResult Error403()
        {
            return View("AccessDenied");
        }

        [Route("Home/Error/401")]
        public IActionResult Error401()
        {
            return View("AccessDenied");
        }

        [Route("Home/Error/404")]
        public IActionResult Error404()
        {
            return View("NotFound");
        }

        [Route("Home/Error/500")]
        public IActionResult Error500()
        {
            return View("InternalServerError");
        }
    }
}
