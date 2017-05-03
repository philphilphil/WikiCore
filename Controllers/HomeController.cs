using Microsoft.AspNetCore.Mvc;
using WikiCore.Models;
using WikiCore.Helpers;
using WikiCore.DB;
using Microsoft.AspNetCore.Authorization;

namespace WikiCore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(int id)
        {
            PageModel m = new PageModel(id);

            return View(m);
        }

        //Tags with name for pretty links
        [Authorize]
        public IActionResult Tag(string name)
        {
            var t = new TagOverviewModel(name);
            return View(t);
        }

        [Authorize]
        public IActionResult Cloud(string name)
        {
            var c = new CloudModel();
            return View(c);
        }

        public IActionResult Error()
        {
            return View();
        }

        [Authorize]
        public JsonResult Search(string text)
        {

            var results = SearchHelper.Search(text.ToLower());
            //Build correct object for semantic-ui search box
            var searchResults = new
            {
                results = results
            };

            return Json(searchResults);
        }
    }
}
