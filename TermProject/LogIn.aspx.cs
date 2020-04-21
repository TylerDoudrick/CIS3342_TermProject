using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;

namespace TermProject
{
    public partial class LogIn : System.Web.UI.Page
    {
        string interactionsWebAPI = "https://localhost:44375/api/datingservice/interactions/";
        string profileWebAPI = "https://localhost:44375/api/datingservice/profile/";
        string authWebAPI = "https://localhost:44375/api/datingservice/authentication/";
        DBConnect dbConnection = new DBConnect();
        SqlCommand commandObj = new SqlCommand();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLoginSubmit_Click(object sender, EventArgs e)
        {
            string username = txtLogInUsername.Text;
            string password = txtLogInPassword.Text;

            bool trigger = false;

            txtLogInUsername.CssClass = txtLogInUsername.CssClass.Replace("is-invalid", "").Trim();
            txtLogInPassword.CssClass = txtLogInPassword.CssClass.Replace("is-invalid", "").Trim();
            if (username.Length <= 0)
            {
                txtLogInUsername.CssClass += " is-invalid";
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

                LoginCredentials cred = new LoginCredentials();
                cred.username = username;
                cred.password = password;
                WebRequest request = WebRequest.Create(authWebAPI);
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
                if(responseData.Length <= 0)
                {
                  //  Response.Write("Account not found");
                }
                else
                {
                    User foundAccount = json.Deserialize<User>(responseData);

                    if(foundAccount.isVerified == "0")
                    {
                        Session["email"] = foundAccount.emailAddress;
                        Session["VerifyingUserID"] = foundAccount.userID;

                        Response.Redirect("Verification.aspx");
                    }else if(foundAccount.finishedRegistration == "0")
                    {
                        Session["token"] = foundAccount.token;
                        Session["RegisteringUserID"] = foundAccount.userID;
                        Response.Redirect("Registration.aspx");
                        
                    }
                    else
                    {

                    Session["email"] = foundAccount.emailAddress;
                    Session["UserID"] = foundAccount.userID;
                    Session["seeking"] = foundAccount.seekingGender;
                    Session["firstName"] = foundAccount.firstName;
                    Session["lastName"] = foundAccount.lastName;
                    Session["token"] = foundAccount.token;

                    getPrefs(Convert.ToInt32(foundAccount.userID));
                    GetAcceptedDates(Convert.ToInt32(foundAccount.userID));
                    GetUnreadMessages((foundAccount.userID));
                    switch (Request.QueryString["target"])
                    {

                        case "Dates":
                            Response.Redirect("Dates.aspx");
                            break;
                        case "LikeandPass":
                            Response.Redirect("LikeandPass.aspx");
                            break;
                        case "Messages":
                            Response.Redirect("Messages.aspx");
                            break;
                        case "Profile":
                            Response.Redirect("Profile.aspx");
                            break;
                        case "Settings":
                            Response.Redirect("Settings.aspx");
                            break;
                        case "MemberProfile":
                            Response.Redirect("MemberProfile.aspx?"+ Request.QueryString.ToString());
                            break;
                        default:
                            Response.Redirect("Dashboard.aspx"); // send total number of accpeted date reqs in url
                            break;
                    }
                    }

                }
                reader.Close();
                response.Close();

                ////Do something

                //commandObj.Parameters.Clear();
                //commandObj.CommandType = CommandType.StoredProcedure;
                //commandObj.CommandText = "TP_LookupUserRecord";

                //SqlParameter inputUsername = new SqlParameter("@username", username)
                //{
                //    Direction = ParameterDirection.Input,

                //    SqlDbType = SqlDbType.VarChar
                //};

                //commandObj.Parameters.Add(inputUsername);


                //DataSet dsUser = dbConnection.GetDataSetUsingCmdObj(commandObj);
                //if (dsUser.Tables[0].Rows.Count > 0)
                //{
                //    DataRow drUserRecord = dsUser.Tables[0].Rows[0];

                //    string email = drUserRecord["emailAddress"].ToString();
                //    Session["email"] = email;

                //    byte[] salt = (byte[])drUserRecord["salt"];
                //    byte[] hashedPassword = (byte[])drUserRecord["password"];


                //    if (CryptoUtilities.comparePassword(hashedPassword, salt, password))
                //    {
                //        Session["UserID"] = drUserRecord["userID"].ToString();
                //        getPrefs(Convert.ToInt32(drUserRecord["userID"].ToString())); // get list of prefs to store in session

                //        // store the seeking gender in session
                //        string seeking = dsUser.Tables[1].Rows[0][0].ToString();
                //        Session["seeking"] = seeking;

                //        switch (Request.QueryString["target"])
                //        {

                //            case "Dates":
                //                Response.Redirect("Dates.aspx");
                //                break;
                //            case "LikeandPass":
                //                Response.Redirect("LikeandPass.aspx");
                //                break;
                //            case "Messages":
                //                Response.Redirect("Messages.aspx");
                //                break;
                //            case "Profile":
                //                Response.Redirect("Profile.aspx");
                //                break;
                //            case "Settings":
                //                Response.Redirect("Settings.aspx");
                //                break;

                //            default:
                //                Response.Redirect("Dashboard.aspx");
                //                break;
                //        }
                //    }
                //    else
                //    {
                //        Response.Write("Failed password check");
                //        //Invalid password :(
                //    }
                //}
                //else
                //{
                //    Response.Write("Failed username check");
                //    //Profile not found
                //}

            }

        }
        protected void btnDebug1_Click(object sender, EventArgs e)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();

