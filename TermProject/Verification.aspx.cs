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

        }

        protected void lbSendAgain_Click(object sender, EventArgs e)
        { // sends email again
            string email = Session["email"].ToString(); string sendAdd = "querydating@gmail.com";

            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(@email));
            msg.Subject = "QUERY Verification Email";
            msg.From = new MailAddress(sendAdd);
            msg.IsBodyHtml = true;
            msg.Body = "<div> Thank you for signing up for Query.com! <Br><BR> Please click the below link to verify your account status." +
                "<Br><BR> <div>";
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new System.Net.NetworkCredential(sendAdd, "CIS3342TermProject");
            smtp.EnableSsl = true;

            smtp.Send(msg);

        }
    }
}