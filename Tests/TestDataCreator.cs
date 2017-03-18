using System;
using System.Collections.Generic;
using System.Linq;
using WikiCore.DB;
using WikiCore.Models;

namespace Tests
{
    public static class TestDataCreator
    {

        //creating this by hand w/o dbservice on purpose
        public static void CreateFourPagesWithTags(WikiContext context)
        {
            CreatePage("title", "eins zwei drei vier fuenf sechs sieben acht neun zehn elf zwoelf dreizehn test vierzehn fuenfzehn sechzehn siebzehn achtzehn neunzehn zwanzig", context);
            CreatePage("title2", "eins zwei drei vier fuenf sechs sieben acht neun zehn elf zwoelf dreizehn test vierzehn fuenfzehn sechzehn siebzehn achtzehn neunzehn zwanzig", context);
            CreatePage("title3", "eins zwei drei vier fuenf sechs sieben acht neun zehn elf zwoelf dreizehn test vierzehn fuenfzehn sechzehn siebzehn achtzehn neunzehn zwanzig", context);
            CreatePage("title4", "eins zwei drei vier fuenf sechs sieben acht neun zehn elf zwoelf dreizehn NOOO vierzehn fuenfzehn sechzehn siebzehn achtzehn neunzehn zwanzig", context);

            CreateTag("eins", context);
            CreateTag("zwei", context);
            CreateTag("drei", context);
            CreateTag("vier", context);

            CreatePageTagRef(1, 1, context);
            CreatePageTagRef(1, 2, context);
            CreatePageTagRef(1, 3, context);
            CreatePageTagRef(1, 4, context);

            CreatePageTagRef(2, 1, context);
            CreatePageTagRef(2, 2, context);
            CreatePageTagRef(2, 3, context);
            CreatePageTagRef(2, 4, context);

            CreatePageTagRef(3, 1, context);
            CreatePageTagRef(3, 2, context);
            CreatePageTagRef(3, 3, context);
            CreatePageTagRef(3, 4, context);

            CreatePageTagRef(4, 1, context);
            CreatePageTagRef(4, 2, context);
            CreatePageTagRef(4, 3, context);
            CreatePageTagRef(4, 4, context);

            context.SaveChanges();
        }

        private static void CreatePageTagRef(int pId, int tId, WikiContext context)
        {
            PageTag pt = new PageTag();
            pt.PageId = pId;
            pt.TagId = tId;
            context.PageTags.Add(pt);
            context.SaveChanges();
        }

        private static void CreateTag(string v, WikiContext context)
        {
            Tag t = new Tag();
            t.Name = v;
            context.Tags.Add(t);
            context.SaveChanges();
        }

        private static void CreatePage(string title, string text, WikiContext context)
        {
            Page p = new Page();
            p.Title = title;
            p.Content = text;
            context.Pages.Add(p);
            context.SaveChanges();
        }
    }
}