            WebRequest request = WebRequest.Create(authWebAPI + "debug/samantha202");
            WebResponse response = request.GetResponse();
            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            string responseData = reader.ReadToEnd();


            if (responseData.Length <= 0)
            {

            }
            else
            {
                User foundAccount = json.Deserialize<User>(responseData);
                Session["email"] = foundAccount.emailAddress;
                Session["UserID"] = foundAccount.userID;
                Session["seeking"] = foundAccount.seekingGender;
                Session["firstName"] = foundAccount.firstName;
                Session["lastName"] = foundAccount.lastName;
                Session["token"] = foundAccount.token;

                getPrefs(Convert.ToInt32(foundAccount.userID));
                GetAcceptedDates(Convert.ToInt32(foundAccount.userID));
                GetUnreadMessages((foundAccount.userID));

                switch (Request.QueryString["target"])
                {

                    case "Dates":
                        Response.Redirect("Dates.aspx");
                        break;
                    case "LikeandPass":
                        Response.Redirect("LikeandPass.aspx");
                        break;
                    case "Messages":
                        Response.Redirect("Messages.aspx");
                        break;
                    case "Profile":
                        Response.Redirect("Profile.aspx");
                        break;
                    case "Settings":
                        Response.Redirect("Settings.aspx");
                        break;
                    case "MemberProfile":
                        Response.Redirect("MemberProfile.aspx?memberID=" + Request.QueryString["memberID"].ToString());
                        break;
                    default:
                        Response.Redirect("Dashboard.aspx"); // send total number of accpeted date reqs in url
                        break;
                }

            }

            reader.Close();
            response.Close();
        }
        protected void btnDebug2_Click(object sender, EventArgs e)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();

            WebRequest request = WebRequest.Create(authWebAPI + "debug/thomasUpdate");
            WebResponse response = request.GetResponse();
            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            string responseData = reader.ReadToEnd();


