using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WikiCore.Models;

namespace WikiCore.DB
{
    public class DBService : IDBService
    {

        private readonly WikiContext db;

        public DBService(WikiContext context)
        {
            this.db = context;
        }

        public int SavePage(EditModel model)
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

        public void UpdatePage(EditModel model)
        {
            var page = db.Pages.Where(p => p.PageId == model.Id).FirstOrDefault();
            page.Title = model.Title;
            page.Content = model.pageContent;
            UpdateTags(model.Id, model.Tags);
            db.SaveChanges();
        }

        //Create, Remove or add new Tags and reference them to Pages
        public void UpdateTags(int pageId, string tags)
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

        public void RemoveTagReference(int pageId, string tag)
        {
            var tags = db.PageTags.Where(t => t.PageId == pageId && t.Tag.Name == tag).ToList();

            db.PageTags.RemoveRange(tags);
            db.SaveChanges();
        }

        // Get all tags for the given page as comma seperated string for the jQuery-TagEditor
        public string LoadTagsForPage(int pageId)
        {
            string pageTags = "";

            var allTags = db.PageTags.Where(t => t.PageId == pageId).Select(t => t.Tag.Name).ToList();

            pageTags = string.Join(",", allTags);


            return pageTags;
        }

        //Check if reference between Tag and Page already exists, if not create it
        public void CreatePageTagReference(int tagId, int pageId)
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
        public Tag GetTag(string name)
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

        public List<Page> SearchPages(string searchText)
        {
            return db.Pages.Where(x => x.Content.ToLower().Contains(searchText) || x.Title.ToLower().Contains(searchText)).ToList();
        }

        public int GetTagWeight(Tag tag)
        {
            return db.PageTags.Where(t => t.Tag == tag).Count();
        }

        public bool SomeUserRegistered()
        {
           return db.Users.Any();
        }
    }

}