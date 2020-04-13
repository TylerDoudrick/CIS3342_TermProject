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

namespace TermProject
{
    public partial class LikeandPass : System.Web.UI.Page
    {
        string interactionsWebAPI = "https://localhost:44375/api/datingservice/interactions/";
        string profileWebAPI = "https://localhost:44375/api/datingservice/profile/";

        DBConnect obj = new DBConnect();
        int userID;
        List<int> memberLikes = new List<int>();
        List<int> memberDislikes = new List<int>();

        List<User> ml = new List<User>();
        List<User> md = new List<User>();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["UserID"] == null) Response.Redirect("Default.aspx");
            else
            {
                userID = Convert.ToInt32(Session["userID"].ToString());
                memberLikes = (List<int>)Session["memberLikes"];
                memberDislikes = (List<int>)Session["memberDislikes"];

                if (!IsPostBack)
                {
                    bind();
                }                
            } // end else

        } // end page load


        protected void bind( )
        {
            List<User> likedUsers = new List<User>();
            List<User> passedUsers = new List<User>();

            DataTable dtLikes = new DataTable();
            dtLikes.Columns.Add("UserId", typeof(int));
            foreach (int id in memberLikes)
            {
                DataRow dr = dtLikes.NewRow();
                dr["UserId"] = id;
                dtLikes.Rows.Add(dr);
            }

            SqlCommand objCMD = new SqlCommand();
            objCMD.Parameters.Clear();
            objCMD.CommandType = CommandType.StoredProcedure;
            objCMD.CommandText = "TP_GetUsersFromList";
            objCMD.Parameters.AddWithValue("@UserList", dtLikes);

            DataSet dsUser = obj.GetDataSetUsingCmdObj(objCMD);
            DataTable dt = dsUser.Tables[0];

            for (int row = 0; row < dt.Rows.Count; row++)
            {
                int id = Convert.ToInt32(dt.Rows[row]["userID"]);
                if (memberLikes.Contains(id))
                {
                    Byte[] imgArrayLiked = (Byte[])dsUser.Tables[0].Rows[row]["profileImage"];
                    MemoryStream memorystreamd = new MemoryStream(imgArrayLiked);
                    BinaryFormatter bfd = new BinaryFormatter();
                    string url = (bfd.Deserialize(memorystreamd)).ToString();

                    User u = new User();
                    u.userID = id;
                    u.name = (dsUser.Tables[0].Rows[row]["firstName"].ToString() + " " + dsUser.Tables[0].Rows[row]["lastName"].ToString());
                    u.tagline = dsUser.Tables[0].Rows[row]["tagline"].ToString();
                    u.imageSRC = url;
                    likedUsers.Add(u);
                } // end if for likes
                else if (memberDislikes.Contains(id))
                {
                    Byte[] imgArrayPassed = (Byte[])dsUser.Tables[0].Rows[row]["profileImage"];
                    MemoryStream memorystreamd = new MemoryStream(imgArrayPassed);
                    BinaryFormatter bfd = new BinaryFormatter();
                    string url = (bfd.Deserialize(memorystreamd)).ToString();

                    User u = new User();
                    u.userID = id;
                    u.name = (dsUser.Tables[0].Rows[row]["firstName"].ToString() + " " + dsUser.Tables[0].Rows[0]["lastName"].ToString());
                    u.tagline = dsUser.Tables[0].Rows[row]["tagline"].ToString();
                    u.imageSRC = url;
                    passedUsers.Add(u);
                } // end pass
            }
            rptmemLikes.DataSource = likedUsers; rptmemLikes.DataBind();
            rptDislikes.DataSource = passedUsers; rptDislikes.DataBind();
                        
        }

        protected void lbUnlike_Command(object sender, CommandEventArgs e)
        { // removes user from liked list
            int unLikeUserID = Convert.ToInt32(e.CommandName);
            memberLikes.Remove(unLikeUserID);
            Session["memberLikes"] = memberLikes; // update session

            BinaryFormatter bf = new BinaryFormatter(); MemoryStream mStream = new MemoryStream();
            Byte[] mLikes;
            bf.Serialize(mStream, memberLikes); mLikes = mStream.ToArray();

            // update db
            SqlCommand objUpdatePref = new SqlCommand();
            objUpdatePref.CommandType = CommandType.StoredProcedure;
            objUpdatePref.CommandText = "TP_UpdatePreferences";
            objUpdatePref.Parameters.AddWithValue("@userID", userID);
            objUpdatePref.Parameters.AddWithValue("@@likes", mLikes);
            obj.DoUpdateUsingCmdObj(objUpdatePref, out string error);

            if (String.IsNullOrEmpty(error))
            {
            } // end if

        }
    } // end class
} // end namespace