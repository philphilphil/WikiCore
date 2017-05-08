using Microsoft.AspNetCore.Mvc;
using WikiCore.Models;
using WikiCore.DB;
using Microsoft.AspNetCore.Authorization;

namespace WikiCore.Controllers
{
    public class MiscController : Controller
    {
        private readonly IDBService _dbs;
        public MiscController(IDBService dbs)
        {
            _dbs = dbs;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View(new MiscModel(_dbs));
        }

        private IActionResult Error(string message)
        {

            ViewData["ErrorMessage"] = message;

            return View("Index", new MiscModel(_dbs));
        }

        [Authorize]
        public IActionResult DeleteTag(MiscModel m)
        {
            _dbs.DeleteTag(m.TagId);
            return RedirectToAction("Index");
        }
    }
}
