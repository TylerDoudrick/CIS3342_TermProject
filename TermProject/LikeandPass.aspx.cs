﻿using Classess;
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
                    Boolean mCheck = true; Boolean dCheck = true;
                    bind(mCheck, dCheck);
                }               
            } // end else
        } // end page load


        protected void bind(Boolean mCheck, Boolean dCheck )
        {
            List<User> likedUsers = new List<User>();
            List<User> passedUsers = new List<User>();
            SqlCommand objCMD = new SqlCommand();

            if (mCheck)
            {
                DataTable dtLikes = new DataTable();
                dtLikes.Columns.Add("UserId", typeof(int));
                foreach (int id in memberLikes)
                {
                    DataRow dr = dtLikes.NewRow();
                    dr["UserId"] = id;
                    dtLikes.Rows.Add(dr);
                }

                objCMD.Parameters.Clear();
                objCMD.CommandType = CommandType.StoredProcedure;
                objCMD.CommandText = "TP_GetUsersFromList";
                objCMD.Parameters.AddWithValue("@UserList", dtLikes);

                DataSet dsUser = obj.GetDataSetUsingCmdObj(objCMD);
                DataTable dtLikeResult = dsUser.Tables[0];

                for (int row = 0; row < dtLikeResult.Rows.Count; row++)
                { // deserialize image and create User objects
                    Byte[] imgArrayLiked = (Byte[])dsUser.Tables[0].Rows[row]["profileImage"];
                    MemoryStream memorystreamd = new MemoryStream(imgArrayLiked);
                    BinaryFormatter bfd = new BinaryFormatter();
                    string url = (bfd.Deserialize(memorystreamd)).ToString();

                    User u = new User();
                    u.userID = Convert.ToInt16(dtLikeResult.Rows[row]["userID"]);
                    u.name = (dsUser.Tables[0].Rows[row]["firstName"].ToString() + " " + dsUser.Tables[0].Rows[row]["lastName"].ToString());
                    u.tagline = dsUser.Tables[0].Rows[row]["tagline"].ToString();
                    u.imageSRC = url;
                    likedUsers.Add(u);
                }
                rptmemLikes.DataSource = likedUsers; rptmemLikes.DataBind();

            }

            if (dCheck)
            {
                DataTable dtPass = new DataTable();
                dtPass.Columns.Add("UserId", typeof(int));
                foreach (int id in memberDislikes)
                {
                    DataRow dr = dtPass.NewRow();
                    dr["UserId"] = id;
                    dtPass.Rows.Add(dr);
                }


                objCMD.Parameters.Clear();
                objCMD.CommandType = CommandType.StoredProcedure;
                objCMD.CommandText = "TP_GetUsersFromList";
                objCMD.Parameters.AddWithValue("@UserList", dtPass);

                DataSet dsUserPass = obj.GetDataSetUsingCmdObj(objCMD);
                DataTable dtPassResult = dsUserPass.Tables[0];

                for (int row = 0; row < dtPassResult.Rows.Count; row++)
                {
                    Byte[] imgArrayLiked = (Byte[])dtPassResult.Rows[row]["profileImage"];
                    MemoryStream memorystreamd = new MemoryStream(imgArrayLiked);
                    BinaryFormatter bfd = new BinaryFormatter();
                    string url = (bfd.Deserialize(memorystreamd)).ToString();

                    User u = new User();
                    u.userID = Convert.ToInt16(dtPassResult.Rows[row]["userID"]);
                    u.name = (dtPassResult.Rows[row]["firstName"].ToString() + " " + dtPassResult.Rows[row]["lastName"].ToString());
                    u.tagline = dtPassResult.Rows[row]["tagline"].ToString();
                    u.imageSRC = url;
                    passedUsers.Add(u);
                }
                rptDislikes.DataSource = passedUsers; rptDislikes.DataBind();
            }                        
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
            objUpdatePref.Parameters.AddWithValue("@likes", mLikes);
            obj.DoUpdateUsingCmdObj(objUpdatePref, out string error);

            if (String.IsNullOrEmpty(error))
            {
                Boolean l = true; Boolean d = false;
                bind(l,d);
            } // end if

        }

        protected void lbLikeFromPass_Command(object sender, CommandEventArgs e)
        { // removes person from pass list + adds them to like list
            int uID = Convert.ToInt32(e.CommandName);
            memberDislikes.Remove(uID);
            memberLikes.Add(uID);

            // serialize the lists
            BinaryFormatter bf = new BinaryFormatter(); MemoryStream mStream = new MemoryStream();
            Byte[] mLikes;
            bf.Serialize(mStream, memberLikes); mLikes = mStream.ToArray();

            Byte[] mPass;
            bf.Serialize(mStream, memberDislikes); mPass = mStream.ToArray();

            // add them to session again
            Session["memberLikes"] = memberLikes; Session["memberDislikes"] = memberDislikes;

            // update db with the updated lists
            SqlCommand objUpdatePref = new SqlCommand();
            objUpdatePref.CommandType = CommandType.StoredProcedure;
            objUpdatePref.CommandText = "TP_UpdatePreferences";
            objUpdatePref.Parameters.AddWithValue("@userID", userID);
            objUpdatePref.Parameters.AddWithValue("@likes", mLikes);
            objUpdatePref.Parameters.AddWithValue("@dislikes", mPass);
            obj.DoUpdateUsingCmdObj(objUpdatePref, out string error);

            // update carousel
            if (String.IsNullOrEmpty(error))
            {
                Boolean l = true; Boolean d = true;
                bind(l, d);
            } // end if
        }

        protected void removePass_Command(object sender, CommandEventArgs e)
        { // removes user from passed list to give them another shot
            int uID = Convert.ToInt32(e.CommandName);
            memberDislikes.Remove(uID);
            Session["memberDislikes"] = memberDislikes;

            // serialize the list
            BinaryFormatter bf = new BinaryFormatter(); MemoryStream mStream = new MemoryStream();
            Byte[] mDislikes;
            bf.Serialize(mStream, memberDislikes); mDislikes = mStream.ToArray();

            // update db
            SqlCommand objUpdatePref = new SqlCommand();
            objUpdatePref.CommandType = CommandType.StoredProcedure;
            objUpdatePref.CommandText = "TP_UpdatePreferences";
            objUpdatePref.Parameters.AddWithValue("@userID", userID);
            objUpdatePref.Parameters.AddWithValue("@dislikes", mDislikes);
            obj.DoUpdateUsingCmdObj(objUpdatePref, out string error);

            // update carousel
            if (String.IsNullOrEmpty(error))
            {
                Boolean l = false; Boolean d = true;
                bind(l, d);
            } // end if
        }
    } // end class
} // end namespace