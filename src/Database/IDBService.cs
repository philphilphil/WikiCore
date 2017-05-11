using System.Collections.Generic;
using WikiCore.Models;

namespace WikiCore.DB
{
    public interface IDBService
    {
        int SavePage(EditModel model);
        void DeletePage(int id);
        List<Tag> GetAllTags();
        List<Page> GetPagesWithTag(Tag tag);
        List<Page> GetAllPages();
        Tag GetTagByName(string tagname);
        Page GetPageOrDefault(int id);
        void DeleteTag(int tagId);
        void UpdatePage(EditModel model);
        int GetTagWeight(Tag tag);
        List<Page> SearchPages(string searchText);
        void UpdateTags(int pageId, string tags);
        void RemoveTagReference(int pageId, string tag);
        string LoadTagsForPage(int pageId);
        void CreatePageTagReference(int tagId, int pageId);
        Tag GetTag(string name);
        bool SomeUserRegistered();
    }
}