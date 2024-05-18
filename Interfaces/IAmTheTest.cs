using HtmlAgilityPack;

namespace BotScraper.Interfaces
{
    interface IAmTheTest
    {
        List<string> GetEmailsInPageAndChildPages(HtmlDocument html, string url);
    }
}