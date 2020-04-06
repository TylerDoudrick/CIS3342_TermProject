using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TermProject
{
    public partial class Verification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["email"] == null) Response.Redirect("Default.aspx");
        }

        protected void lbSendAgain_Click(object sender, EventArgs e)
        { // sends email again
            string email = Session["email"].ToString(); string sendAdd = "querydating@gmail.com";
            string link = Request.Url.Host+"'/Registration.aspx'";
            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(@email));
            msg.Subject = "QUERY Verification Email";
            msg.From = new MailAddress(sendAdd);
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.Normal;
            msg.Body = "<div> Thank you for signing up for Query! <Br><BR> Please click the below link to verify your account status." +
                "<Br><BR> <a href=" + link + "> Click here! </a>  </div>";
            SmtpClient smtp = new SmtpClient("smtp.temple.edu");
            smtp.EnableSsl = true;

            smtp.Send(msg);

        }
    }
}