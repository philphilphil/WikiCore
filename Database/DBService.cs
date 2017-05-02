using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WikiCore.Models;

namespace WikiCore.DB
{
    public class DBService
    {

        private readonly WikiContext db;

        public DBService(WikiContext context)
        {
            this.db = context;
        }

        public DBService()
        {
            //When no context is given create default one. this will always be the case except for unit tests
            var options = new DbContextOptionsBuilder<WikiContext>().UseSqlite("Filename=./WikiCoreDatabase.db").Options;
            this.db = new WikiContext(options);
        }
        internal int SavePage(EditModel model)
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

        public void DeletePage(int id)
        {
            var page = db.Pages.Where(p => p.PageId == id).FirstOrDefault();
            db.Pages.Remove(page);

            var tags = db.PageTags.Where(t => t.PageId == id).ToList();
            db.PageTags.RemoveRange(tags);

            db.SaveChanges();
        }

        public List<Tag> GetAllTags()
        {
            return db.Tags.ToList();
        }

        public List<Page> GetPagesWithTag(Tag tag)
        {
            var tagRef = db.PageTags.Where(x => x.Tag == tag).Select(p => p.PageId).ToList();

            return db.Pages.Where(x => tagRef.Contains(x.PageId)).ToList();
        }

        public List<Page> GetAllPages()
        {
            return db.Pages.ToList();
        }

        public Tag GetTagByName(string tagname)
        {
            return db.Tags.Where(x => x.Name.ToLower() == tagname).FirstOrDefault();
        }

        public Page GetPageOrDefault(int id)
        {
            Page page = db.Pages.Where(p => p.PageId == id).FirstOrDefault();

            //If not found get default
            if (page == null)
            {
                page = db.Pages.Where(p => p.PageId == 1).FirstOrDefault();
            }

            return page;
        }

        //Delete tag and all references between page and tag
        public void DeleteTag(int tagId)
        {
            var tag = db.Tags.Where(t => t.TagId == tagId).FirstOrDefault();
            db.Tags.Remove(tag);

            var tagReferences = db.PageTags.Where(p => p.TagId == tagId).ToList();
            db.PageTags.RemoveRange(tagReferences);

            db.SaveChanges();
        }

        internal void UpdatePage(EditModel model)
        {
            var page = db.Pages.Where(p => p.PageId == model.Id).FirstOrDefault();
            page.Title = model.Title;
            page.Content = model.pageContent;
            UpdateTags(model.Id, model.Tags);
            db.SaveChanges();
        }

        //Create, Remove or add new Tags and reference them to Pages
        private void UpdateTags(int pageId, string tags)
        {
            List<String> tagList;
            if (string.IsNullOrEmpty(tags))
            {
                tagList = new List<String>();
            }
            else
            {
                tagList = tags.Split(',').ToList();
            }
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

        private void RemoveTagReference(int pageId, string tag)
        {
            var tags = db.PageTags.Where(t => t.PageId == pageId && t.Tag.Name == tag).ToList();

            db.PageTags.RemoveRange(tags);
            db.SaveChanges();
        }

        // Get all tags for the given page as comma seperated string for the jQuery-TagEditor
        internal string LoadTagsForPage(int pageId)
        {
            string pageTags = "";

            var allTags = db.PageTags.Where(t => t.PageId == pageId).Select(t => t.Tag.Name).ToList();

            pageTags = string.Join(",", allTags);


            return pageTags;
        }

        //Check if reference between Tag and Page already exists, if not create it
        private void CreatePageTagReference(int tagId, int pageId)
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

        //Get Tag from db or create it if not existing yet
        private Tag GetTag(string name)
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