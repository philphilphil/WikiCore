using WikiCore.Models;

namespace WikiCore.DB
{
    public static class DBService
    {
        public static void AddCategorie(HelpModel h)
        {
            using (var db = new WikiContext())
            {
                Category c = new Category();
                c.Name = "asd";

                db.Categories.Add(c);
                db.SaveChanges();
            }
        }
    }
}