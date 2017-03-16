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

        public IActionResult Add()
        {
            return View(new EditModel());
        }

        public IActionResult Save(EditModel model)
        {
            int pageId = dbs.SavePage(model);

            if (pageId == 0)
            {
                ViewData["ErrorMessage"] = "Something went wrong adding the page..";
                return View("Add", new EditModel());
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
