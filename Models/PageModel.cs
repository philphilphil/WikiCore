
using System;
using System.Collections.Generic;
using System.Linq;
using WikiCore.DB;

namespace WikiCore.Models
{

    public class PageModel
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
        public string pageContent { get; set; }
        public string Title { get; set; }

        public string Tags { get; set; }
        public int Id { get; set; }

        //public List<Category> Categories = new List<Category>();
        public PageModel(int id)
        {
            
            using (var db = new WikiContext())
            {
                //Search for page, if not found load default page
                Page page = dbs.GetPageOrDefault(id);

                LoadPageData(page);
            }
        }

        private void LoadPageData(Page page)
        {
            this.Title = page.Title;
            this.pageContent = CommonMark.CommonMarkConverter.Convert(page.Content);
            this.Id = page.PageId;
            this.Tags = dbs.LoadTags(page.PageId);
        }


    }
}