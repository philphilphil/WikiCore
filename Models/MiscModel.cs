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

        public List<Tuple<int, string, int>> CategoryTree = new List<Tuple<int, string, int>>();
        public string CategoryName { get; set; }

        public int CategoryId { get; set; }

        public MiscModel()
        {
            using (var db = new WikiContext())
            {
                this.Pages = db.Pages.ToList();
                LoadCategories();
                BuildCategoryTree();
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
        private void BuildCategoryTree()
        {
            using (var db = new WikiContext())
            {
                //Get toplevel Categories
                List<Category> toplevelCategories = db.Categories.Where(c => c.CategoryParentId == 0).OrderBy(c => c.Id).ToList();

                foreach (Category cat in toplevelCategories)
                {
                    CategoryTree.Add(Tuple.Create(1, cat.Name, cat.Id));
                    LoadCategorieChildren(cat, 1);
                }
            }

        }

        private void LoadCategorieChildren(Category cate, int v)
        {
            using (var db = new WikiContext())
            {
                List<Category> childCategories = db.Categories.Where(c => c.CategoryParentId == cate.Id).OrderBy(c => c.Id).ToList();

                foreach (Category cat in childCategories)
                {
                    CategoryTree.Add(Tuple.Create(v+1, cat.Name, cat.Id));
                    LoadCategorieChildren(cat, v+1);
                }
            }
        }
    }
}
