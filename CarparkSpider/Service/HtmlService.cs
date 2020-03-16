using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace CarparkSpider.Service
{
    public class HtmlService
    {
        public static string WebReq(string url, string queryStr, string method)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);

                if (method == "POST")
                {
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.Method = method;

                    if (!string.IsNullOrEmpty(queryStr))
                    {
                        var qbyte = Encoding.UTF8.GetBytes(queryStr);
                        request.ContentLength = qbyte.Length;
                        var writer = request.GetRequestStream();
                        writer.Write(qbyte, 0, qbyte.Length);
                        writer.Close();
                    }
                }
                var response = (HttpWebResponse)request.GetResponse();

                var s = response.GetResponseStream();
                string readerStr;
                var html = string.Empty;
                var reader = new StreamReader(s, Encoding.UTF8);
                while ((readerStr = reader.ReadLine()) != null)
                {
                    html += readerStr + "\r\n";
                }
                return html;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
