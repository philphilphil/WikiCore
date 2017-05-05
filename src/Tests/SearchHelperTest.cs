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
            //should find 3 items with "test" in them
            List<SearchResult> sr = SearchHelper.Search("test", context);
            Assert.Equal(3, sr.Count());

            //should find no item with "NONE" in them
            List<SearchResult> sr2 = SearchHelper.Search("NONE", context);
            Assert.Equal(0, sr2.Count());

            //should find 3 item with "tes" in them
            List<SearchResult> sr3 = SearchHelper.Search("tes", context);
            Assert.Equal(3, sr3.Count());
        }

        [Fact]
        public void DescriptionCorrect()
        {
            //should find the correct description for the found item
            SearchResult sr = SearchHelper.Search("test", context)[0];

            //description should be correct amount of characters 30 in each direction + 4 for "test"
            Assert.Equal(64, sr.description.Length);
        }
    }
}