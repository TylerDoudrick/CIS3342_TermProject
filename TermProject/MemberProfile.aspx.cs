using System;
using System.Collections.Generic;
using System.Data;
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
using Newtonsoft.Json;

namespace TermProject
{
    // there's a div with contact info - that's hidden until date request is approved!!

    public partial class MemberProfile : System.Web.UI.Page
    {
        string interactionsWebAPI = "https://localhost:44375/api/datingservice/interactions/";
        int memberUserID; int userID; 
        string url = "Dashboard.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
            {
                userID = Convert.ToInt32(Session["userID"].ToString());

                divPrivateBasic.Attributes.Add("style", "display:flex"); // show private info in the basic info category
                divFavThings.Attributes.Add("style", "display:flex"); // show fav things
                btnBlock.Enabled = true; btnLike.Enabled = true; btnPass.Enabled = true; btnDateReq.Disabled = false;
                userID = Convert.ToInt32(Session["userID"].ToString()); // get userID from session
                //memberLikes = (List<int>)Session["memberLikes"]; memberDislikes = (List<int>)Session["memberDislikes"]; memberBlocks = (List<int>)Session["memberBlocks"];
            } // end if 
            else
            {
                btnBlock.Enabled = false; btnLike.Enabled = false; btnPass.Enabled = false; btnDateReq.Disabled = true;
            }
            memberUserID = Convert.ToInt32(Request.QueryString["memberID"]); // this is the user id of the person who's profile we're viewing
        } // end page load

        protected void btnLike_Click(object sender, EventArgs e)
        {
            List<int> memberLikes = (List<int>)Session["memberLikes"];

            if (!(memberLikes.Contains(memberUserID)))
            {
                memberLikes.Add(memberUserID);
                Session["memberLikes"] = memberLikes;
                string message = "You have liked " + lblName.InnerText;
                UpdatePreferences();
            }
           
        } // end btn like event handler

        protected void btnPass_Click(object sender, EventArgs e)
        {
            List<int> memberDislikes = (List<int>)Session["memberDislikes"];
            if (!(memberDislikes.Contains(memberUserID)))
            {
                memberDislikes.Add(memberUserID);
                Session["memberDislikes"] = memberDislikes;
                string message = "You have passed on " + lblName.InnerText;
                UpdatePreferences();
            }
           
        } // end btn pass event handler

        protected void btnBlock_Click(object sender, EventArgs e)
        {
            List<int> memberBlocks = (List<int>)Session["memberBlocks"];
            if (!(memberBlocks.Contains(memberUserID)))
            {
                memberBlocks.Add(memberUserID);
                Session["memberBlocks"] = memberBlocks;
                string message = "You have blocked " + lblName.InnerText;

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
            }
          

        } // end btn block event handler

        protected void btnDateRequest_Click(object sender, EventArgs e)
        {
            string message = txtMessage.Text;
            IDictionary<string, string> newValues = new Dictionary<string, string>
            {
                ["sendingID"] = userID.ToString(),
                ["recID"] = memberUserID.ToString(),
                ["message"] = message
            };

            JavaScriptSerializer js = new JavaScriptSerializer();
            String jsonValues = js.Serialize(newValues);

            try
            {
                WebRequest request = WebRequest.Create(interactionsWebAPI + "addDateReq/");
                request.Method = "POST";
                request.ContentLength = jsonValues.Length;
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

                /*  
                 *  // if publishing works then this will be needed
                DataSet ds = JsonConvert.DeserializeObject<DataSet>(data);
                string memEmail = ds.Tables[0].Rows[0][0].ToString();

                 string sendAdd = "querydating@gmail.com";
                 MailMessage msg = new MailMessage();
                 msg.To.Add(new MailAddress(@memEmail));
                 msg.Subject = "QUERY Verification Email";
                 msg.From = new MailAddress(sendAdd);
                 msg.IsBodyHtml = true;
                 msg.Priority = MailPriority.Normal;
                 msg.Body = "<div> You've gotten a date request! Log into your account to view the request " +
                     "and to accept or deny! </div>";
                 SmtpClient smtp = new SmtpClient("smtp.temple.edu");
                 smtp.EnableSsl = true;

                 smtp.Send(msg); */

                // redirect to dashboard
                string success = "Successfully sent a date request to " + lblName.InnerText;
                string script = "window.onload = function(){ alert('" + success + "'); window.location = '" + url + "'; }";
                ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
            }
            catch
            {
                Response.Write("Could not send date request. Error Occurred.");
            }
            
        } // end date request eventhandler

        protected void UpdatePreferences()
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
          //  string script = "window.onload = function(){ alert('" + message + "'); window.location = '" + url + "'; }";
            //ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
        }
    } // end class
} // end namespace