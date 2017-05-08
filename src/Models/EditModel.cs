using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using WikiCore.DB;

namespace WikiCore.Models
{
    public class EditModel
    {
        private readonly IDBService _dbs;

        public string pageContent { get; set; }

        public List<SelectListItem> Categories = new List<SelectListItem>();

        public string Tags { get; set; }

        public string AllTags { get; set; }

        public string Title { get; set; }

        public int Id { get; set; }
        public EditModel(int id, IDBService dbs)
        {
            _dbs = dbs;

            var page = _dbs.GetPageOrDefault(id);
            this.pageContent = page.Content;
            this.Title = page.Title;
            this.Id = page.PageId;
            this.Tags = _dbs.LoadTagsForPage(page.PageId);

            LoadTagsForAutocomplete();
        }

        private void LoadTagsForAutocomplete()
        {
            var allTags = _dbs.GetAllTags().Select(t => t.Name);

            foreach (var t in allTags)
            {
                this.AllTags += "'" + t + "',";
            }

        }

        public EditModel(IDBService dbs)
        {
            _dbs = dbs;
            LoadTagsForAutocomplete();
        }
        
        public EditModel()
        {

        }
    }
}