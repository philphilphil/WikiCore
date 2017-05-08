using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WikiCore.DB;

namespace WikiCore.Helpers
{
    public static class TagCloudHelper
    {
        public static List<TagEntry> GetTagCloudJson(IDBService dbs)
        {
            List<TagEntry> result = new List<TagEntry>();

            var tags = dbs.GetAllTags();

            foreach (var tag in tags)
            {
                int weight = dbs.GetTagWeight(tag);
                result.Add(new TagEntry { text = tag.Name, weight = weight, link = "/Tag/" + tag.Name });
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