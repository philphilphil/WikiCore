using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WikiCore.Models;
using WikiCore.SQLite;

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

        public IActionResult Update(EditModel model)
        {
            using (var db = new WikiContext())
            {
                var page = db.Pages.Where(p => p.Id == model.Id).FirstOrDefault();
                page.Title = model.Title;
                page.Content = model.pageContent;
                db.SaveChanges();      

                return RedirectToAction("Index", "Home", new { page.Id }); 
            }           
        }
    }
}