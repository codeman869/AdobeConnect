using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Xml;

namespace Adobe_Connect.Services
{
    class AdobeConnectService
    {
        static HttpClient client;

        static AdobeConnectService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://cityofport.adobeconnect.com");
        }

        public static async Task<bool> Login(string username, string password)
        {

            var query = new FormUrlEncodedContent(new Dictionary<string, string>() {

                {"action", "login" },
                {"login", username },
                {"password", password }

            }).ReadAsStringAsync().Result;



            HttpResponseMessage response = await client.GetAsync("/api/xml?" + query);

            if (!response.IsSuccessStatusCode) return false;


            Console.WriteLine("Logged In!");


            var responseString = await response.Content.ReadAsStringAsync();

            Console.WriteLine(responseString);


            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(responseString);

            string loggedIn = xdoc.SelectSingleNode("results/status").Attributes["code"].Value;

            Console.WriteLine(loggedIn);



            if (loggedIn == "ok") return true;

            return false;

        }

        public static async Task<List<Models.Meeting>> GetMeetings()
        {
            List <Models.Meeting> meetings = new List<Models.Meeting>();

            var query = new FormUrlEncodedContent(new Dictionary<string, string>() {

                {"action", "sco-contents" },
                {"sco-id", "2668004808"  },

            }).ReadAsStringAsync().Result;

            HttpResponseMessage response = await client.GetAsync("/api/xml?" + query);

            if (!response.IsSuccessStatusCode) return null;

            XmlDocument xdoc = new XmlDocument();

            string responseString = await response.Content.ReadAsStringAsync();

            xdoc.LoadXml(responseString);

            XmlNodeList nodes = xdoc.SelectNodes("results/scos/sco");

            foreach(XmlNode node in nodes)
            {
                meetings.Add(SerializeMeeting(node));

            }

            return meetings;

        }

        static Models.Meeting SerializeMeeting(XmlNode meetingInfo)
        {
            //Models.Meeting meeting = new Models.Meeting();

            string scoId = meetingInfo.Attributes["sco-id"].Value;
            string type = meetingInfo.Attributes["icon"].Value;

            string name = meetingInfo.SelectSingleNode("name").InnerText;
            string description = meetingInfo.SelectSingleNode("description").InnerText;
            string url = meetingInfo.SelectSingleNode("url-path").InnerText;

            string begin = meetingInfo.SelectSingleNode("date-begin").InnerText;
            string end = meetingInfo.SelectSingleNode("date-end").InnerText;

            DateTime beginDateTime = DateTime.Parse(begin);
            DateTime endDateTime = DateTime.Parse(end);

            return new Models.Meeting(name, description, scoId, type, url, beginDateTime, endDateTime);
        }
    }
}
