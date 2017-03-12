using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using WikiCore.DB;

namespace WikiCore.Models
{

    public class MiscModel
    {

        public List<Page> Pages = new List<Page>();

        public string CategoryName { get; set; }

        public int TagId { get; set; }

        public List<Tag> Tags = new List<Tag>();
        public List<SelectListItem> TagsSelect = new List<SelectListItem>();

        public MiscModel()
        {
            using (var db = new WikiContext())
            {
                this.Pages = db.Pages.ToList();
            }
        }
    }
}
