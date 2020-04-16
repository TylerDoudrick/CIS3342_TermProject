using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TP_WebAPI.Controllers
{
    [Route("api/datingservice/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        [EnableCors("AllowOrigin")]
        [HttpGet("{id}")]
        public string getNotifications(string userID)
        {
            //Methodology:
            /*
            Reach out to the database, grab rows from the notification table for this user
            
            Exclude notifications that have already been served (or dismissed? ajax could do that)

            Serve the notifications back tothe AJAX with JSON
            */
            Random random = new Random();
            int num = random.Next(0, 100);
            if (num < 50) { return "New Messages"; } else
            {
                return "";
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
    }
}