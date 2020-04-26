using Classess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using Newtonsoft.Json;
using System.Net;
using System.Web.Script.Serialization;

namespace TermProject
{
    public partial class Dashboard : System.Web.UI.Page
    {
        string interactionsWebAPI = "https://localhost:44375/api/datingservice/interactions/";
        string profileWebAPI = "https://localhost:44375/api/datingservice/profile/";

        DBConnect obj = new DBConnect();
        SqlCommand commandObj = new SqlCommand();
        int userID;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // if (Session["UserID"] == null) Response.Redirect("Default.aspx");

                commandObj.Parameters.Clear();
                commandObj.CommandType = CommandType.StoredProcedure;
                commandObj.CommandText = "TP_GetAllUsers";
                if (Session["UserID"] == null)
                { // if user is not logged in, show all users
                    divNumDates.Visible = false;
                    divNumMessages.Visible = false;
                    divVisitorDate.Visible = true;
                    divVisitorMess.Visible = true;

                    divMemberOnlyFeature.Disabled = true;
                    string seeking = "Both";
                    commandObj.Parameters.AddWithValue("@seekingGen", seeking);
                }
                else
                {
                    divNumDates.Visible = true;
                    divNumMessages.Visible = true;
                    divVisitorDate.Visible = false;
                    divVisitorMess.Visible = false;

                    userID = Convert.ToInt32(Session["UserID"]);
                    string seeking = Session["seeking"].ToString();
                    commandObj.Parameters.AddWithValue("@seekingGen", seeking);
                }
                DataSet dsUser = obj.GetDataSetUsingCmdObj(commandObj); // get the dataset
                DataTable dt = dsUser.Tables[0];
                List<User> people = new List<User>();
                if (Session["UserID"] == null)
                {
                    for (int row = 0; row < dt.Rows.Count; row++)
                    {
                        DataRow r = dt.Rows[row];
                        User u = SetValues(r);
                        people.Add(u);
                    } // end for loop
                } // end outer if
                else
                {
                    GetAcceptedDates();
                    GetUnreadMessages();
                    //int plannedDates = Convert.ToInt32(Session["plannedDates"]);
                    //int unreadMessages = Convert.ToInt32(Session["unreadMessages"]);

                    //hNumPlannedDates.InnerText = plannedDates + " Planned Dates";
                    //hNumUnreadMessages.InnerText = unreadMessages + " Unread Messages";

                    List<int> memberDislikes = (List<int>)Session["memberDislikes"];
                    List<int> memberBlocks = (List<int>)Session["memberBlocks"];
                    for (int row = 0; row < dt.Rows.Count; row++)
                    {
                        DataRow r = dt.Rows[row];
                        Boolean blocksContains = memberBlocks.Contains(Convert.ToInt32(r["userID"]));
                        Boolean passContains = memberDislikes.Contains(Convert.ToInt32(r["userID"]));
                        if (!blocksContains && !passContains && !((Convert.ToInt32(r["userID"]) == userID)))
                        {
                            User u = SetValues(r);
                            people.Add(u);
                        } // end inner if 
                    } // end for loop
                } // end else
                rptPeople.DataSource = people; rptPeople.DataBind();
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "FailureToast", "showDBError();", true);

            }
        } // end page load

        protected User SetValues(DataRow Row)
        {
            User u = new User();

            Byte[] imgArray = (Byte[])Row["profileImage"];
            MemoryStream memorystreamd = new MemoryStream(imgArray);
            BinaryFormatter bfd = new BinaryFormatter();
            string url = (bfd.Deserialize(memorystreamd)).ToString();
            u.userID = Convert.ToInt16(Row["userID"]);
            u. name = Row["firstName"].ToString()+ " " + Row["lastName"].ToString(); ;
            u.tagline = Row["tagline"].ToString();
            u.imageSRC = url;
            u.city = Row["city"].ToString();
            u.state = Row["state"].ToString();

            if (Row["gender"].ToString().Trim().ToLower() == "female")
            {
                u.gender = "F";
            }
            else
            {
                u.gender = "M";
            }

            u.occuption = Row["occupation"].ToString();

            DateTime now = DateTime.Now;
            DateTime birthday = Convert.ToDateTime(Row["birthday"].ToString());
            TimeSpan timelived = now.Subtract(birthday);
            int age = timelived.Days / 365;
            u.age = age;

            string heading = (u.name) + " (" + u.gender + ") , " + u.age;
            u.heading = heading;
            u.occuption = Row["occupation"].ToString();

            return u;
        }

        protected void lbGoToProfile_Command(object sender, CommandEventArgs e)
        { // transfers you to the profile you clicked on
            int uID = Convert.ToInt32(e.CommandName);
            Response.Redirect("MemberProfile.aspx?memberID=" + uID);
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        { // testing purporse
            Response.Redirect("MemberProfile.aspx?memberID=5");            

        }

        protected void lbGoToProfile_Command1(object sender, CommandEventArgs e)
        { // transfers you to the person's profile
            int memID = Convert.ToInt32(e.CommandName);
            Response.Redirect("MemberProfile.aspx?memberID=" + memID);
        } // end go to profile

        protected void GetAcceptedDates()
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

            for (int i = 0; i < one.Rows.Count; i++)
            {
                int id = Convert.ToInt32(one.Rows[i]["userID"]);
                acceptedDates.Add(id);
            }
            for (int i = 0; i < two.Rows.Count; i++)
            {
                int id = Convert.ToInt32(two.Rows[i]["userID"]);
                acceptedDates.Add(id);
            }
            Session["acceptedDates"] = acceptedDates;
            Session["plannedDates"] = two.Rows.Count;// count of planned dates

            hNumPlannedDates.InnerText = two.Rows.Count + " Planned Dates";
        } // end get accepted dates

        protected void GetUnreadMessages()
        {
            User u = new User();
            u.userID = userID;

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
            if (im == null)
            {
                hNumUnreadMessages.InnerText =  "0 Unread Messages";
            }
            else
            {
                Session["unreadMessages"] = im.Count(); // store the number of unread messages in session
                hNumUnreadMessages.InnerText = im.Count() + " Unread Messages";
            }
        } // end get unread messages
    }
}