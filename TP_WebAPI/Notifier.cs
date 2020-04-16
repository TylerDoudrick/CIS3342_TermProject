using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TermProject;

namespace TP_WebAPI
{
    public class Notifier
    {
        DBConnect databaseObj = new DBConnect();
        SqlCommand commandObj = new SqlCommand();
        public Notifier()
        {

        }

        public void NotifyMessage(string userId)
        {
            //Add record to Notifications table with userId and some kind of description 
            //Type: Message
        }

        public void NotifyDate(string userId)
        {
            //Add record to the Notifications table with userId and some kind of description
            //Type: Date
        }
        
    }
}
