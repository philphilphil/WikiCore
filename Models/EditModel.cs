using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using WikiCore.DB;

namespace WikiCore.Models
{
    public class EditModel
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

        public List<SelectListItem> Categories = new List<SelectListItem>();

        public string Tags { get; set; }

        public string AllTags { get; set; }

        public string Title { get; set; }

        public int Id { get; set; }
        public EditModel(int id)
        {
            var page = dbs.GetPageOrDefault(id);
            this.pageContent = page.Content;
            this.Title = page.Title;
            this.Id = page.PageId;
            this.Tags = dbs.LoadTagsForPage(page.PageId);

            LoadTagsForAutocomplete();
        }

        private void LoadTagsForAutocomplete()
        {
            var allTags = dbs.GetAllTags().Select(t => t.Name);
            
            foreach(var t in allTags) {
                this.AllTags += "'" + t + "',";
            }

        }

        public EditModel()
        {
             LoadTagsForAutocomplete();
        }
    }
}