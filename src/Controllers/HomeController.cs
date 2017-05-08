using Microsoft.AspNetCore.Mvc;
using WikiCore.Models;
using WikiCore.Helpers;
using WikiCore.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WikiCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        public HomeController(SignInManager<IdentityUser> signInManager)
        {
            this._signInManager = signInManager;
        }

        public IActionResult Index(int id)
        {
            if (!this._signInManager.IsSignedIn(User))
            {
                id = 1;
            }
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