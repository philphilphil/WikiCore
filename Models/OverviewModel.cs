using System.Collections.Generic;
using System.Linq;
using WikiCore.SQLite;

namespace WikiCore.Models
{

    public class OverviewModel {

         public List<Page> Pages = new List<Page>();

        public OverviewModel() {
             using (var db = new WikiContext())
            {
                this.Pages = db.Pages.ToList();
            }
        }
    }
} 
