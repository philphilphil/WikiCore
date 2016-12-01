using System.Linq;
using WikiCore.SQLite;

namespace WikiCore.Models
{
    public class EditModel {

        public string pageContent { get; set; }

        public string Title { get; set; }

        public int Id { get; set; }
        public EditModel(int id) {
            using (var db = new WikiContext())
            {
                var page = db.Pages.Where(p => p.Id == id).FirstOrDefault();

                if(page != null) {
                    this.pageContent = page.Content;
                    this.Title = page.Title;
                    this.Id = page.Id;
                }
            }

            
        }

         public EditModel() {
        }
    }
}