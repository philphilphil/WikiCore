using System.Linq;
using Microsoft.EntityFrameworkCore;
using WikiCore.DB;

public static class DbInitializer {
    public static void InitializeDb() {
        
        var options = new DbContextOptionsBuilder<WikiContext>().UseSqlite("Filename=./WikiCoreDatabase.db").Options;
        using (WikiContext db = new WikiContext(options)) {
            
            db.Database.EnsureCreated();

            //Only Initialize if DB is Empty
            if (db.Pages.Any())
                return;
            
            CreateOverviewPage(db);
            
        }
    }

    private static void CreateOverviewPage(WikiContext db)
    {
        
        Tag t1 = new Tag();
        t1.Name = "WikiCore";
        t1.Color = 1;
        db.Tags.Add(t1);

        Page p = new Page();
        p.Title = "Start";
        p.Content = "Welcome to __WikiCore__. Some Text will be added here explaining how WikiCore works.";
        db.Pages.Add(p);

        var pt = new PageTag { Tag = t1, Page = p };
        db.PageTags.Add(pt);

        db.SaveChanges();
    }
}