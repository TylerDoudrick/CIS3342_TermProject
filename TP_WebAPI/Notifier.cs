using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
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
            commandObj.Parameters.Add("@Email", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
            commandObj.Parameters.Add("@SenderName", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;


            if (databaseObj.DoUpdateUsingCmdObj(commandObj, out string erro) == -2)
            {
                throw new Exception(erro);
            }

            // send email
            string email = commandObj.Parameters["@Email"].Value.ToString();
            string sender = commandObj.Parameters["@SenderName"].Value.ToString();
            string sendAdd = "querydating@gmail.com";
            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(@email));
            msg.Subject = "QUERY New Message";
            msg.From = new MailAddress(sendAdd);
            msg.IsBodyHtml = true;
            msg.Body = "<div> You have a new message from "+ sender +"!<Br><BR> <div>";
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new System.Net.NetworkCredential(sendAdd, "CIS3342TermProject");
            smtp.EnableSsl = true;


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

            // send email
            string email = commandObj.Parameters["@Email"].Value.ToString();
            string sender = commandObj.Parameters["@SenderName"].Value.ToString();
            string sendAdd = "querydating@gmail.com";
            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(@email));
            msg.Subject = "QUERY New Message";
            msg.From = new MailAddress(sendAdd);
            msg.IsBodyHtml = true;
            msg.Body = "<div> You have a new date notification from " + sender + "!<Br><BR> <div>";
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new System.Net.NetworkCredential(sendAdd, "CIS3342TermProject");
            smtp.EnableSsl = true;
        }
        
    }
}
