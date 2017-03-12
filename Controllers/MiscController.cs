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
            // if (m.CategoryId == m.CategoryNewId)
            // {
            //    return Error("Please select a different category to move pages to");
            // } else {
            //     DBService.DeleteCategory(m.CategoryId, m.CategoryNewId);
            // } 

            return RedirectToAction("Index");
        }

        private IActionResult Error(string message) {

            ViewData["ErrorMessage"] = message;

            return View("Index", new MiscModel());
        }
    }
}
