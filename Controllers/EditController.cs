using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WikiCore.Models;
using WikiCore.DB;

namespace WikiCore.Controllers
{
    public class EditController : Controller
    {
        public IActionResult Index(int id)
        {
            return View(new EditModel(id));
        }

        public IActionResult Add()
        {
            return View(new EditModel());
        }

        public IActionResult Save(EditModel model)
        {
            int pageId = DBService.SavePage(model);

            if (pageId == 0)
            {
                ViewData["ErrorMessage"] = "Something went wrong adding the page..";
                return View("Add", new EditModel());
            }

            return RedirectToAction("Index", "Home", new { id = pageId });

        }

        public IActionResult Update(EditModel model)
        {

            DBService.UpdatePage(model);

            return RedirectToAction("Index", "Home", new { id = model.Id });

        }
    }
}
