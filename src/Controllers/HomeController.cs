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
        private readonly IDBService _dbs;
        public HomeController(SignInManager<IdentityUser> signInManager, IDBService dbs)
        {
            _dbs = dbs;
            this._signInManager = signInManager;
        }

        public IActionResult Index(int id)
        {
            if (!this._signInManager.IsSignedIn(User))
            {
                id = 1;
            }
            PageModel m = new PageModel(id, _dbs);

            return View(m);
        }

        //Tags with name for pretty links
        [Authorize]
        public IActionResult Tag(string name)
        {
            var t = new TagOverviewModel(name, _dbs);
            return View(t);
        }

        [Authorize]
        public IActionResult Cloud(string name)
        {
            var c = new CloudModel(_dbs);
            return View(c);
        }

        public IActionResult Error()
        {
            return View();
        }

        [Authorize]
        public JsonResult Search(string text)
        {

            var results = SearchHelper.Search(text.ToLower(), _dbs);
            //Build correct object for semantic-ui search box
            var searchResults = new
            {
                results = results
            };

            return Json(searchResults);
        }
    }
}
