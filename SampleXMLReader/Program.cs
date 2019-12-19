using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SampleXMLReader
{
    class Program
    {
        static void Main(string[] args)
        {
            var jobList = JobItems();
            foreach (var job in jobList)
            {
                Console.WriteLine("Title:" + job.Title);
                Console.WriteLine("Guid:" + job.Guid);
                Console.WriteLine("Description:" + job.Description);
                Console.WriteLine("Company:" + job.JobCompany);
                Console.WriteLine("Location:" + job.JobLocation);
                Console.WriteLine("Postion:" + job.JobPosition);
                Console.WriteLine("Link:" + job.Link);
                Console.WriteLine("PubDate:" + job.PubDate);
                Console.WriteLine("************************************");
               
            }
        }

        private static List<JobItem> JobItems()
        {
            //the sample xml is given in the project is for reference only
            string feedUrl = "https://your-link-to-the-xml/rss/";
            List<JobItem> JobItemList = new List<JobItem>();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(feedUrl.ToString());
            request.Method = "GET";
            request.KeepAlive = true;

            HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse();
            if (webResponse != null)
            { //Xml response read from a stream; 
                StreamReader responseStream = new StreamReader(webResponse.GetResponseStream());
                //get the response 
                string webResponseStream = responseStream.ReadToEnd();
                XmlDocument feedXML = new XmlDocument();
                feedXML.LoadXml(webResponseStream);

                string basePath = @"rss/channel"; //basenode
                XmlNode baseNode = feedXML.SelectSingleNode(basePath);
                foreach (XmlNode item in baseNode.ChildNodes)
                {
                    if (item.Name == "item")
                    {
                        JobItem pod = new JobItem();
                        foreach (XmlNode xmlNode in item)
                        {
                            if (xmlNode.Name == "title")
                            {
                                pod.Title = xmlNode.InnerText.Replace("�", "");
                            }
                            if (xmlNode.Name == "pubDate")
                            {
                                pod.PubDate = DateTime.Parse(xmlNode.InnerText.Replace(" CST", ""));
                            }
                            if (xmlNode.Name == "guid")
                            {
                                pod.Guid = xmlNode.InnerText;
                            }
                            if (xmlNode.Name == "jobs:position")
                            {
                                pod.JobPosition = xmlNode.InnerText;
                            }
                            if (xmlNode.Name == "description")
                            {
                                pod.Description = xmlNode.InnerText;
                            }
                            if (xmlNode.Name == "jobs:company")
                            {
                                pod.JobCompany = xmlNode.InnerText;
                            }
                            if (xmlNode.Name == "jobs:location")
                            {
                                pod.JobLocation = xmlNode.InnerText;
                            }
                            if (xmlNode.Name == "link")
                            {
                                pod.Link = xmlNode.InnerText;
                            }
                        }
                        JobItemList.Add(pod);
                    }
                }

            }

            return JobItemList;
        }
    }
}
