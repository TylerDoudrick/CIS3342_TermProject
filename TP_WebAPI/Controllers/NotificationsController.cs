using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TermProject;

namespace TP_WebAPI.Controllers
{
    [Authorize]
    [EnableCors("AllowOrigin")]
    [Route("api/datingservice/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        [HttpGet("{userID}")]
        public List<Notification> getNotifications(int userID)
        {
            //Return the list of notifications
            List<Notification> notifications = new List<Notification>();
            try
            {
                DBConnect databaseObj = new DBConnect();
                SqlCommand commandObj = new SqlCommand();

                commandObj.CommandType = CommandType.StoredProcedure;
                commandObj.CommandText = "TP_GetUserNotifications";
                commandObj.Parameters.AddWithValue("@UserID", userID);

                DataSet res = databaseObj.GetDataSetUsingCmdObj(commandObj);

                if (res.Tables[0].Rows.Count == 0)
                {
                    return null;
                }
                else
                {
                    foreach (DataRow row in res.Tables[0].Rows)
                    {
                        Notification temp = new Notification();
                        temp.notificationMessage = row["notificationMessage"].ToString();
                        temp.notificationType = row["notificationType"].ToString();
                        temp.notificationID = row["notificationID"].ToString();
                        notifications.Add(temp);
                    }
                    return notifications;
                }
            }
            catch
            {
                return null;
            }
        }

        [HttpDelete("dismiss")]
        public Response dismissNotification([FromBody] Notification notification)
        {
            try { 
            //Dismiss the notification given the userid and the notificationid
            Response response = new Response();

            DBConnect databaseObj = new DBConnect();
            SqlCommand commandObj = new SqlCommand();

            commandObj.CommandType = CommandType.StoredProcedure;
            commandObj.CommandText = "TP_DismissNotification";
            commandObj.Parameters.AddWithValue("@UserID", notification.userID);
            commandObj.Parameters.AddWithValue("@NotificationID", notification.notificationID);

            if(databaseObj.DoUpdateUsingCmdObj(commandObj, out string err) == -2){
                response.result = "fail";
                response.message = err;
            }
            else
            {
                response.result = "success";
            }

            return response;
            }
            catch
            {
                return null;
            }
        }

        [HttpDelete("dismiss/messages/{userID}")]
        public Response dismissAllMessages(string userID)
        {
            try { 
            //Blanket dismiss all message notifications when the user visits the message page
            Response response = new Response();

            DBConnect databaseObj = new DBConnect();
            SqlCommand commandObj = new SqlCommand();

            commandObj.CommandType = CommandType.StoredProcedure;
            commandObj.CommandText = "TP_DismissMessageNotifications";
            commandObj.Parameters.AddWithValue("@UserID", userID);

            if (databaseObj.DoUpdateUsingCmdObj(commandObj, out string err) == -2)
            {
                response.result = "fail";
                response.message = err;
            }
            else
            {
                response.result = "success";
            }

            return response;
            }
            catch
            {
                return null;
            }
        }

        [HttpDelete("dismiss/dates/{userID}")]
        public Response dismissAllDates(string userID)
        {
            try { 
            //Blanket dismiss all date notifications when the user visits the dates page

            Response response = new Response();

            DBConnect databaseObj = new DBConnect();
            SqlCommand commandObj = new SqlCommand();

            commandObj.CommandType = CommandType.StoredProcedure;
            commandObj.CommandText = "TP_DismissDateNotifications";
            commandObj.Parameters.AddWithValue("@UserID", userID);

            if (databaseObj.DoUpdateUsingCmdObj(commandObj, out string err) == -2)
            {
                response.result = "fail";
                response.message = err;
            }
            else
            {
                response.result = "success";
            }

            return response;
            }
            catch
            {
                return null;
            }
        }

        public class Notification{
            public string notificationID { get; set; }
            public string notificationMessage { get; set; }
            public string notificationType { get; set; }
            public string userID { get; set; }
        }

        public class Response
        {
            public string result { get; set; }
            public string message { get; set; }
        }
    }
}