using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using WikiCore.DB;

namespace WikiCore.Models
{

    public class MiscModel
    {

        public List<Page> Pages = new List<Page>();

        // public List<Category> Categories = new List<Category>();
        public List<SelectListItem> Categories = new List<SelectListItem>();

        public string CategoryName { get; set; }

        public int CategoryId { get; set; }

        public MiscModel()
        {
            using (var db = new WikiContext())
            {
                this.Pages = db.Pages.ToList();
                LoadCategories();
            }
        }

        private void LoadCategories()
        {
            using (var db = new WikiContext())
            {
                var cats = db.Categories.ToList();
                this.Categories.Add(new SelectListItem
                {
                    Text = "None",
                    Value = "0",
                });

                foreach (var item in cats)
                {
                    this.Categories.Add(new SelectListItem
                    {
                        Text = item.Name,
                        Value = item.Id.ToString(),
                    });
                }
            }
        }
    }
}
