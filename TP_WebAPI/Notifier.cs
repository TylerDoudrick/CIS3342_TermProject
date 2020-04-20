using System;
using System.Collections.Generic;
using System.Data;
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

        public void NotifyMessage(int userID, string message)
        {
            DBConnect databaseObj = new DBConnect();
            SqlCommand commandObj = new SqlCommand();


            commandObj.CommandType = CommandType.StoredProcedure;
            commandObj.CommandText = "TP_CreateNotification";
            commandObj.Parameters.AddWithValue("@userID", userID);
            commandObj.Parameters.AddWithValue("@message", message);
            commandObj.Parameters.AddWithValue("@type", 2);

            if(databaseObj.DoUpdateUsingCmdObj(commandObj, out string erro) == -2)
            {
                throw new Exception(erro);
            }
            //Add record to Notifications table with userId and some kind of description 
            //Type: Message
        }

        public void NotifyDate(int userID, string message)
        {
            DBConnect databaseObj = new DBConnect();
            SqlCommand commandObj = new SqlCommand();


            commandObj.CommandType = CommandType.StoredProcedure;
            commandObj.CommandText = "TP_CreateNotification";
            commandObj.Parameters.AddWithValue("@userID", userID);
            commandObj.Parameters.AddWithValue("@message", message);
            commandObj.Parameters.AddWithValue("@type", 1);

            if (databaseObj.DoUpdateUsingCmdObj(commandObj, out string erro) == -2)
            {
                throw new Exception(erro);
            }
            //Add record to the Notifications table with userId and some kind of description
            //Type: Date
        }
        
    }
}
