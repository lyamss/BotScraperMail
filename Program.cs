using BotScraper.Service;
using HtmlAgilityPack;


namespace BotScraper
{
    public class Program
    {
        public static void Main(string[] args)
        {

            if (args.Length == 0 || args[0] is null)
            {
                throw new Exception("Args is null");
            }

            string URL = args[0];

            if (!MyEmailScraper.urlRegex.IsMatch(URL))
            {
                throw new Exception("URL is not valid");
            }

            ArtASCII.ShowASCII();

            string emailsFile = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\emails.txt";

            var page = new MyWebBrowser();
            HtmlDocument html = page.GetHtml(URL);

            new MyEmailScraper(emailsFile).GetEmailsInPageAndChildPages(html, URL);

            Console.ResetColor();
        }
    }
}