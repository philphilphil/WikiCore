using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using WikiCore.DB;
using WikiCore.Models;
using WikiCore.Helpers;
using Xunit;

namespace Tests
{
    public class DBServiceTest
    {
        private WikiContext context { get; set; }

        public DBServiceTest()
        {
            var conn = new SqliteConnection("DataSource=:memory:");
            conn.Open();
            var options = new DbContextOptionsBuilder<WikiContext>().UseSqlite(conn).Options;
            var context = new WikiContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            this.context = context;

            TestDataCreator.CreateFourPagesWithTags(context);
        }

        [Fact]
        public void FourPagesCreated()
        {
            //should find 4 items total in DB
            Assert.Equal(4, context.Pages.Count());
        }

        [Fact]
        public void FourTagsCreated()
        {
            //should find 4 tags total in DB
            Assert.Equal(4, context.Tags.Count());
        }

        [Fact]
        public void OneTagDeleted()
        {
            DBService dbs = new DBService(context);
            dbs.DeleteTag(1); //Delete first tag

            //should find 3 tags total in DB
            Assert.Equal(3, context.Tags.Count());

            //should find 3 tags total for page 1
            int refCount = context.PageTags.Where(p => p.PageId == 1).Count();
            Assert.Equal(3, refCount);
        }

        [Fact]
        public void OnePageUpdated()
        {
            EditModel em = new EditModel(false);
            em.Id = 1;
            em.pageContent = "empty";
            em.Tags = "eins,zwei,drei,vier";
            em.Title = "tit";

            DBService dbs = new DBService(context);
            dbs.UpdatePage(em);

            var updatedPage = context.Pages.Where(x => x.PageId == 1).FirstOrDefault();

            //values changed
            Assert.Equal(updatedPage.Title, "tit");
            Assert.Equal(updatedPage.Content, "empty");
        }

        [Fact]
        public void OneTagDeletedFromPage()
        {
            EditModel em = new EditModel(false);
            em.Id = 2;
            em.pageContent = "empty";
            em.Tags = "eins,zwei,drei";
            em.Title = "tit";

            DBService dbs = new DBService(context);
            dbs.UpdatePage(em);

            //should still find 4 tags in total (other references)
            Assert.Equal(4, context.Tags.Count());

            //should find 3 tags total for page 2
            int refCount = context.PageTags.Where(p => p.PageId == 2).Count();
            Assert.Equal(3, refCount);
        }

        [Fact]
        public void TwoTagsAddedToPage()
        {
            EditModel em = new EditModel(false);
            em.Id = 2;
            em.pageContent = "empty";
            em.Tags = "eins,zwei,drei,vier,fuenf,sechs";
            em.Title = "tit";

            DBService dbs = new DBService(context);
            dbs.UpdatePage(em);

            //should find 6 tags
            Assert.Equal(6, dbs.GetAllTags().Count());

            //should find 6 tags total for page 2
            int refCount = context.PageTags.Where(p => p.PageId == 2).Count();
            Assert.Equal(6, refCount);
        }

    }
}