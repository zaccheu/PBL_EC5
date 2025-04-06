using Microsoft.AspNetCore.Mvc;

namespace PBL_EC5.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Dashboard1()
        {
            return View();
        }

        public IActionResult Dashboard2()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View("Error");
        }
    }
}