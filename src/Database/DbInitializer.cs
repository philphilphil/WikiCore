using System.Linq;
using Microsoft.EntityFrameworkCore;
using WikiCore.DB;

public static class DbInitializer
{
    public static void InitializeDb(string connectionString)
    {

        var options = new DbContextOptionsBuilder<WikiContext>().UseSqlite(connectionString).Options;
        using (WikiContext db = new WikiContext(options))
        {

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
        p.Content = "Welcome to __WikiCore__.\r\n\r\nWikiCore is a modest, small and fast Wiki featuring [MarkDown](https://daringfireball.net/projects/markdown/) editing.\r\n\r\nUnlike regular Wikis pages are organized with tags.\r\n\r\nPlease report Bugs in a [GitHub-Issue](https://github.com/philphilphil/WikiCore/issues).";
        db.Pages.Add(p);

        var pt = new PageTag { Tag = t1, Page = p };
        db.PageTags.Add(pt);

        db.SaveChanges();
    }
}