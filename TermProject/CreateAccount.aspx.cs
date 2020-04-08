using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TermProject
{
    public partial class CreateAccount : System.Web.UI.Page
    {
        string interactionsWebAPI = "https://localhost:44375/api/datingservice/interactions/";
        string profileWebAPI = "https://localhost:44375/api/datingservice/profile/";
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btnCreateAccount_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            /*string sendAdd = "querydating@gmail.com";
            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(@email));
            msg.Subject = "QUERY Verification Email";
            msg.From = new MailAddress(sendAdd);
            msg.IsBodyHtml = true;
            msg.Body = "<div> Thank you for signing up for Query! <Br><BR> Please click the below link to verify your account status." +
                "<Br><BR> <div>";
            SmtpClient smtp = new SmtpClient("smtp.temple.edu");
            smtp.EnableSsl = true;

            smtp.Send(msg); */
            
            List<int> memberLikes = new List<int>();
            List<int> memberDislikes = new List<int>();
            List<int> memberBlocks = new List<int>();

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream mStream = new MemoryStream();
            Byte[] mLikes; Byte[] mDislikes; Byte[] mBlocks;

            bf.Serialize(mStream, memberLikes); mLikes = mStream.ToArray();
            bf.Serialize(mStream, memberDislikes); mDislikes = mStream.ToArray();
            bf.Serialize(mStream, memberBlocks); mBlocks = mStream.ToArray();

            int userID = 100;  // this needs to be changed to the userID of the new user

            WebRequest request = WebRequest.Create(interactionsWebAPI + "insertPreferences/");
            request.Method = "POST";
            request.ContentLength = userID.ToString().Length + mLikes.Length + mDislikes.Length+mBlocks.Length;
            request.ContentType = "application/x-www-form-urlencoded";
            //request.ContentType = "application/json";
            // Write the JSON data to the Web Request
         
             StreamWriter writer = new StreamWriter(request.GetRequestStream());
             writer.Write(userID);
             //writer.Write(mLikes, 0, ml);
             writer.Write(mDislikes);
             writer.Write(mBlocks);
             writer.Flush();
             writer.Close();
            // Read the data from the Web Response, which requires working with streams.

            WebResponse response = request.GetResponse();
            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            String data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            Session["email"] = email;
            Session["userID"] = userID;
            Session["memberLikes"] = memberLikes;
            Session["memberDislikes"] = memberDislikes;
            Session["memberBlocks"] = memberBlocks;
            Response.Redirect("Registration.aspx");
        }
    }
}