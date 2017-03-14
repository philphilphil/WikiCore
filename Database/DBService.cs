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

        //Delete tag and all references between page and tag
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

        internal static void UpdatePage(EditModel model)
        {
            using (var db = new WikiContext())
            {
                var page = db.Pages.Where(p => p.PageId == model.Id).FirstOrDefault();
                page.Title = model.Title;
                page.Content = model.pageContent;
                UpdateTags(model.Id, model.Tags);
                db.SaveChanges();
            }
        }

        //Create, Remove or add new Tags and reference them to Pages
        private static void UpdateTags(int pageId, string tags)
        {
            using (var db = new WikiContext())
            {
                List<String> tagList = tags.Split(',').ToList();
                List<String> currentTags = db.PageTags.Where(t => t.PageId == pageId).Select(t => t.Tag.Name).ToList();

                //Get items currently referenced to the page that where removed during edit
                List<string> removeTags = currentTags.Except(tagList).ToList();
                foreach (string tag in removeTags)
                {
                    RemoveTagReference(pageId, tag);
                }

                foreach (string tag in tagList)
                {
                    Tag dbTag = GetTag(tag);

                    CreatePageTagReference(dbTag.TagId, pageId);
                }

            }
        }

        private static void RemoveTagReference(int pageId, string tag)
        {
            using (var db = new WikiContext())
            {
                var tags = db.PageTags.Where(t => t.PageId == pageId && t.Tag.Name == tag).ToList();

                db.PageTags.RemoveRange(tags);
                db.SaveChanges();

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
        private static void CreatePageTagReference(int tagId, int pageId)
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