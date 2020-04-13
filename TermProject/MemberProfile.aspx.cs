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
using Models;

namespace TermProject
{
    // there's a div with contact info - that's hidden until date request is approved!!

    public partial class MemberProfile : System.Web.UI.Page
    {
        string interactionsWebAPI = "https://localhost:44375/api/datingservice/interactions/";
        int memberUserID; int userID; string memberName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
            {
                userID = Convert.ToInt32(Session["userID"].ToString());

                divPrivateBasic.Attributes.Add("style", "display:flex"); // show private info in the basic info category
                divFavThings.Attributes.Add("style", "display:flex"); // show fav things
                btnBlock.Enabled = true; btnLike.Enabled = true; btnPass.Enabled = true; btnDateRequest.Enabled = true;
                userID = Convert.ToInt32(Session["userID"].ToString()); // get userID from session
                //memberLikes = (List<int>)Session["memberLikes"]; memberDislikes = (List<int>)Session["memberDislikes"]; memberBlocks = (List<int>)Session["memberBlocks"];
            } // end if 
            else
            {
                btnBlock.Enabled = false; btnLike.Enabled = false; btnPass.Enabled = false; btnDateRequest.Enabled = false;
            }
            memberUserID = Convert.ToInt32(Request.QueryString["memberID"]); // this is the user id of the person who's profile we're viewing
        } // end page load

        protected void btnLike_Click(object sender, EventArgs e)
        {
            List<int>   memberLikes = (List<int>)Session["memberLikes"];

            memberLikes.Add(memberUserID);
            Session["memberLikes"] = memberLikes;
            string message = "You have liked " + memberName;
            UpdatePreferences(message);
        } // end btn like event handler

        protected void btnPass_Click(object sender, EventArgs e)
        {
            List<int> memberDislikes = (List<int>)Session["memberDislikes"];
            memberDislikes.Add(memberUserID);
            Session["memberDislikes"] = memberDislikes;
            string message = "You have passed on " + memberName;
            UpdatePreferences(message);
        } // end btn pass event handler

        protected void btnBlock_Click(object sender, EventArgs e)
        {
            List<int> memberBlocks = (List<int>)Session["memberBlocks"];
            memberBlocks.Add(memberUserID);
            Session["memberBlocks"] = memberBlocks;
            string message = "You have blocked " + memberName;
            UpdatePreferences(message);

            IDictionary<string, string> newValues = new Dictionary<string, string>
            {
                ["userID"] = userID.ToString(),
                ["memID"] = memberUserID.ToString()
            };
            JavaScriptSerializer js = new JavaScriptSerializer();
            String jsonValues = js.Serialize(newValues);

            // remove messages, dates between the two
            WebRequest request = WebRequest.Create(interactionsWebAPI + "blockUser");
            request.Method = "PUT";
            request.ContentType = "application/json";

            StreamWriter writer = new StreamWriter(request.GetRequestStream());
            writer.Write(jsonValues);
            writer.Flush();
            writer.Close();

            WebResponse response = request.GetResponse();
            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            String data = reader.ReadToEnd();
            reader.Close();
            response.Close();
            
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
            List<int> memberLikes = (List<int>)Session["memberLikes"]; List<int> memberDislikes = (List<int>)Session["memberDislikes"]; List<int> memberBlocks = (List<int>)Session["memberBlocks"];
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream mStream = new MemoryStream();
            Byte[] mLikes; Byte[] mDislikes; Byte[] mBlocks;

            bf.Serialize(mStream, memberLikes); mLikes = mStream.ToArray();
            bf.Serialize(mStream, memberDislikes); mDislikes = mStream.ToArray();
            bf.Serialize(mStream, memberBlocks); mBlocks = mStream.ToArray();

            // int userID = 100;  // this needs to be changed to the userID of the new user

            Preferences p = new Preferences();
            p.id = userID; p.mLikes = mLikes; p.mDislikes = mDislikes; p.mBlocks = mBlocks;
            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonP = js.Serialize(p);

            WebRequest request = WebRequest.Create(interactionsWebAPI + "updatePreferences/");
            request.Method = "PUT";
            request.ContentLength = jsonP.Length;
            request.ContentType = "application/json";


            // Write the JSON data to the Web Request           
            StreamWriter writer = new StreamWriter(request.GetRequestStream());
            writer.Write(jsonP);
            writer.Flush();
            writer.Close();

            // Read the data from the Web Response, which requires working with streams.
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