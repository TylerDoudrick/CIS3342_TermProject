using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class User
    {
        public string heading { get; set; }
        public int userID { get; set; }
        public string name { get;set;}
        public string tagline { get; set; }

        public string imageSRC { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string gender { get; set; }
        public int age { get; set; }
        public string occuption { get; set; }

        public string emailAddress { get; set; }
        public string seekingGender { get; set; }
        public string token { get; set; }
    }
}
