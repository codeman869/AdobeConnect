using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Adobe_Connect.Models
{
    class Meeting : INotifyPropertyChanged
    {
        public enum MeetingTypes
        {
            meeting,
            virtual_classroom
        };

        private MeetingTypes _type;
        public string Name { get; }
        public string Description { get; }
        public string ScoId { get; }
        public string URL { get;  }
        public DateTime Start { get; }
        public DateTime End { get; }


        public MeetingTypes Type {
            get { return _type; }
            set
            {
                if (_type == value)
                    return;
                _type = value;
                NotifyPropertyChanged();
            }
        }

        public Meeting(string name, string description, string scoID, string type, string url, DateTime begin, DateTime end)
        {
            Name = name;
            Description = description;
            ScoId = scoID;
            
            URL = url;
            Start = begin;
            End = end;
            
            if(type == "meeting")
            {
                _type = MeetingTypes.meeting;
            } else
            {
                _type = MeetingTypes.virtual_classroom;
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }




   
}
