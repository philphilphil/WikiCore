using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WikiCore.Models;
using WikiCore.DB;
using Microsoft.AspNetCore.Authorization;

namespace WikiCore.Controllers
{
    public class EditController : Controller
    {
        private DBService _dbs;
        public DBService dbs
        {
            get
            {
                if (_dbs == null)
                {
                    _dbs = new DBService();
                }
                return this._dbs;
            }
            set { }
        }
        [Authorize]
        public IActionResult Index(int id)
        {
            return View(new EditModel(id));
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            if(id == 1) {
                ViewData["ErrorMessage"] = "Can't delete first page";
                return View("Index", new EditModel(id));
            }

            dbs.DeletePage(id);

            //Redirect to first page
            return RedirectToRoute("Page", new { id = 1 });
        }

        [Authorize]
        public IActionResult Add()
        {
            return View(new EditModel(true));
        }

        [Authorize]
        public IActionResult Save(EditModel model)
        {
            int pageId = dbs.SavePage(model);

            if (pageId == 0)
            {
                ViewData["ErrorMessage"] = "Something went wrong adding the page..";
                return View("Add", new EditModel(true));
            }

            return RedirectToRoute("Page", new { id = pageId });

        }

        [Authorize]
        public IActionResult Update(EditModel model)
        {

            dbs.UpdatePage(model);

            return RedirectToRoute("Page", new { id = model.Id });

        }
    }
}