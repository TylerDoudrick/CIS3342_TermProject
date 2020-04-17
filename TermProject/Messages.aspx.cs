using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TermProject
{
    public partial class Messages : System.Web.UI.Page
    {
        string interactionsWebAPI = "https://localhost:44375/api/datingservice/interactions/";
        string profileWebAPI = "https://localhost:44375/api/datingservice/profile/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null) Response.Redirect("LogIn.aspx?target=Messages");
            /*
             *   string email =Session["email"].ToString();
            string sendAdd = "querydating@gmail.com";
            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(@email));
            msg.Subject = "QUERY Verification Email";
            msg.From = new MailAddress(sendAdd);
            msg.IsBodyHtml = true;
            msg.Body = "<div> You got a new message! <br><BR> Sign into your account to view the message of your admirer!" +
                "<Br><BR> <div>";
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new System.Net.NetworkCredential(sendAdd, "CIS3342TermProject");
            smtp.EnableSsl = true;

            smtp.Send(msg);
            Session["email"] = email;

             * */
        }

        protected void showMessage(object sender, EventArgs e)
        {
            divMessageList.Visible = false;
            divMessageListControls.Visible = false;
            divViewMessageControls.Visible = true;
            divViewMessage.Visible = true;
        }
        protected void ViewMessageList(object sender, EventArgs e)
        {
            divMessageList.Visible = true;
            divMessageListControls.Visible = true;
            divViewMessageControls.Visible = false;
            divViewMessage.Visible = false;
        }
    }
}