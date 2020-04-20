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
                int plannedDates = Convert.ToInt32(Session["plannedDates"]);
                int unreadMessages = Convert.ToInt32(Session["unreadMessages"]);

                hNumPlannedDates.InnerText = plannedDates + " Planned Dates";
                hNumUnreadMessages.InnerText = unreadMessages + " Unread Messages";

                List<int> memberDislikes = (List<int>)Session["memberDislikes"];
                List<int> memberBlocks = (List<int>)Session["memberBlocks"];
                for (int row = 0; row < dt.Rows.Count; row++)
                {
                    DataRow r = dt.Rows[row];
                    Boolean blocksContains = memberBlocks.Contains(Convert.ToInt32(r["userID"]));
                    Boolean passContains = memberDislikes.Contains(Convert.ToInt32(r["userID"]));
                    if ( !blocksContains && !passContains && !((Convert.ToInt32(r["userID"]) == userID)))
                    {
                        User u = SetValues(r);
                        people.Add(u);
                    } // end inner if 
                } // end for loop
            } // end else
            rptPeople.DataSource = people; rptPeople.DataBind();

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
    }
}