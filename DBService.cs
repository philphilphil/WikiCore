using WikiCore.Models;

namespace WikiCore.DB
{
    public static class DBService
    {
        public static void AddCategorie(MiscModel h)
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
    }
}