using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WikiCore.DB;

namespace WikiCore.Models
{

    public class MiscModel
    {

        public List<Page> Pages = new List<Page>();
        public string CategoryName { get; set; }
        public int TagId { get; set; }
        public List<SelectListItem> TagsSelect = new List<SelectListItem>();

        public MiscModel()
        {

            //Move to db service
            var options = new DbContextOptionsBuilder<WikiContext>().UseSqlite("Filename=./WikiCoreDatabase.db").Options;
            using (var db = new WikiContext(options))
            {
                this.Pages = db.Pages.ToList();
                this.TagsSelect =  db.Tags.Select(x => new SelectListItem { Value = x.TagId.ToString(),Text = x.Name }).ToList();
            }
        }
    }
}
