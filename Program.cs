using BotScraper.Interfaces;
using BotScraper.Service;
using HtmlAgilityPack;


namespace BotScarper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ArtASCII.ShowASCII();

            string URL = args[0];

            if (!MyEmailScraper.urlRegex.IsMatch(URL))
            {
                throw new Exception("URL is not valid");
            }

            string emailsFile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads\emails.txt";

            var page = new MyWebBrowser();
            HtmlDocument html  = page.GetHtml(URL);

            IAmTheTest emailScraper = new MyEmailScraper(emailsFile);
            emailScraper.GetEmailsInPageAndChildPages(html, URL);
        }
    }
}