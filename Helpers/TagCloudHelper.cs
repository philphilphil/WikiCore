using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WikiCore.DB;

namespace WikiCore.Helpers
{
    public static class TagCloudHelper
    {
        public static List<TagEntry> GetTagCloudJson()
        {
            List<TagEntry> result = new List<TagEntry>();

            //TODO: fix context or move to service
            var options = new DbContextOptionsBuilder<WikiContext>().UseSqlite("Filename=./WikiCoreDatabase.db").Options;
            using (WikiContext db = new WikiContext(options))
            {
                var tags = db.Tags.ToList();

                foreach (var tag in tags)
                {
                    var weight = db.PageTags.Where(t => t.Tag == tag).Count();
                    result.Add(new TagEntry { text = tag.Name, weight = weight, link = "/Tag/" + tag.Name });
                }
            }
            return result;
        }
    }

    public class TagEntry
    {
        public string text { get; set; }
        public int weight { get; set; }
        public string link { get; set; }
    }
}