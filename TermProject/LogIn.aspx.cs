using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace TermProject
{
    public partial class LogIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLoginSubmit_Click(object sender, EventArgs e)
        {
            string email = txtLogInEmail.Text;
            string password = txtLogInPassword.Text;

            bool trigger = false;

            txtLogInEmail.CssClass = txtLogInEmail.CssClass.Replace("is-invalid", "").Trim();
            txtLogInPassword.CssClass = txtLogInPassword.CssClass.Replace("is-invalid", "").Trim();
            if (email.Length <= 0)
            {
                txtLogInEmail.CssClass += " is-invalid";
                trigger = true;
            }
            if (password.Length <= 0)
            {
                txtLogInPassword.CssClass += " is-invalid";
                trigger = true;
            }

            if (trigger)
            {
                //Failed, do nothing
            }
            else
            {
                string sendAdd = "querydating@gmail.com";
                //Do something
                MailMessage msg = new MailMessage();
                msg.To.Add(new MailAddress(@email));
                msg.Subject = "QUERY Verification Email";
                msg.From = new MailAddress(sendAdd);
                msg.IsBodyHtml = true;
                msg.Body = "TEST";
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new System.Net.NetworkCredential(sendAdd, "CIS3342TermProject");
                smtp.EnableSsl = true;

                smtp.Send(msg);
            }
            Session["LoggedIn"] = "true";
            Response.Redirect("Dashboard.aspx");
        }
    }
}