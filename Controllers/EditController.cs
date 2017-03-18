using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WikiCore.Models;
using WikiCore.DB;

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
        public IActionResult Index(int id)
        {
            return View(new EditModel(id));
        }

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

        public IActionResult Add()
        {
            return View(new EditModel(true));
        }

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

        public IActionResult Update(EditModel model)
        {

            dbs.UpdatePage(model);

            return RedirectToRoute("Page", new { id = model.Id });

        }
    }
}
