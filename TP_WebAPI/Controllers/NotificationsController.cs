using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TermProject;

namespace TP_WebAPI.Controllers
{
    [Route("api/datingservice/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        [EnableCors("AllowOrigin")]
        [HttpGet("{userID}")]
        public List<Notification> getNotifications(int userID)
        {
            //Methodology:
            /*
            Reach out to the database, grab rows from the notification table for this user
            
            Exclude notifications that have already been served (or dismissed? ajax could do that)

            Serve the notifications back tothe AJAX with JSON
            */
            List<Notification> notifications = new List<Notification>();

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
                foreach(DataRow row in res.Tables[0].Rows)
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

        [HttpPost("dismiss/{id}")]
        public void dismissNotification(string userID, [FromBody] string notificationID)
        {
            //Methodology:
            /*
             * 
              Reach out to the notifications table and update the record defined by the id
              to note that record as "dismissed" or some shit so that it doesn't get served again
             
             */
        }

        [HttpPost("dismiss/all/{id}")]
        public void dismissAllNotifications(string userID)
        {
            //See above but do more.
        }

        public class Notification{
            public string notificationID { get; set; }
            public string notificationMessage { get; set; }
            public string notificationType { get; set; }
        }
    }
}