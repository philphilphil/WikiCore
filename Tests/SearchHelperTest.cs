using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using WikiCore.DB;
using WikiCore.Models;
using WikiCore.SearchHelpers;
using Xunit;

namespace Tests
{
    public class SearchHelperTest
    {
        [Fact]
        public void FindCorrectAmountOfPages()
        {
            // Open connection to DB in memory
            var conn = new SqliteConnection("DataSource=:memory:");
            conn.Open();

            try
            {
                var options = new DbContextOptionsBuilder<WikiContext>().UseSqlite(conn).Options;

                // Create DB, add sample entries
                using (var context = new WikiContext(options))
                {
                    context.Database.EnsureCreated();
                    CreatePage("title", "eins zwei drei vier fuenf sechs sieben acht neun zehn elf zwoelf dreizehn test vierzehn fuenfzehn sechzehn siebzehn achtzehn neunzehn zwanzig", "eins, zwei, drei, vier", context);
                    CreatePage("title2", "eins zwei drei vier fuenf sechs sieben acht neun zehn elf zwoelf dreizehn test vierzehn fuenfzehn sechzehn siebzehn achtzehn neunzehn zwanzig", "eins, zwei, drei, vier", context);
                    CreatePage("title3", "eins zwei drei vier fuenf sechs sieben acht neun zehn elf zwoelf dreizehn test vierzehn fuenfzehn sechzehn siebzehn achtzehn neunzehn zwanzig", "eins, zwei, drei, vier", context);
                    CreatePage("title4", "eins zwei drei vier fuenf sechs sieben acht neun zehn elf zwoelf dreizehn NOOO vierzehn fuenfzehn sechzehn siebzehn achtzehn neunzehn zwanzig", "eins, zwei, drei, vier", context);

                }

                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new WikiContext(options))
                {
                    //should find 4 items total in DB
                    Assert.Equal(4, context.Pages.Count());

                    //should find 3 items with "test" in them
                    List<SearchResult> sr = SearchHelper.Search("test", context);
                    Assert.Equal(3, sr.Count());
                }
            }
            finally
            {
                conn.Close();
            }
        }

        private void CreatePage(string title, string text, string tags, WikiContext context)
        {
            var dbs = new DBService(context);
            EditModel m = new EditModel();
            m.Tags = tags;
            m.Title = title;
            m.pageContent = text;
            dbs.SavePage(m);
        }
    }
}