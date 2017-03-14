using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using WikiCore.DB;

namespace WikiCore.Models
{
    public class EditModel
    {

        public string pageContent { get; set; }

        public List<SelectListItem> Categories = new List<SelectListItem>();

        public string Tags { get; set; }

        public string Title { get; set; }

        public int Id { get; set; }
        public EditModel(int id)
        {
            using (var db = new WikiContext())
            {
                var page = db.Pages.Where(p => p.PageId == id).FirstOrDefault();

                if (page != null)
                {
                    this.pageContent = page.Content;
                    this.Title = page.Title;
                    this.Id = page.PageId;
                    this.Tags = DBService.LoadTags(page.PageId);
                }
            }
        }

        public EditModel()
        {
        }
    }
}