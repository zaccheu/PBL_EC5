using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using PBL_EC5.Models;
using System.Diagnostics;

namespace PBL_EC5.Controllers
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
            if (!HelperControllers.VerificaUserLogado(HttpContext.Session))
                return RedirectToAction("Login", "Usuario");
            else
                ViewBag.Logado = true;

            return View();
        }

        public IActionResult Sobre()
        {
            if (!HelperControllers.VerificaUserLogado(HttpContext.Session))
                return RedirectToAction("Login", "Usuario");
            else
                ViewBag.Logado = true;

            return View();
        }

        public IActionResult SobreRegressao()
        {
            return View("SobreRegressaoLinear");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
