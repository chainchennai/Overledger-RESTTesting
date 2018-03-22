using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace RESTTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    System.Console.WriteLine("Enter request URL:");
                    string url = System.Console.ReadLine();
                    System.Console.WriteLine("\n(1) GET\n(2) POST");
                    string method = System.Console.ReadLine();
                    string contentFileName = null;
                    if (method == "2")
                    {
                        System.Console.WriteLine("Enter content filename : ");
                        contentFileName = System.Console.ReadLine();
                    }
                    HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
                    req.ContentType = "text/plain";
                    if (method == "2")
                    {
                        req.ContentType = "text/xml";
                        req.Method = "POST";
                        StreamReader reader = new StreamReader(File.OpenRead(contentFileName));
                        Stream reqStream = req.GetRequestStream();
                        byte[] fileToSend = System.Text.Encoding.UTF8.GetBytes(reader.ReadToEnd());
                        reader.Close();
                        reqStream.Write(fileToSend, 0, fileToSend.Length);
                        reqStream.Close();
                    }
                    else
                    {
                        req.Method = "GET";
                    }


                    HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                    var sr = new StreamReader(resp.GetResponseStream());
                    Console.WriteLine(sr.ReadToEnd());
                }
                catch (Exception e)
                {
                    System.Console.WriteLine(e.Message);
                }
            }

        }
    }
}
