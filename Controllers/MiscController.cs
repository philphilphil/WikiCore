using Microsoft.AspNetCore.Mvc;
using WikiCore.Models;
using WikiCore.DB;
using Microsoft.AspNetCore.Authorization;

namespace WikiCore.Controllers
{
    public class MiscController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View(new MiscModel());
        }

        private IActionResult Error(string message)
        {

            ViewData["ErrorMessage"] = message;

            return View("Index", new MiscModel());
        }

        [Authorize]
        public IActionResult DeleteTag(MiscModel m)
        {
            var dbs = new DBService();
            dbs.DeleteTag(m.TagId);
            return RedirectToAction("Index");
        }
    }
}
