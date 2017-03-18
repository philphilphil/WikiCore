using WikiCore.DB;
using WikiCore.Models;

namespace Tests
{
    public static class TestDataCreator
    {
        public static void CreateFourPagesWithTags(WikiContext context)
        {
            CreatePage("title", "eins zwei drei vier fuenf sechs sieben acht neun zehn elf zwoelf dreizehn test vierzehn fuenfzehn sechzehn siebzehn achtzehn neunzehn zwanzig", "eins, zwei, drei, vier", context);
            CreatePage("title2", "eins zwei drei vier fuenf sechs sieben acht neun zehn elf zwoelf dreizehn test vierzehn fuenfzehn sechzehn siebzehn achtzehn neunzehn zwanzig", "eins, zwei, drei, vier", context);
            CreatePage("title3", "eins zwei drei vier fuenf sechs sieben acht neun zehn elf zwoelf dreizehn test vierzehn fuenfzehn sechzehn siebzehn achtzehn neunzehn zwanzig", "eins, zwei, drei, vier", context);
            CreatePage("title4", "eins zwei drei vier fuenf sechs sieben acht neun zehn elf zwoelf dreizehn NOOO vierzehn fuenfzehn sechzehn siebzehn achtzehn neunzehn zwanzig", "eins, zwei, drei, vier", context);
        }

        private static void CreatePage(string title, string text, string tags, WikiContext context)
        {
            var dbs = new DBService(context);
            EditModel m = new EditModel();
            m.Tags = tags;
            m.Title = title;
            m.pageContent = text;
            dbs.SavePage(m);
        }
    }
}