using BotScraper.Interfaces;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace BotScraper.Service
{
    public class MyEmailScraper : IAmTheTest
    {
        private readonly string _emailsFile;

        public static Regex urlRegex = new Regex(@"^https?:\/\/[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)$", RegexOptions.Compiled);

        private static Regex emailRegex = new Regex(@"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}\b", RegexOptions.Compiled);

        private static HashSet<string> VisitedUrls = new HashSet<string>();

        private readonly HashSet<string> _emailsHashSet;

        public MyEmailScraper(string emailsFile)
        {
            _emailsFile = emailsFile;
            _emailsHashSet = new HashSet<string>();
            if (File.Exists(_emailsFile))
            {
                foreach (string line in File.ReadLines(_emailsFile))
                {
                    _emailsHashSet.Add(line);
                }
            }
        }

        public List<string> GetEmailsInPageAndChildPages(HtmlDocument html, string url)
        {
            if (VisitedUrls.Contains(url))
            {
                return new List<string>();
            }

            VisitedUrls.Add(url);

            var emailsInPage = emailRegex.Matches(html.ToString());
            foreach (Match email in emailsInPage)
            {
                string emailValue = email.Value;
                if (!_emailsHashSet.Contains(emailValue))
                {
                    _emailsHashSet.Add(emailValue);
                    File.AppendAllText(_emailsFile, emailValue + Environment.NewLine);
                }
            }

            var linkNodes = html.DocumentNode.SelectNodes("//a[@href]");
            Console.WriteLine($"Processed {linkNodes.Count} links on {url}");
            if (linkNodes != null)
            {
                foreach (var link in linkNodes)
                {
                    var linkHref = link.Attributes["href"].Value;
                    if (linkHref.StartsWith("mailto:", StringComparison.OrdinalIgnoreCase))
                    {
                        var emailAddress = linkHref.Substring("mailto:".Length);
                        if (!_emailsHashSet.Contains(emailAddress))
                        {
                            _emailsHashSet.Add(emailAddress);
                            File.AppendAllText(_emailsFile, emailAddress + Environment.NewLine);
                        }
                    }
                    else if (urlRegex.IsMatch(linkHref))
                    {
                        GetEmailsInPageAndChildPages(html, linkHref);
                    }
                }
            }
            return new List<string>();
        }
    }
}