            if (responseData.Length <= 0)
            {

            }
            else
            {
                User foundAccount = json.Deserialize<User>(responseData);
                Session["email"] = foundAccount.emailAddress;
                Session["UserID"] = foundAccount.userID;
                Session["seeking"] = foundAccount.seekingGender;
                Session["firstName"] = foundAccount.firstName;
                Session["lastName"] = foundAccount.lastName;
                Session["token"] = foundAccount.token;

                getPrefs(Convert.ToInt32(foundAccount.userID));
                GetAcceptedDates(Convert.ToInt32(foundAccount.userID));
                GetUnreadMessages((foundAccount.userID));

                switch (Request.QueryString["target"])
                {

                    case "Dates":
                        Response.Redirect("Dates.aspx");
                        break;
                    case "LikeandPass":
                        Response.Redirect("LikeandPass.aspx");
                        break;
                    case "Messages":
                        Response.Redirect("Messages.aspx");
                        break;
                    case "Profile":
                        Response.Redirect("Profile.aspx");
                        break;
                    case "Settings":
                        Response.Redirect("Settings.aspx");
                        break;
                    case "MemberProfile":
                        Response.Redirect("MemberProfile.aspx?memberID=" + Request.QueryString["memberID"].ToString());
                        break;
                    default:
                        Response.Redirect("Dashboard.aspx"); // send total number of accpeted date reqs in url
                        break;
                }

            }

