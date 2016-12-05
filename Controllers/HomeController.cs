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

        public IActionResult Help()
        {

            return View(new HelpModel());
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult AddCategory(HelpModel m)
        {

            if (!string.IsNullOrEmpty(m.CategoryName))
            {
               DBService.AddCategorie(m);
            }

            return View("Help", new HelpModel());
        }
    }
}
