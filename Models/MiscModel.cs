using System.Collections.Generic;
using System.Linq;
using WikiCore.DB;

namespace WikiCore.Models
{

    public class MiscModel
    {

        public List<Page> Pages = new List<Page>();

        public List<Category> Categories = new List<Category>();

        public string CategoryName { get; set; }

        public MiscModel()
        {
            using (var db = new WikiContext())
            {
                this.Pages = db.Pages.ToList();
                this.Categories = db.Categories.ToList();
            }
        }
    }
}
