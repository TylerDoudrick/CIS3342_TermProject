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
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;


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
            //string ph = "images/person21.jpg";
            //BinaryFormatter serializer = new BinaryFormatter();
            //MemoryStream memStream = new MemoryStream();
            //serializer.Serialize(memStream, ph);
            //Byte[] imgArray = memStream.ToArray();

            //commandObj.Parameters.Clear();
            //commandObj.CommandType = CommandType.StoredProcedure;
            //commandObj.CommandText = "test";
            //commandObj.Parameters.AddWithValue("@profID", 21);
            //commandObj.Parameters.AddWithValue("@image", imgArray);
            //dbConnection.DoUpdateUsingCmdObj(commandObj, out string err);

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
                    getPrefs(Int32.Parse(foundAccount.userID));
                    GetAcceptedDates(Int32.Parse(foundAccount.userID));

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
                            Response.Redirect("Dashboard.aspx");
                            break;
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
            Session["FirstName"] = "Samantha";
            Session["LastName"] = "Rogers";
            Session["UserID"] = "2";
            Session["token"] = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE1ODcyNzY2Nzd9.nUnarRJiy26XQjw9AFE986rYRTvykpLJs8483vX91wE";


            List<int> memberLieks = new List<int>(); memberLieks.Add(3); memberLieks.Add(9); memberLieks.Add(2); Session["memberLikes"] = memberLieks;
            List<int> memberDislikes = new List<int>(); memberDislikes.Add(7); Session["memberDislikes"] = memberDislikes;
            List<int> memberBlocks = new List<int>();memberBlocks.Add(6); Session["memberBlocks"] = memberBlocks;
            GetAcceptedDates(2);

            Session["memberBlocks"] = memberBlocks;
            Session["seeking"] = "Male";
            //Response.Redirect("Dashboard.aspx");

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
                    Response.Redirect("Dashboard.aspx");
                    break;
            }
        }
        protected void btnDebug2_Click(object sender, EventArgs e)
        {
            Session["FirstName"] = "Thomas";
            Session["LastName"] = "Smith";
            Session["UserID"] = "1";
            Session["seeking"] = "Female";
            Session["token"] = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE1ODcyNzY2ODV9.RKvyybRJyA9tS1rfsCL5nj7-dmtzCt6f0586b_V9E5Q";

            GetAcceptedDates(1);
            
            List<int> memberLieks = new List<int>();
            memberLieks.Add(2); memberLieks.Add(6); memberLieks.Add(8);
            Session["memberLikes"] = memberLieks;
            List<int> memberDislikes = new List<int>(); memberDislikes.Add(4); memberDislikes.Add(6); memberDislikes.Add(2); Session["memberDislikes"] = memberDislikes;
            List<int> memberBlocks = new List<int>(); memberBlocks.Add(3); memberBlocks.Add(5); Session["memberBlocks"] = memberBlocks;

            Session["memberBlocks"] = memberBlocks;
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
                    Response.Redirect("Dashboard.aspx");
                    break;
            }
        }

        protected void getPrefs(int userID)
        {
            commandObj.CommandType = CommandType.StoredProcedure;
            commandObj.CommandText = "TP_GetPreferences";

            SqlParameter uid = new SqlParameter("@userID", userID)
            {
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.VarChar
            };

            commandObj.Parameters.Add(uid);

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
            public string token { get; set; }
        }
        protected void GetAcceptedDates(int userID)
        { // if there's a successeful login, this will get all accepted dates so personal information can be made avaiable for those users.
            WebRequest request = WebRequest.Create(interactionsWebAPI + "getAcceptedDates/" + userID);
            WebResponse response = request.GetResponse();
            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            String data = reader.ReadToEnd();
            reader.Close(); response.Close();

            JavaScriptSerializer js = new JavaScriptSerializer();
            DataSet ds = JsonConvert.DeserializeObject<DataSet>(data);
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
        } // end get accepted dates
    }
}