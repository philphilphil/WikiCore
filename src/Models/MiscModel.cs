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

        public MiscModel() {}

        public MiscModel(IDBService _dbs)
        {
            this.Pages = _dbs.GetAllPages();
            this.TagsSelect = _dbs.GetAllTags().Select(x => new SelectListItem { Value = x.TagId.ToString(), Text = x.Name }).ToList();
        }
    }
}
