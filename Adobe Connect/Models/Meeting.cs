using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adobe_Connect.Models
{
    class Meeting
    {
        public string Name { get; }
        public string Description { get; }
        public string ScoId { get; }
        public string Type { get;  }
        public string URL { get;  }
        public DateTime Start { get; }
        public DateTime End { get; }

        public Meeting(string name, string description, string scoID, string type, string url, DateTime begin, DateTime end)
        {
            Name = name;
            Description = description;
            ScoId = scoID;
            Type = type;
            URL = url;
            Start = begin;
            End = end;
            
        }
    }




   
}
