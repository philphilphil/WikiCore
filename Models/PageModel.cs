
using System;
using System.Collections.Generic;
using System.Linq;
using WikiCore.DB;

namespace WikiCore.Models
{

    public class PageModel
    {

        //public List<LogEntry> Logs = new List<LogEntry>();
        public string pageContent { get; set; }
        public string Title { get; set; }
        public int Id { get; set; }

        //public List<Category> Categories = new List<Category>();
        public PageModel(int id)
        {

            using (var db = new WikiContext())
            {
                //Search for page, if not found load default page
                Page page = db.Pages.Where(p => p.PageId == id).FirstOrDefault();
                if (page != null)
                {
                    LoadPageData(page);
                }
                else
                {
                    Page pageOverview = db.Pages.Where(p => p.PageId == 1).FirstOrDefault();
                    LoadPageData(pageOverview);
                }
            }

         //   LoadCategories();
        }

        private void LoadCategories()
        {
            using (var db = new WikiContext())
            {
               // this.Categories = db.Categories.OrderByDescending(x => x.Name).ToList();
            }
        }

        private void LoadPageData(Page page)
        {
            this.Title = page.Title;
            this.pageContent = CommonMark.CommonMarkConverter.Convert(page.Content);
            this.Id = page.PageId;
        }


    }
}