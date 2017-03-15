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
        public void FindSingleItem()
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
                    CreatePage("", "", "", context);

                }

                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new WikiContext(options))
                {
                    Assert.Equal(1, context.Pages.Count());
                   // Assert.AreEqual("http://sample.com", context.Blogs.Single().Url);
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
            m.Tags = "eins, zwei, drei, vier";
            m.Title = "Test Page One";
            m.pageContent = "eins zwei drei vier fuenf sechs sieben acht neun zehn elf zwoelf dreizehn test vierzehn fuenfzehn sechzehn siebzehn achtzehn neunzehn zwanzig";
            dbs.SavePage(m);
        }
    }
}