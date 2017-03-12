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

        public string CategoryId { get; set; }

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
                    
                   // var cat = db.Categories.Where(c => c.Id == page.CategoryId).FirstOrDefault();
                    // if (cat == null)
                    // {
                    //     //Default category, maybe add seed with cat??
                    //      this.CategoryId = "1";
                    // } else
                    // {
                    //     this.CategoryId = cat.Id.ToString();
                    // }
                }
            }

            LoadCategories();
        }

        public EditModel()
        {
            LoadCategories();
        }

        private void LoadCategories()
        {
            // using (var db = new WikiContext())
            // {
            //     var cats = db.Categories.ToList();

            //     foreach (var item in cats)
            //     {
            //         this.Categories.Add(new SelectListItem 
            //         {
            //             Text = item.Name,
            //             Value = item.Id.ToString(),
            //         });
            //     }
            // }
        }
    }
}