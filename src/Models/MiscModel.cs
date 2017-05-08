using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WikiCore.DB;

namespace WikiCore.Models
{

    public class MiscModel
    {
        private DBService _dbs;
        public DBService dbs
        {
            get
            {
                if (_dbs == null)
                {
                    _dbs = new DBService();
                }
                return this._dbs;
            }
            set { }
        }
        public List<Page> Pages = new List<Page>();
        public string CategoryName { get; set; }
        public int TagId { get; set; }
        
        public List<SelectListItem> TagsSelect = new List<SelectListItem>();

        public MiscModel()
        {
            this.Pages = dbs.GetAllPages();
            this.TagsSelect =  dbs.GetAllTags().Select(x => new SelectListItem { Value = x.TagId.ToString(),Text = x.Name }).ToList();
        }
    }
}
