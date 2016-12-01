using Microsoft.AspNetCore.Mvc;
using WikiCore.Models;
using WikiCore.SQLite;

namespace WikiCore.Controllers
{
    public class EditController : Controller
    {
        public IActionResult Index()
        {
            return View(new EditModel());
        }

        public IActionResult Add()
        {
            return View(new EditModel());
        }

        public IActionResult Save(EditModel model)
        {
            using (var db = new WikiContext())
            {
                var page = new Page {
                    Title = model.Title, 
                    Content = model.pageContent
                };
                db.Pages.Add(page);
                db.SaveChanges();      

                return RedirectToAction("Index", "Home", new { page.Id }); 
            }           
        }
    }
}
