using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace  WikiCore.DB
{
    public class WikiContext : DbContext
    {
        public DbSet<Page> Pages { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./WikiCoreDatabase.db");
        }
    }

    public class Page
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public string Content { get; set; }
    }

    public class Category {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CategoryParentId { get; set; }
        public string Name { get; set; }
    }
}