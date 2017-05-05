using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WikiCore.Models;

namespace WikiCore.DB
{
    public class WikiContext : IdentityDbContext
    {
        public DbSet<Page> Pages { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PageTag> PageTags { get; set; }

        public WikiContext(DbContextOptions<WikiContext> options)
        : base(options)
        { }

        public WikiContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PageTag>()
                 .HasKey(t => new { t.PageId, t.TagId });

            modelBuilder.Entity<PageTag>()
                .HasOne(pt => pt.Page)
                .WithMany(p => p.PageTags)
                .HasForeignKey(pt => pt.PageId);

            modelBuilder.Entity<PageTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.PageTags)
                .HasForeignKey(pt => pt.TagId);
        }
    }

    public class Page
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PageId { get; set; }
        public string Title { get; set; }
        public List<PageTag> PageTags { get; set; }
        public string Content { get; set; }
    }

    public class Tag
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TagId { get; set; }
        public int Color { get; set; }
        public string Name { get; set; }
        public List<PageTag> PageTags { get; set; }
    }

    public class PageTag
    {
        public int PageId { get; set; }
        public Page Page { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}