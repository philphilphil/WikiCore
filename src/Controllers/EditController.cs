using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WikiCore.Models;
using WikiCore.DB;
using Microsoft.AspNetCore.Authorization;

namespace WikiCore.Controllers
{
    public class EditController : Controller
    {
        private readonly IDBService _dbs;
        public EditController(IDBService dbs)
        {
            _dbs = dbs;
        }

        [Authorize]
        public IActionResult Index(int id)
        {
            return View(new EditModel(id, _dbs));
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            if (id == 1)
            {
                ViewData["ErrorMessage"] = "Can't delete first page";
                return View("Index", new EditModel(id, _dbs));
            }

            _dbs.DeletePage(id);

            //Redirect to first page
            return RedirectToRoute("Page", new { id = 1 });
        }

        [Authorize]
        public IActionResult Add()
        {
            return View(new EditModel(_dbs));
        }

        [Authorize]
        public IActionResult Save(EditModel model)
        {
            int pageId = _dbs.SavePage(model);

            if (pageId == 0)
            {
                ViewData["ErrorMessage"] = "Something went wrong adding the page..";
                return View("Add", new EditModel(_dbs));
            }

            return RedirectToRoute("Page", new { id = pageId });

        }

        [Authorize]
        public IActionResult Update(EditModel model)
        {

            _dbs.UpdatePage(model);

            return RedirectToRoute("Page", new { id = model.Id });

        }
    }
}
