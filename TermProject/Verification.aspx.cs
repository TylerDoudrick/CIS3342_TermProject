using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TermProject
{
    public partial class Verification : System.Web.UI.Page
    {
        string authWebAPI = "https://localhost:44375/api/datingservice/authentication/";
        string interactionsWebAPI = "https://localhost:44375/api/datingservice/interactions/";
        string profileWebAPI = "https://localhost:44375/api/datingservice/profile/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["email"] == null) Response.Redirect("Default.aspx");
            lblEmail.Text = Session["email"].ToString();
        }

        protected void lbSendAgain_Click(object sender, EventArgs e)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] random = new byte[16];
            rng.GetBytes(random);

            string rngString = Convert.ToBase64String(random);
            string trimmed = rngString.Substring(0, rngString.Length - 2);
            try {
                SqlCommand commandObj = new SqlCommand();
                commandObj.Parameters.Clear();
                commandObj.CommandType = CommandType.StoredProcedure;
                commandObj.CommandText = "TP_InsertVerification";

                commandObj.Parameters.AddWithValue("@UserID", Session["VerifyingUserID"].ToString());
                commandObj.Parameters.AddWithValue("@code", trimmed);


                DBConnect OBJ = new DBConnect();
                if (OBJ.DoUpdateUsingCmdObj(commandObj, out string err) != -2)
                {

                    // sends email again
                    string email = Session["email"].ToString(); string sendAdd = "querydating@gmail.com";
                    string link = Request.Url.Host + "'/Verification.aspx'";
                    // send email
                    MailMessage msg = new MailMessage();
                    msg.To.Add(new MailAddress(@email));
                    msg.Subject = "QUERY Welcome Email";
                    msg.From = new MailAddress(sendAdd);
                    msg.IsBodyHtml = true;
                    msg.Body = "<div> Thank you for signing up for Query.com! <Br><BR> You have successfully " +
                        "created an account. To verify, please enter the verification code: <strong>" + trimmed +
                        "</strong><Br><BR> <div>";
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

                    smtp.Credentials = new System.Net.NetworkCredential(sendAdd, "CIS3342TermProject");
                    smtp.EnableSsl = true;

                    smtp.Send(msg);
                    Response.Redirect("Verification.aspx");

                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "FailureToast", "showDBError();", true);

            }
        }

        protected void btnSubmitVerification_Click(object sender, EventArgs e)
        {
            string code = txtVerificationCode.Text;
            string email = lblEmail.Text;

            VerificationCredentials cred = new VerificationCredentials();
            cred.email = email;
            cred.code = code;
            WebRequest request = WebRequest.Create(authWebAPI+"verify");
            request.Method = "POST";
            JavaScriptSerializer json = new JavaScriptSerializer();
            string jsonCred = json.Serialize(cred);
            byte[] postData = Encoding.ASCII.GetBytes(jsonCred);
            request.ContentType = "application/json";
            request.ContentLength = postData.Length;

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(postData, 0, postData.Length);

            WebResponse response = request.GetResponse();
            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            string responseData = reader.ReadToEnd();
            if (responseData.Length <= 0)
            {
              //  Response.Write("Failed");
            }
            else
            {
                User foundAccount = json.Deserialize<User>(responseData);

                Session["RegisteringUserID"] = foundAccount.userID;
                Session["token"] = foundAccount.token;

                Response.Redirect("Registration.aspx");
            }
        }

        public class User
        {
            public string userID { get; set; }

            public string token { get; set; }
        }
        public class VerificationCredentials
        {
            public string email { get; set; }
            public string code { get; set; }
        }
    }
    }