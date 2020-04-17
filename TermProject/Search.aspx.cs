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
            gvTemp.DataSource = null;
            gvTemp.DataBind();
            rpCarousel.DataSource = null;
            rpCarousel.DataBind();
            int wantChildren = Int32.Parse(ddlWantKids.SelectedValue);
            int hasKids = Int32.Parse(ddlHasKids.SelectedValue);

            DataTable dtReligions = new DataTable();
            dtReligions.Columns.Add("TypeId", typeof(int));

            DataTable dtCommitments = new DataTable();
            dtCommitments.Columns.Add("TypeId", typeof(int));

            DataTable dtInterests = new DataTable();
            dtInterests.Columns.Add("TypeId", typeof(int));

            DataTable dtLikes = new DataTable();
            dtLikes.Columns.Add("TypeId", typeof(int));

            DataTable dtDislikes = new DataTable();
            dtDislikes.Columns.Add("TypeId", typeof(int));

            foreach (ListItem item in ddl.LBReligion.Items)
            {
                if (item.Selected)
                {
                    DataRow newRow = dtReligions.NewRow();
                    newRow["TypeId"] = Int32.Parse(item.Value);
                    dtReligions.Rows.Add(newRow);
                }
            }
            foreach (ListItem item in ddl.LBCommitment.Items)
            {
                if (item.Selected)
                {
                    DataRow newRow = dtCommitments.NewRow();
                    newRow["TypeId"] = Int32.Parse(item.Value);
                    dtCommitments.Rows.Add(newRow);
                }
            }
            foreach (ListItem item in ddl.LBInterest.Items)
            {
                if (item.Selected)
                {
                    DataRow newRow = dtInterests.NewRow();
                    newRow["TypeId"] = Int32.Parse(item.Value);
                    dtInterests.Rows.Add(newRow);
                }
            }
            foreach (ListItem item in ddl.LBLikes.Items)
            {
                if (item.Selected)
                {
                    DataRow newRow = dtLikes.NewRow();
                    newRow["TypeId"] = Int32.Parse(item.Value);
                    dtLikes.Rows.Add(newRow);
                }
            }
            foreach (ListItem item in ddl.LBDislikes.Items)
            {
                if (item.Selected)
                {
                    DataRow newRow = dtDislikes.NewRow();
                    newRow["TypeId"] = Int32.Parse(item.Value);
                    dtDislikes.Rows.Add(newRow);
                }
            }

            DBConnect databaseObj = new DBConnect();
            SqlCommand commandObj = new SqlCommand();
            commandObj.CommandType = CommandType.StoredProcedure;
            commandObj.CommandText = "TP_PerformSearch";

            commandObj.Parameters.AddWithValue("@SelectedReligions", dtReligions);
            commandObj.Parameters.AddWithValue("@SelectedCommitments", dtCommitments);
            commandObj.Parameters.AddWithValue("@SelectedInterests", dtInterests);
            commandObj.Parameters.AddWithValue("@SelectedLikes", dtLikes);
            commandObj.Parameters.AddWithValue("@SelectedDislikes", dtDislikes);

            DataSet foundProfiles = databaseObj.GetDataSetUsingCmdObj(commandObj);

            DataTable profilesTable = foundProfiles.Tables[0];
            profilesTable.Columns.Add("age", typeof(int));



            if (foundProfiles.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow profileRows in profilesTable.Rows)
                {
                    if ((Int32.Parse(profileRows["numChildren"].ToString()) > 0 && hasKids == 0) || (Int32.Parse(profileRows["numChildren"].ToString()) == 0 && hasKids == 1))
                    {
                        profileRows.Delete();
                    }
                    else if ((Int32.Parse(profileRows["wantChildren"].ToString()) == 1 && wantChildren == 0) || (Int32.Parse(profileRows["wantChildren"].ToString()) == 0 && wantChildren == 1))
                    {
                        profileRows.Delete();
                    }
                    else
                    {
                        DateTime now = DateTime.Now;
                        DateTime birthday = Convert.ToDateTime(profileRows["birthday"].ToString());
                        TimeSpan timelived = now.Subtract(birthday);
                        int age = timelived.Days / 365;
                        profileRows["age"] = age;
                    }
                }
                //foundProfiles.AcceptChanges();
                divResults.Attributes.Add("style", "display:flex");
                gvTemp.DataSource = foundProfiles;
                gvTemp.DataBind();
                rpCarousel.DataSource = foundProfiles;
                rpCarousel.DataBind();
            }
            else{
                Response.Write("none");
            }


        } // end search


    } // end class
} // end namespace