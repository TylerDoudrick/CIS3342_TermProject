using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace TermProject
{
    public partial class Search : System.Web.UI.Page
    {
        DBConnect obj = new DBConnect();
        string interactionsWebAPI = "https://localhost:44375/api/datingservice/interactions/";
        string profileWebAPI = "https://localhost:44375/api/datingservice/profile/";
        protected void Page_Load(object sender, EventArgs e)
        {
        } // end page load

       
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            divResults.Attributes.Add("style", "display:flex");

            List<string> religions = new List<string>();
            List<string> commitments = new List<string>();
            List<string> interests = new List<string>();
            List<string> likes = new List<string>();
            List<string> dislikes = new List<string>();

            foreach (ListItem item in ddl.LBReligion.Items)
            {
                if (item.Selected)
                {
                    religions.Add(item.Value);
                }
            }
            foreach (ListItem item in ddl.LBCommitment.Items)
            {
                if (item.Selected)
                {
                    commitments.Add(item.Value);
                }
            }
            foreach (ListItem item in ddl.LBInterest.Items)
            {
                if (item.Selected)
                {
                    interests.Add(item.Value);
                }
            }
            foreach (ListItem item in ddl.LBLikes.Items)
            {
                if (item.Selected)
                {
                    likes.Add(item.Value);
                }
            }
            foreach (ListItem item in ddl.LBDislikes.Items)
            {
                if (item.Selected)
                {
                    dislikes.Add(item.Value);
                }
            }

            DBConnect databaseObj = new DBConnect();
            SqlCommand commandObj = new SqlCommand();
            commandObj.CommandType = CommandType.StoredProcedure;
            commandObj.CommandText = "TP_UpdateDetails";

            DataTable dtReligions = new DataTable();
            dtReligions.Columns.Add("UserId", typeof(int));
            dtReligions.Columns.Add("TypeId", typeof(int));

            DataTable dtCommitments = new DataTable();
            dtCommitments.Columns.Add("UserId", typeof(int));
            dtCommitments.Columns.Add("TypeId", typeof(int));

            DataTable dtInterests = new DataTable();
            dtInterests.Columns.Add("UserId", typeof(int));
            dtInterests.Columns.Add("TypeId", typeof(int));

            DataTable dtLikes = new DataTable();
            dtLikes.Columns.Add("UserId", typeof(int));
            dtLikes.Columns.Add("TypeId", typeof(int));

            DataTable dtDislikes = new DataTable();
            dtDislikes.Columns.Add("UserId", typeof(int));
            dtDislikes.Columns.Add("TypeId", typeof(int));

            foreach (string str in religions)
            {
                DataRow newRow = dtReligions.NewRow();
                newRow["UserId"] = Int32.Parse(id);
                newRow["TypeId"] = Int32.Parse(str);
                dtReligions.Rows.Add(newRow);
            }
            foreach (string str in commitments)
            {
                DataRow newRow = dtCommitments.NewRow();
                newRow["UserId"] = Int32.Parse(id);
                newRow["TypeId"] = Int32.Parse(str);
                dtCommitments.Rows.Add(newRow);
            }
            foreach (string str in interests)
            {
                DataRow newRow = dtInterests.NewRow();
                newRow["UserId"] = Int32.Parse(id);
                newRow["TypeId"] = Int32.Parse(str);
                dtInterests.Rows.Add(newRow);
            }
            foreach (string str in likes)
            {
                DataRow newRow = dtLikes.NewRow();
                newRow["UserId"] = Int32.Parse(id);
                newRow["TypeId"] = Int32.Parse(str);
                dtLikes.Rows.Add(newRow);
            }
            foreach (string str in dislikes)
            {
                DataRow newRow = dtDislikes.NewRow();
                newRow["UserId"] = Int32.Parse(id);
                newRow["TypeId"] = Int32.Parse(str);
                dtDislikes.Rows.Add(newRow);
            }

            commandObj.Parameters.AddWithValue("@UserId", id);
            commandObj.Parameters.AddWithValue("@Religions", dtReligions);
            commandObj.Parameters.AddWithValue("@Commitments", dtCommitments);
            commandObj.Parameters.AddWithValue("@Interests", dtInterests);
            commandObj.Parameters.AddWithValue("@Likes", dtLikes);
            commandObj.Parameters.AddWithValue("@Dislikes", dtDislikes);

            if (databaseObj.DoUpdateUsingCmdObj(commandObj, out string exception) == -2)
            {
                return exception;
            }
            else
            {
                return "true";
            }



        } // end search


    } // end class
} // end namespace