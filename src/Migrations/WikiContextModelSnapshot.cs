using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WikiCore.DB;

namespace WikiCore.Migrations
{
    [DbContext(typeof(WikiContext))]
    partial class WikiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("WikiCore.DB.Page", b =>
                {
                    b.Property<int>("PageId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<string>("Title");

                    b.HasKey("PageId");

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("WikiCore.DB.PageTag", b =>
                {
                    b.Property<int>("PageId");

                    b.Property<int>("TagId");

                    b.HasKey("PageId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("PageTags");
                });

            modelBuilder.Entity("WikiCore.DB.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Color");

                    b.Property<string>("Name");

                    b.HasKey("TagId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("WikiCore.DB.PageTag", b =>
                {
                    b.HasOne("WikiCore.DB.Page", "Page")
                        .WithMany("PageTags")
                        .HasForeignKey("PageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WikiCore.DB.Tag", "Tag")
                        .WithMany("PageTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
