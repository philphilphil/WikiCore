using System.Linq;
using WikiCore.DB;

public static class DbInitializer {
    public static void InitializeDb() {
        
        using (WikiContext db = new WikiContext()) {
            
            db.Database.EnsureCreated();

            //Only Initialize if DB is Empty
            if (db.Pages.Any())
                return;
            
            CreateOverviewPage(db);
            
        }
    }

    private static void CreateOverviewPage(WikiContext db)
    {
        
        Category c = new Category();
        c.Name = "Main";
        db.Categories.Add(c);

        Page p = new Page();
        p.CategoryId = c.Id;
        p.Title = "Start";
        p.Content = "Welcome to __WikiCore__. Some Text will be added here explaining how WikiCore works.";
        db.Pages.Add(p);

        db.SaveChanges();
    }
}