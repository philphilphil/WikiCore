using System;
using System.Linq;
using WikiCore.Models;

namespace WikiCore.DB
{
    public static class DBService
    {
        public static void AddCategory(MiscModel h)
        {
            using (var db = new WikiContext())
            {
                Category c = new Category();
                c.Name = h.CategoryName;
                c.CategoryParentId = h.CategoryId;

                db.Categories.Add(c);
                db.SaveChanges();
            }
        }

        internal static void DeleteCategory(int categoryId, int newCategoryId)
        {
            using (var db = new WikiContext())
            {
                var movePages = db.Pages.Where(c => c.Id == categoryId).ToList();
                movePages.ForEach(c => c.CategoryId = newCategoryId);

                var moveCategories = db.Categories.Where(c => c.CategoryParentId == categoryId).ToList();
                moveCategories.ForEach(c => c.CategoryParentId = newCategoryId);
                
                var delC = db.Categories.Where(c => c.Id == categoryId).FirstOrDefault();
                db.Categories.Remove(delC);
                db.SaveChanges();
            }
        }
    }
}