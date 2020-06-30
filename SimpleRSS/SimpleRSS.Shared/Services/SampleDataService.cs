using SimpleRSS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleRSS.Shared.Services
{
    public static class SampleDataService
    {
        public static IList<RSSItem> GetRss()
        {
            var item = new RSSItem
            {
                Pubdate = DateTime.Now,
                Title = "Getting started Uno",
                Description = "Setting up your development environment Prerequisites Windows 10 1809"
            };
            var rssies = new List<RSSItem>();
            for (int i = 0; i < 10; i++)
            {
                rssies.Add(item);
            }
            return rssies;
        }
    }
}
