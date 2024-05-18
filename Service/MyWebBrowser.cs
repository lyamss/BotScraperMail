using BotScraper.Interfaces;
using HtmlAgilityPack;

namespace BotScraper.Service
{
    public class MyWebBrowser : IWebBrowser
    {
        public HtmlDocument GetHtml(string url)
        {
            var web = new HtmlWeb();
            var page = web.Load(url);
            return page;
        }
    }
}