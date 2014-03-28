using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Display
{
    class WebScrap
    {
        public WebScrap()
        {
            WebClient w = new WebClient();
            String s = w.DownloadString("http://en.wikipedia.org/");
            Regex x = new Regex("<[^>]*>");
            //String target = s.replaceAll("(?i)<td[^>]*>", " ").replaceAll("\\s+", " ").trim();
            String ss = x.Replace(s, " ");

            using (StreamWriter writer = new StreamWriter("webOutput.txt"))
            {
                writer.WriteLine(ss);
            }
        }

        public WebScrap(String URL)
        {
            WebClient w = new WebClient();
            String s = w.DownloadString(URL);
            Regex x = new Regex("<[^>]*>");
            //String target = s.replaceAll("(?i)<td[^>]*>", " ").replaceAll("\\s+", " ").trim();
            String ss = x.Replace(s, " ");

            using (StreamWriter writer = new StreamWriter("webOutput.txt"))
            {
                writer.WriteLine(ss);
            }
        }

    }
}
