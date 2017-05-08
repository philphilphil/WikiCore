using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WikiCore.DB;

namespace WikiCore.Models
{
    public class TagOverviewModel
    {

        private readonly IDBService _dbs;
        public Tag Tag { get; set; }
        public List<Page> Pages = new List<Page>();

        public TagOverviewModel(string tagname, IDBService dbs)
        {
            _dbs = dbs;

            LoadTagData(tagname);
        }
        
        private void LoadTagData(string tagname)
        {
            Tag tag = _dbs.GetTagByName(tagname.ToLower());

            if (tag != null)
            {
                this.Tag = tag;
                this.Pages = _dbs.GetPagesWithTag(tag);
            }
            else
            {
                //Not the best errorhandling, fix later
                this.Tag = new Tag { Name = "Not found." };
            }
        }
    }
}
