using Microsoft.AspNetCore.Mvc;
using WikiCore.Models;
using WikiCore.SearchHelpers;
using WikiCore.DB;

namespace WikiCore.Controllers
{
    public class MiscController : Controller
    {
        public IActionResult Index()
        {
            return View(new MiscModel());
        }

        public IActionResult AddCategory(MiscModel m)
        {
            if (!string.IsNullOrEmpty(m.CategoryName))
            {
                DBService.AddCategory(m);
            }

            return RedirectToAction("Index");
        }

        public IActionResult DeleteCategory(MiscModel m)
        {
            if (!string.IsNullOrEmpty(m.CategoryName))
            {
                //DBService.DelCategory(m);
            }

            return RedirectToAction("Index");
        }
    }
}
