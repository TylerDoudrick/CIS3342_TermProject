using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
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
        DBConnect obj = new DBConnect();
        string interactionsWebAPI = "https://localhost:44375/api/datingservice/interactions/";
        string profileWebAPI = "https://localhost:44375/api/datingservice/profile/"; int memberUserID; int userID; 
        string url = "Dashboard.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
            {
                userID = Convert.ToInt32(Session["userID"].ToString());
                divPrivateBasic2.Attributes.Add("style", "display:block");
                divPrivateBasic.Attributes.Add("style", "display:flex"); // show private info in the basic info category
                divFavThings.Attributes.Add("style", "display:block"); // show fav things
                btnBlock.Enabled = true; btnLike.Enabled = true; btnPass.Enabled = true; btnDateReq.Enabled = true;
                userID = Convert.ToInt32(Session["userID"].ToString()); // get userID from session
                //memberLikes = (List<int>)Session["memberLikes"]; memberDislikes = (List<int>)Session["memberDislikes"]; memberBlocks = (List<int>)Session["memberBlocks"];
            } // end if 
            else
            {
                btnBlock.Enabled = false; btnLike.Enabled = false; btnPass.Enabled = false; btnDateReq.Enabled = false;
            }
            memberUserID = Convert.ToInt32(Request.QueryString["memberID"]); // this is the user id of the person who's profile we're viewing
            grabPersonalProfile();
        } // end page load

        protected void btnLike_Click(object sender, EventArgs e)
        {
            List<int> memberLikes = (List<int>)Session["memberLikes"];

            if (!(memberLikes.Contains(memberUserID)))
            {
                memberLikes.Add(memberUserID);
                Session["memberLikes"] = memberLikes;
                UpdatePreferences();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "FailureToast", "showError();", true);
            }

        } // end btn like event handler

        protected void grabPersonalProfile()
        {
            WebRequest request = WebRequest.Create(profileWebAPI + memberUserID); // grab info from validation table values
            WebResponse response = request.GetResponse();
            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);

            String data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            DataSet result = JsonConvert.DeserializeObject<DataSet>(data);
            if (result.Tables[0].Rows.Count != 1)
            {
                //error
            }
            else
            {
                DataRow profile = result.Tables[0].Rows[0];
                DataTable religion = result.Tables[1];
                DataTable commitments = result.Tables[2];
                DataTable interests = result.Tables[3];
                DataTable likes = result.Tables[4];
                DataTable dislikes = result.Tables[5];

                DataRow image = result.Tables[6].Rows[0]; // get image 
                string x = image[0] as string;
                //    Byte[] imgArray = Convert.FromBase64String(base64);
                Byte[] imgArray = Encoding.ASCII.GetBytes(x);

                //printing characters with byte values
                MemoryStream memorystreamd = new MemoryStream(imgArray);
                BinaryFormatter bfd = new BinaryFormatter();
                string url = (bfd.Deserialize(memorystreamd)).ToString();
                img.ImageUrl = url;

                DateTime now = DateTime.Now;
                DateTime birthday = Convert.ToDateTime(profile["birthday"].ToString());
                TimeSpan timelived = now.Subtract(birthday);
                int age = timelived.Days / 365;
                Name.Text = profile["firstName"].ToString() + " " + profile["lastName"].ToString() + ", " + age.ToString();
                lblLocation.Text = profile["city"].ToString() + ", " + profile["state"].ToString();
                lblTagline.Text = profile["tagline"].ToString();


                lblPhoneNumber.Text = profile["phoneNumber"].ToString();
               lblEmail.Text = profile["emailAddress"].ToString();
                lblBio.Text = profile["bio"].ToString();

                //Seeking Gender
                lblNumKids.Text = profile["numChildren"].ToString();
                lblWantKids.Text = (profile["wantChildren"].ToString());
                lblSeekingGender.Text = (profile["seekingGender"].ToString());

                lblOccupation.Text =(profile["occupation"].ToString());
                lblFavSongs.Text = profile["favSongs"].ToString();
                lblFavSayings.Text = profile["favSayings"].ToString();
                lblFavRestaurants.Text = profile["favRestaurants"].ToString();
                lblFavMovies.Text = profile["favMovies"].ToString();
                lblFavBooks.Text = profile["favBooks"].ToString();

                //Table 1 is religion, Table 2 is Commitments, Table 3 is Interests, Table 4 is Likes, Table 5 is Dislikes
                string strReligions = "";
                string strCommitments = "";
                string strInterests = "";
                string strLikes = "";
                string strDislikes = "";

                foreach (DataRow row in religion.Rows)
                {
                    strReligions += row["ReligionType"].ToString();
                    strReligions += ", ";

                }
                foreach (DataRow row in commitments.Rows)
                {
                    strCommitments += row["CommitmentType"].ToString();
                    strCommitments += ", ";
                }

                foreach (DataRow row in interests.Rows)
                {
                    strInterests += row["InterestType"].ToString();
                    strInterests += ", ";
                }

                foreach (DataRow row in likes.Rows)
                {
                    strLikes += row["LikeType"].ToString();
                    strLikes += ", ";
                }

                foreach (DataRow row in dislikes.Rows)
                {
                    strDislikes += row["DislikeType"].ToString();
                    strDislikes += ", ";
                }


                if (strReligions.Length > 0) lblReligion.Text = strReligions.Remove(strReligions.Length - 2, 2);
                if (strCommitments.Length > 0) lblCommitment.Text = strCommitments.Remove(strCommitments.Length - 2, 2);
                if (strInterests.Length > 0) lblInterests.Text = strInterests.Remove(strInterests.Length - 2, 2);
                if (strLikes.Length > 0) lblLikes.Text = strLikes.Remove(strLikes.Length - 2, 2);
                if (strDislikes.Length > 0) lblDislikes.Text = strDislikes.Remove(strDislikes.Length - 2, 2);

            }
        }

        protected void btnPass_Click(object sender, EventArgs e)
        {
            List<int> memberDislikes = (List<int>)Session["memberDislikes"];
            if (!(memberDislikes.Contains(memberUserID)))
            {
                memberDislikes.Add(memberUserID);
                Session["memberDislikes"] = memberDislikes;
                UpdatePreferences();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ErrorToast", "showError();", true);
            }



        } // end btn pass event handler

        protected void btnBlock_Click(object sender, EventArgs e)
        {
            List<int> memberBlocks = (List<int>)Session["memberBlocks"];
            if (!(memberBlocks.Contains(memberUserID)))
            {
                memberBlocks.Add(memberUserID);
                Session["memberBlocks"] = memberBlocks;

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

                ClientScript.RegisterStartupScript(this.GetType(), "SuccessToast", "showSuccess();", true);

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ErrorToast", "showError();", true);
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
                
                string sendAdd = "querydating@gmail.com";
                DataSet ds = JsonConvert.DeserializeObject<DataSet>(data);
                string memEmail = ds.Tables[0].Rows[0][0].ToString();
                MailMessage msg = new MailMessage();
                msg.To.Add(new MailAddress(memEmail));
                msg.Subject = "QUERY New Dating Request";
                msg.From = new MailAddress(sendAdd);
                msg.IsBodyHtml = true;
                msg.Body = "<div> You have a new dating request! <BR><br> Log into your account to view the request and accept/deny! <br><BR><div class='text-body text-center'>Happy Dating! </ div > " +"<Br><BR> <div>";
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new System.Net.NetworkCredential(sendAdd, "CIS3342TermProject");
                smtp.EnableSsl = true;

                smtp.Send(msg);


                // redirect to dashboard
                /*   string success = "Successfully sent a date request to " + Name.InnerText;
                   string script = "window.onload = function(){ alert('" + success + "'); window.location = '" + url + "'; }";
                   ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);*/
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessToast", "showSuccess();", true);

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
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessToast", "showSuccess();", true);

            // redirect to dashboard
            //  string script = "window.onload = function(){ alert('" + message + "'); window.location = '" + url + "'; }";
            //ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
        }
    } // end class
} // end namespace