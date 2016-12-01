using Microsoft.AspNetCore.Mvc;
using WikiCore.Models;

namespace WikiCore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(int id)
        {
            PageModel m = new PageModel(id);
           
           return View(m);
        }

        public IActionResult Overview()
        {

            return View(new OverviewModel());
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