            reader.Close();
            response.Close();
        }
        protected void btnDebug3_Click(object sender, EventArgs e)
        {
            string username = txtLogInUsername.Text;

            JavaScriptSerializer json = new JavaScriptSerializer();

            WebRequest request = WebRequest.Create(authWebAPI + "debug/"+username);
            WebResponse response = request.GetResponse();
            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            string responseData = reader.ReadToEnd();


            if (responseData.Length <= 0)
            {

            }
            else
            {
                User foundAccount = json.Deserialize<User>(responseData);
                Session["email"] = foundAccount.emailAddress;
                Session["UserID"] = foundAccount.userID;
                Session["seeking"] = foundAccount.seekingGender;
                Session["firstName"] = foundAccount.firstName;
                Session["lastName"] = foundAccount.lastName;
                Session["token"] = foundAccount.token;
                getPrefs(Convert.ToInt32(foundAccount.userID));

                GetAcceptedDates(Convert.ToInt32(foundAccount.userID));
                GetUnreadMessages((foundAccount.userID));

                switch (Request.QueryString["target"])
                {

                    case "Dates":
                        Response.Redirect("Dates.aspx");
                        break;
                    case "LikeandPass":
                        Response.Redirect("LikeandPass.aspx");
                        break;
                    case "Messages":
                        Response.Redirect("Messages.aspx");
                        break;
                    case "Profile":
                        Response.Redirect("Profile.aspx");
                        break;
                    case "Settings":
                        Response.Redirect("Settings.aspx");
                        break;

                    default:
                        Response.Redirect("Dashboard.aspx"); // send total number of accpeted date reqs in url
                        break;
                }

            }

            reader.Close();
            response.Close();
        }

        protected void btnSendRecovery_Click(object sender, EventArgs e)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] random = new byte[16];
            rng.GetBytes(random);

            string rngString = Convert.ToBase64String(random);
            string trimmed = rngString.Substring(0,rngString.Length - 2);
           // Response.Write(trimmed);

            commandObj.Parameters.Clear();
            commandObj.CommandType = CommandType.StoredProcedure;
            commandObj.CommandText = "TP_CheckEmail";

            commandObj.Parameters.AddWithValue("@EmailAddress", txtPasswordRecoveryEmail.Text);
            commandObj.Parameters.AddWithValue("@Verification", trimmed);
            commandObj.Parameters.Add("@RowCount", SqlDbType.Int, 2).Direction = ParameterDirection.Output;


            DBConnect OBJ = new DBConnect();
            DataSet ds = OBJ.GetDataSetUsingCmdObj(commandObj);

            if(Int32.Parse(commandObj.Parameters["@RowCount"].Value.ToString()) == 1)
            {
                Response.Redirect("AccountRecovery.aspx");
            }
            else
            {
                Response.Redirect("AccountRecovery.aspx");
            }
        }

        protected void getPrefs(int userID)
        {
            commandObj.Parameters.Clear();

            commandObj.CommandType = CommandType.StoredProcedure;
            commandObj.CommandText = "TP_GetPreferences";
            
            commandObj.Parameters.AddWithValue("@userID", userID);

            DBConnect OBJ = new DBConnect();
            DataSet ds = OBJ.GetDataSetUsingCmdObj(commandObj);

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow test = ds.Tables[0].Rows[0];
                //Response.Write(test["memberLikes"]);
                Byte[] testarray = (Byte[])test["memberLikes"];
                MemoryStream memorystreamd = new MemoryStream(testarray);
                BinaryFormatter bfd = new BinaryFormatter();
                List<int> memberLikes = bfd.Deserialize(memorystreamd) as List<int>;

                Byte[] test2 = (Byte[])ds.Tables[0].Rows[0][1];
                MemoryStream m2 = new MemoryStream(test2);
                BinaryFormatter bfd2 = new BinaryFormatter();
                List<int> memberDislikes = bfd2.Deserialize(m2) as List<int>;

                Byte[] test3 = (Byte[])ds.Tables[0].Rows[0][1];
                MemoryStream m3 = new MemoryStream(test3);
                BinaryFormatter bfd3 = new BinaryFormatter();
                List<int> memberBlocks = bfd3.Deserialize(m3) as List<int>;

                Session["memberLikes"] = memberLikes;
                Session["memberDislikes"] = memberDislikes;
                Session["memberBlocks"] = memberBlocks;
            } // end if
        } // end method


        public class LoginCredentials
        {
            public string username { get; set; }
            public string password { get; set; }
        }
        public class User
        {
            public string userID { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string emailAddress { get; set; }
            public string seekingGender { get; set; }
            public string finishedRegistration { get; set; }
            public string isVerified { get; set; }
            public string token { get; set; }
            
        }

        protected void GetAcceptedDates(int userID)
        { // if there's a successeful login, this will get all accepted dates so personal information can be made avaiable for those users.
            WebRequest request = WebRequest.Create(interactionsWebAPI + "getAcceptedDates/" + userID);
            request.Headers.Add("Authorization", "Bearer " + Session["token"].ToString());
            WebResponse response = request.GetResponse();
            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            String data = reader.ReadToEnd();
            reader.Close(); response.Close();

            JavaScriptSerializer js = new JavaScriptSerializer();
            DataSet ds = JsonConvert.DeserializeObject<DataSet>(data);

            // datatable one is date requests, datatable two is planned dates
            DataTable one = ds.Tables[0]; DataTable two = ds.Tables[1];

            List<int> acceptedDates = new List<int>();

            for (int i=0;i<one.Rows.Count; i++)
            {
                int id = Convert.ToInt32(one.Rows[i]["userID"]);
                acceptedDates.Add(id);
            }
            for ( int i =0; i < two.Rows.Count; i++)
            {
                int id = Convert.ToInt32(two.Rows[i]["userID"]);
                acceptedDates.Add(id);
            }
            Session["acceptedDates"] = acceptedDates;

            Session["plannedDates"] = two.Rows.Count;// count of planned dates
        } // end get accepted dates

        protected void GetUnreadMessages(string uID)
        {
            User u = new User();
            u.userID = uID;

            // serialize the object
            JavaScriptSerializer js = new JavaScriptSerializer();
            String jsonValues = js.Serialize(u);

            // create the reqest
            WebRequest request = WebRequest.Create(interactionsWebAPI + "GetUserInbox");
            request.Headers.Add("Authorization", "Bearer " + Session["token"].ToString());

            request.Method = "POST";
            request.ContentType = "application/json";

            // write data to body
            StreamWriter writer = new StreamWriter(request.GetRequestStream());
            writer.Write(jsonValues);
            writer.Flush();
            writer.Close();

            // get response and read it
            WebResponse response = request.GetResponse();
            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            String data = reader.ReadToEnd();
            reader.Close(); response.Close();

            List<IncomingMessage> im = JsonConvert.DeserializeObject<List<IncomingMessage>>(data);
            Session["unreadMessages"]= im.Count(); // store the number of unread messages in session
        } // end get unread messages
    }
}