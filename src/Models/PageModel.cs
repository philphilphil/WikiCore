using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using WikiCore.DB;

namespace WikiCore.Models
{

    public class PageModel
    {

        private readonly IDBService _dbs;
        public string pageContent { get; set; }
        public string Title { get; set; }

        public string Tags { get; set; }
        public int Id { get; set; }

        //public List<Category> Categories = new List<Category>();
        public PageModel(int id, IDBService dbs)
        {
            _dbs = dbs;
            //Search for page, if not found load default page
            Page page = _dbs.GetPageOrDefault(id);

            LoadPageData(page);
        }

        private void LoadPageData(Page page)
        {
            this.Title = page.Title;
            this.pageContent = CommonMark.CommonMarkConverter.Convert(page.Content);
            this.Id = page.PageId;
            this.Tags = _dbs.LoadTagsForPage(page.PageId);
        }


    }
}