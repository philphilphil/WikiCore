using System;
using System.Collections.Generic;
using System.Linq;
using WikiCore.Models;

namespace WikiCore.DB
{
    public static class DBService
    {
        internal static int SavePage(EditModel model)
        {
            using (var db = new WikiContext())
            {
                var page = new Page
                {
                    Title = model.Title,
                    Content = model.pageContent,
                };

                db.Pages.Add(page);
                db.SaveChanges();

                UpdateTags(page.PageId, model.Tags);

                return page.PageId;
            }
        }

        internal static void DeleteTag(int tagId)
        {
            using (var db = new WikiContext())
            {
                var tag = db.Tags.Where(t => t.TagId == tagId).FirstOrDefault();
                db.Tags.Remove(tag);

                var tagReferences = db.PageTags.Where(p => p.TagId == tagId).ToList();
                db.PageTags.RemoveRange(tagReferences);

                db.SaveChanges();
            }
        }

        //Create, Remove or add new Tags and reference them to Pages
        private static void UpdateTags(int pageId, string tags)
        {
            using (var db = new WikiContext())
            {
                //Todo: Remove Tags that where removed in the view.

                List<String> tagList = tags.Split(',').ToList();

                foreach (string tag in tagList)
                {
                    Tag dbTag = GetTag(tag);

                    CreatePageTageReference(dbTag.TagId, pageId);
                }

            }
        }

        // Get all tags for the given page as comma seperated string for the jQuery-TagEditor
        internal static string LoadTags(int pageId)
        {
            string pageTags = "";
            using (var db = new WikiContext())
            {
                var allTags = db.PageTags.Where(t => t.PageId == pageId).Select(t => t.Tag.Name).ToList();

                pageTags = string.Join(",", allTags);

            }

            return pageTags;
        }

        //Check if reference between Tag and Page already exists, if not create it
        private static void CreatePageTageReference(int tagId, int pageId)
        {
            using (var db = new WikiContext())
            {
                PageTag tag = db.PageTags.Where(t => t.PageId == pageId && t.TagId == tagId).FirstOrDefault();

                if (tag == null)
                {
                    PageTag newTag = new PageTag();
                    newTag.PageId = pageId;
                    newTag.TagId = tagId;
                    db.PageTags.Add(newTag);
                    db.SaveChanges();
                }
            }
        }

        //Get Tag from db or create it if not existing yet
        private static Tag GetTag(string name)
        {
            using (var db = new WikiContext())
            {
                Tag tag = db.Tags.Where(t => t.Name == name).FirstOrDefault();

                if (tag == null)
                {
                    Tag newTag = new Tag();
                    newTag.Name = name;
                    db.Tags.Add(newTag);
                    db.SaveChanges();
                    return newTag;
                }
                else
                {
                    return tag;
                }
            }
        }
    }

}