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
            //Move to db service
            Tag tag = dbs.GetTagByName(tagname.ToLower());

            if (tag != null) {
                this.Tag = tag;
            } else {
                
            }

            this.Pages = dbs.GetPagesWithTag(tag);
        }
    }
}
