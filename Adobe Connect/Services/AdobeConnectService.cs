using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Xml;
using Adobe_Connect.Models;

namespace Adobe_Connect.Services
{
    class AdobeConnectService
    {
        static HttpClient client;
        const string path = "/api/xml?";

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



            HttpResponseMessage response = await client.GetAsync(path + query);

            if (!response.IsSuccessStatusCode) return false;


            var responseString = await response.Content.ReadAsStringAsync();

            var responseStatus = CheckResponseStatus(responseString);


            if (responseStatus) return true;

            return false;

        }

        public static async Task<List<Meeting>> GetMeetings()
        {
            List <Meeting> meetings = new List<Meeting>();

            var query = new FormUrlEncodedContent(new Dictionary<string, string>() {

                {"action", "sco-contents" },
                {"sco-id", "2668004808"  },
                


            }).ReadAsStringAsync().Result;

            HttpResponseMessage response = await client.GetAsync(path + query);

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

        public static async Task<bool> UpdateMeeting(Meeting meeting)
        {

            string meetingtype = meeting.Type == Meeting.MeetingTypes.meeting ? "meeting" : "virtual-classroom";

            var query = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                {"action", "sco-update"},
                {"sco-id", meeting.ScoId },
                {"icon", meetingtype },
                {"source-sco-id", "2654344692" }
            }).ReadAsStringAsync().Result;

            HttpResponseMessage response = await client.GetAsync(path + query);

            if (!response.IsSuccessStatusCode) return false;

            

            string responseString = await response.Content.ReadAsStringAsync();


            var responseStatus = CheckResponseStatus(responseString);

            

            if (responseStatus) return true;

            return false;
        }

        static Meeting SerializeMeeting(XmlNode meetingInfo)
        {

            string scoId = meetingInfo.Attributes["sco-id"].Value;
            string type = meetingInfo.Attributes["icon"].Value;

            string name = meetingInfo.SelectSingleNode("name").InnerText;
            string description = meetingInfo.SelectSingleNode("description").InnerText;
            string url = meetingInfo.SelectSingleNode("url-path").InnerText;

            string begin = meetingInfo.SelectSingleNode("date-begin").InnerText;
            string end = meetingInfo.SelectSingleNode("date-end").InnerText;

            DateTime beginDateTime = DateTime.Parse(begin);
            DateTime endDateTime = DateTime.Parse(end);

            return new Meeting(name, description, scoId, type, url, beginDateTime, endDateTime);
        }

        static bool CheckResponseStatus(string response)
        {
            XmlDocument xdoc = new XmlDocument();

            xdoc.LoadXml(response);
            string success = xdoc.SelectSingleNode("results/status").Attributes["code"].Value;

            if (success == "ok") return true;

            return false;
        }
    }
}
