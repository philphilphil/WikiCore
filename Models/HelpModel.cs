using System.Collections.Generic;
using System.Linq;
using WikiCore.DB;

namespace WikiCore.Models
{

    public class HelpModel
    {

        public List<Page> Pages = new List<Page>();

        public string CategoryName { get; set; }

        public HelpModel()
        {
            using (var db = new WikiContext())
            {
                this.Pages = db.Pages.ToList();
            }
        }
    }
}
