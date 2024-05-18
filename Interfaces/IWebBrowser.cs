using HtmlAgilityPack;

namespace BotScraper.Interfaces
{
    interface IWebBrowser
    {
        // Returns null if the url could not be visited.
        HtmlDocument GetHtml(string url);
    }
}