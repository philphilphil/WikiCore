using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WikiCore.DB;

namespace WikiCore.Helpers
{
    public static class SearchHelper
    {

        private const int descriptionWidth = 30;

        public static List<SearchResult> Search(string searchText, IDBService dbs)
        {
            List<SearchResult> result = new List<SearchResult>();

            List<Page> pagesFound = dbs.SearchPages(searchText);

            foreach (Page item in pagesFound)
            {
                result.Add(new SearchResult { title = item.Title, url = "/Page/" + item.PageId, description = GetDescription(searchText, item.Content) });
            }

            return result;
        }

        private static string GetDescription(string searchText, string content)
        {
            int indexOfsearchText = content.IndexOf(searchText);
            content = content.Replace("\r", "").Replace("\n", "");

            //If there is les than descriptionWidth-characters infront of the foundText, display from the start
            int startIndex;
            if (indexOfsearchText < descriptionWidth)
            {
                startIndex = 0;
            }
            else
            {
                startIndex = indexOfsearchText - descriptionWidth;
            }

            //Get descLength, if smaller than chars left, get everything
            int descLength;
            if (content.Length - descriptionWidth - searchText.Length - startIndex < descriptionWidth)
            {
                descLength = content.Length - startIndex;
            }
            else
            {
                descLength = descriptionWidth + searchText.Length + descriptionWidth;
            }

            string desc = content.Substring(startIndex, descLength);

            return desc;
        }
    }

    public class SearchResult
    {
        public string title { get; set; }
        public string url { get; set; }
        public string description { get; set; }
    }
}