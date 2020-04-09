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
    // there's a div with contact info - that's hidden until date request is approved!!

    public partial class MemberProfile : System.Web.UI.Page
    {
        string interactionsWebAPI = "https://localhost:44375/api/datingservice/interactions/";
        int memberUserID; int userID; string memberName = "";
        List<int> memberLikes = new List<int>(); List<int> memberDislikes = new List<int>(); List<int> memberBlocks = new List<int>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
            { 
                divPrivateBasic.Attributes.Add("style", "display:flex"); // show private info in the basic info category
                divFavThings.Attributes.Add("style", "display:flex"); // show fav things
                btnBlock.Enabled = true; btnLike.Enabled = true; btnPass.Enabled = true; btnDateRequest.Enabled = true;
                userID = Convert.ToInt32(Session["userID"].ToString()); // get userID from session
                memberLikes = (List<int>)Session["memberLikes"]; memberDislikes = (List<int>)Session["memberDislikes"]; memberBlocks = (List<int>)Session["memberBlocks"];
            } // end if 
            else
            {
                btnBlock.Enabled = false; btnLike.Enabled = false; btnPass.Enabled = false; btnDateRequest.Enabled = false;
            }
            memberUserID = 2; // this is the user id of the person who's profile we're viewing
        } // end page load

        protected void btnLike_Click(object sender, EventArgs e)
        {

            memberLikes.Add(memberUserID);
            Session["memberLikes"] = memberLikes;
            string message = "You have liked " + memberName;
            UpdatePreferences(message);
        } // end btn like event handler

        protected void btnPass_Click(object sender, EventArgs e)
        {
            memberDislikes.Add(memberUserID);
            Session["memberDislikes"] = memberDislikes;
            string message = "You have passed on " + memberName;
            UpdatePreferences(message);
        } // end btn pass event handler

        protected void btnBlock_Click(object sender, EventArgs e)
        {
            memberBlocks.Add(memberUserID);
            Session["memberBlocks"] = memberBlocks;
            string message = "You have blocked " + memberName;
            UpdatePreferences(message);
        } // end btn block event handler

        protected void btnDateRequest_Click(object sender, EventArgs e)
        {
            /* string email = Session["email"].ToString(); // this needs to be changed to member's email!
             string sendAdd = "querydating@gmail.com";
             MailMessage msg = new MailMessage();
             msg.To.Add(new MailAddress(@email));
             msg.Subject = "QUERY Verification Email";
             msg.From = new MailAddress(sendAdd);
             msg.IsBodyHtml = true;
             msg.Priority = MailPriority.Normal;
             msg.Body = "<div> You've gotten a date request! Log into your account to view the request " +
                 "and to accept or deny! </div>";
             SmtpClient smtp = new SmtpClient("smtp.temple.edu");
             smtp.EnableSsl = true;

             smtp.Send(msg); */
        } // end date request eventhandler

        protected void UpdatePreferences(string message)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream mStream = new MemoryStream();
            Byte[] mLikes; Byte[] mDislikes; Byte[] mBlocks;
            JavaScriptSerializer js = new JavaScriptSerializer();
            bf.Serialize(mStream, memberLikes); mLikes = mStream.ToArray();
            bf.Serialize(mStream, memberDislikes); mDislikes = mStream.ToArray();
            bf.Serialize(mStream, memberBlocks); mBlocks = mStream.ToArray();

            // set up the web api call and add the parameters
            WebRequest request = WebRequest.Create(interactionsWebAPI + "updatePreferences");
            request.Method = "PUT";
            request.ContentLength = userID.ToString().Length + mLikes.Length + mDislikes.Length + mBlocks.Length;
            request.ContentType = "application/json";
            StreamWriter writer = new StreamWriter(request.GetRequestStream());
            writer.Write(userID); writer.Write(mLikes); writer.Write(mDislikes); writer.Write(mBlocks);
            writer.Flush(); writer.Close();

            // send request and read response
            WebResponse response = request.GetResponse();
            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            String data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            // redirect to dashboard
            string url = "Dashboard.aspx";
            string script = "window.onload = function(){ alert('" + message + "'); window.location = '" + url + "'; }";
            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
        }
    } // end class
} // end namespace