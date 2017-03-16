using Microsoft.AspNetCore.Mvc;
using WikiCore.Models;
using WikiCore.SearchHelpers;
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

        //Tags with name for pretty links
        public IActionResult Tag(string name)
        {
            TagOverviewModel t = new TagOverviewModel(name);
            return View(t);
        }

        public IActionResult Error()
        {
            return View();
        }

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
