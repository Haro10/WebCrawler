using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;

namespace WebCrawler
{
    class Program
    {
        static bool ReturnTrue()
        {
            Console.WriteLine("ReturnTrue");
            return true;
        }

        static bool ReturnFalse()
        {
            Console.WriteLine("ReturnFalse");
            return false;
        }

        static void Main(string[] args)
        {
            HtmlWeb htmlWeb = new HtmlWeb()
            {
                AutoDetectEncoding = false,
                OverrideEncoding = Encoding.UTF8  //Set UTF8 để hiển thị tiếng Việt
            };

            //Load trang web, nạp html vào document
            HtmlDocument document = htmlWeb.Load("http://www.webtretho.com/forum/f26/");

            //Load các tag li trong tag ul
            //  var threadItems = document.DocumentNode.SelectNodes("//ul[@id='threads']/li").ToList();

            var items = new List<object>();

            /*Get Nodes By Xpath*/

            //            foreach (var item in threadItems)
            //            {
            //                //Extract các giá trị từ các tag con của tag li
            //                var linkNode = item.SelectSingleNode(".//a[contains(@class,'title')]");
            //                var link = linkNode.Attributes["href"].Value;
            //                var text = linkNode.InnerText;
            //                var readCount = item.SelectSingleNode(".//div[@class='folTypPost']/ul/li/b").InnerText;
            //
            //                items.Add(new { text, readCount, link });
            //            }


            /*Get node by LINQ*/
            //            var threadItems = document.DocumentNode.Descendants("ul")
            //                .First(node => node.Attributes.Contains("id") && node.Attributes["id"].Value == "threads")
            //                .ChildNodes.Where(node => node.Name == "li").ToList();
            //
            //            foreach (var item in threadItems)
            //            {
            //                var linkNode = item.Descendants("a").First(node =>
            //                node.Attributes.Contains("class") && node.Attributes["class"].Value.Contains("title"));
            //                var link = linkNode.Attributes["href"].Value;
            //                var text = linkNode.InnerText;
            //                var readCount = item.Descendants("b").First().InnerText;
            //
            //                items.Add(new { text, readCount, link });
            //            }


            /*Get Nodes by CSS syntax*/
            var threadItems = document.DocumentNode.QuerySelectorAll("ul#threads > li").ToList();

            foreach (var item in threadItems)
            {
                var linkNode = item.QuerySelector("a.title");
                var link = linkNode.Attributes["href"].Value;
                var text = linkNode.InnerText;
                var readCount = item.QuerySelector("div.folTypPost > ul > li > b").InnerText;

                items.Add(new { link, text, readCount });
            }
        }
    }
}
