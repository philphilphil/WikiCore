using System;
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

        public int CategoryId { get; set; }
        public int CategoryNewId { get; set; }

        public MiscModel()
        {
            using (var db = new WikiContext())
            {
                this.Pages = db.Pages.ToList();
            }
        }
    }
}
