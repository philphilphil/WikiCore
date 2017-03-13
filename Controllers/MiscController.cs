using Microsoft.AspNetCore.Mvc;
using WikiCore.Models;
using WikiCore.DB;

namespace WikiCore.Controllers
{
    public class MiscController : Controller
    {
        public IActionResult Index()
        {
            return View(new MiscModel());
        }

        private IActionResult Error(string message) {

            ViewData["ErrorMessage"] = message;

            return View("Index", new MiscModel());
        }
    }
}
