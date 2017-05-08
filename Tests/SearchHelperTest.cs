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
    public class SearchHelperTest
    {
        private WikiContext context { get; set; }

        public SearchHelperTest()
        {
            var conn = new SqliteConnection("DataSource=:memory:");
            conn.Open();
            var options = new DbContextOptionsBuilder<WikiContext>().UseSqlite(conn).Options;
            var context = new WikiContext(options);
            context.Database.EnsureCreated();
            this.context = context;

            TestDataCreator.CreateFourPagesWithTags(context);
        }

        [Fact]
        public void FindCorrectAmountOfPagesInSearch()
        {
            DBService dbs = new DBService(context);

            //should find 3 items with "test" in them
            List<SearchResult> sr = SearchHelper.Search("test", dbs);
            Assert.Equal(3, sr.Count());

            //should find no item with "NONE" in them
            List<SearchResult> sr2 = SearchHelper.Search("NONE", dbs);
            Assert.Equal(0, sr2.Count());

            //should find 3 item with "tes" in them
            List<SearchResult> sr3 = SearchHelper.Search("tes", dbs);
            Assert.Equal(3, sr3.Count());
        }

        [Fact]
        public void DescriptionCorrect()
        {
            DBService dbs = new DBService(context);
            //should find the correct description for the found item
            SearchResult sr = SearchHelper.Search("test", dbs)[0];

            //description should be correct amount of characters 30 in each direction + 4 for "test"
            Assert.Equal(64, sr.description.Length);
        }
    }
}