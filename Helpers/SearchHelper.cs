using System.Collections.Generic;
using System.Linq;
using WikiCore.DB;

namespace WikiCore.SearchHelpers
{
    public static class SearchHelper {
        public static List<SearchResult> Search(string text) {
            List<SearchResult> result = new List<SearchResult>();

            List<Page> findResults;
            using (var db = new WikiContext())
            {
                findResults = db.Pages.Where(x => x.Content.ToLower().Contains(text) || x.Title.ToLower().Contains(text)).ToList();
            }

            foreach (Page item in findResults) {
               result.Add(new SearchResult{ title = item.Title, url = "/Home/Index/" + item.Id, description = "asdasd"});                 
            }

            //search test data
            // result.Add(new SearchResult{ title = "test1", url = "google,de", description = "asdasd"});
            // result.Add(new SearchResult{ title = "testasdasd1", url = "google,de", description = "asdasd"});
            // result.Add(new SearchResult{ title = "tesasdasdt1", url = "google,de", description = "asdasd"});
            // result.Add(new SearchResult{ title = "tesasdasdasdasdt1", url = "google,de", description = "asdasd"});

            return result;
        }
    }

    public class SearchResult {
        public string title { get; set; }
        public string url { get; set; }
        public string description { get; set; }
    }
}