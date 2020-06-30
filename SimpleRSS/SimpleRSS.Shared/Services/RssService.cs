using SimpleRSS.Models;
using SimpleRSS.Commons;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SimpleRSS.Services
{
    public static class RssService
    {
        /// <summary>
        /// Retrieves feed data from the server and updates the appropriate FeedViewModel properties.
        /// </summary>
        public static async Task<RSSChannel> GetFeedAsync(Uri rssLink)
        {
            try
            {
                using (HttpClient hc = new HttpClient())
                {
                    string result = await hc.GetStringAsync(rssLink);

                    RSSChannel feed = GetChannelFromString(result);
                    return feed;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public static RSSChannel GetChannelFromString(string xml)
        {
            var xdoc = new XmlDocument();
            xdoc.LoadXml(xml);

            var channel = xdoc.SelectSingleNode("rss/channel");
            var returnValue = new RSSChannel
            {
                Title = channel.SelectSingleNode("title").GetString(),
                Link = channel.SelectSingleNode("link").GetString(),
                Description = channel.SelectSingleNode("description").GetString(),
                Pubdate = channel.SelectSingleNode("pubDate").GetDateTime(),
                Items = new List<RSSItem>()
            };

            var items = xdoc.SelectNodes("rss/channel/item");
            foreach (XmlNode item in items)
            {
                returnValue.Items.Add(new RSSItem
                {
                    Title = item.SelectSingleNode("title").GetString(),
                    ItemLink = item.SelectSingleNode("link").GetString(),
                    Description = item.SelectSingleNode("description").GetString(),
                    Pubdate = item.SelectSingleNode("pubDate").GetDateTime()
                });
            }
            return returnValue;
        }
    }
}
