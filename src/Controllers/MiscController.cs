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
            if(m.TagToDeleteId == 0) {
                return Error("All tags are deleted.");
            }
            _dbs.DeleteTag(m.TagToDeleteId);
            return RedirectToAction("Index");
        }
    }
}
