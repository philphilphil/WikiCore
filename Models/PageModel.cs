
using System.Collections.Generic;
using System.Linq;
using WikiCore.SQLite;

namespace WikiCore.Models
{

    public class PageModel {

        //public List<LogEntry> Logs = new List<LogEntry>();
        public string pageContent { get; set;}
        public string Title {get; set;}
        public int Id { get; set; }
        public PageModel(int id) {
            
            using (var db = new WikiContext())
            {
                //Search for page, if not found load default page
                Page page = db.Pages.Where(p => p.Id == id).FirstOrDefault();
                if(page != null) {
                    LoadPageData(page);
                } else {
                    Page pageOverview = db.Pages.Where(p => p.Id == 1).FirstOrDefault();
                    LoadPageData(pageOverview);
                }
            }
        }

        private void LoadPageData( Page page) {
            this.Title = page.Title;
            this.pageContent =  CommonMark.CommonMarkConverter.Convert(page.Content);
            this.Id = page.Id;                    
        }
            
        
    }
}