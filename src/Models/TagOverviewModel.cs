using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WikiCore.DB;

namespace WikiCore.Models
{
    public class TagOverviewModel
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
        public Tag Tag { get; set; }
        public List<Page> Pages = new List<Page>();
        public TagOverviewModel(string tagname)
        {
            Tag tag = dbs.GetTagByName(tagname.ToLower());

            if (tag != null) {
                this.Tag = tag;
                this.Pages = dbs.GetPagesWithTag(tag);
            }else {
                //Not the best errorhandling, fix later
                this.Tag = new Tag { Name = "Not found."};
            }
        }
    }
}
