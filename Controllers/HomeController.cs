using Microsoft.AspNetCore.Mvc;
using WikiCore.Models;
using WikiCore.DB;

namespace WikiCore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(int id)
        {
            PageModel m = new PageModel(id);

            return View(m);
        }

        public IActionResult Misc()
        {

            return View(new MiscModel());
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult AddCategory(MiscModel m)
        {

            if (!string.IsNullOrEmpty(m.CategoryName))
            {
               DBService.AddCategorie(m);
            }

            return View("Help", new MiscModel());
        }
    }
}
