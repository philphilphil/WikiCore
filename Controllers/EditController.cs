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
            using (var db = new WikiContext())
            {
                var page = new Page {
                    Title = model.Title, 
                    Content = model.pageContent,
                  //  CategoryId = int.Parse(model.CategoryId)

                };
                db.Pages.Add(page);
                db.SaveChanges();      

                return RedirectToAction("Index", "Home", new { page.PageId }); 
            }           
        }

        public IActionResult Update(EditModel model)
        {
            using (var db = new WikiContext())
            {
                var page = db.Pages.Where(p => p.PageId == model.Id).FirstOrDefault();
                page.Title = model.Title;
                page.Content = model.pageContent;
              //  page.CategoryId = int.Parse(model.CategoryId);
                db.SaveChanges();      

                return RedirectToAction("Index", "Home", new { page.PageId }); 
            }           
        }
    }
}
