using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
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
            if (Session["UserID"] == null)
            {
                //If the user is not logged in, they have limited options for searching
                ddl.HideInterestLikesDis();
                divHaveKids.Visible = false;
                divWantKids.Visible = false;
                divWantMore.Visible = true;
            }
        } // end page load

       
        protected void btnSearch_Click(object sender, EventArgs e)
        {

            //When the search button is clicked, send the data into the stored procedure

            rpCarousel.DataSource = null;
            rpCarousel.DataBind();
            int wantChildren = Int32.Parse(ddlWantKids.SelectedValue);
            int hasKids = Int32.Parse(ddlHasKids.SelectedValue);

            int ageMin = Int32.Parse(txtMinAge.Text);
            int ageMax = Int32.Parse(txtMaxAge.Text);

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
            try
            {
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
                profilesTable.Columns.Add("imageSrc", typeof(string));

                List<int> memberBlocks = (List<int>)Session["memberBlocks"];
                List<int> memberDislikes = (List<int>)Session["memberDislikes"];

                if (foundProfiles.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow profileRows in profilesTable.Rows)
                    {
                        //Check if the person is supposed to be blocked
                        if (!(memberBlocks.Contains(Int32.Parse(profileRows["userID"].ToString()))) && !(memberDislikes.Contains(Int32.Parse(profileRows["userID"].ToString()))))
                        {
                            if ((Int32.Parse(profileRows["numChildren"].ToString()) > 0 && hasKids == 0) || (Int32.Parse(profileRows["numChildren"].ToString()) == 0 && hasKids == 1))
                            {
                                //If the number of children doesn't match, delete the row
                                profileRows.Delete();
                            }
                            else if ((Int32.Parse(profileRows["wantChildren"].ToString()) == 1 && wantChildren == 0) || (Int32.Parse(profileRows["wantChildren"].ToString()) == 0 && wantChildren == 1))
                            {
                                //If the want children doesn't match, delete the row
                                profileRows.Delete();
                            }
                            else
                            {
                                //If the age isn't within the proper range, delete the row.
                                DateTime now = DateTime.Now;
                                DateTime birthday = Convert.ToDateTime(profileRows["birthday"].ToString());
                                TimeSpan timelived = now.Subtract(birthday);
                                int age = timelived.Days / 365;

                                if (age < ageMin || age > ageMax || (ageMax == ageMin && age != ageMin))
                                {
                                    profileRows.Delete();

                                }
                                else
                                {
                                    profileRows["age"] = age;

                                    Byte[] imgArray = (Byte[])profileRows["profileImage"];
                                    MemoryStream memorystreamd = new MemoryStream(imgArray);
                                    BinaryFormatter bfd = new BinaryFormatter();
                                    profileRows["imageSrc"] = (bfd.Deserialize(memorystreamd)).ToString();
                                }
                            }
                        }
                        else
                        {
                            profileRows.Delete();
                        }
                    }
                    //Set the repeated to show the profiles in a pretty carousel
                    rpCarousel.DataSource = foundProfiles;
                    rpCarousel.DataBind();
                    divNoneFound.Visible = false;
                    searchResults.Visible = true;
                    divSearchOptions.Visible = false;
                    divSearchControls.Visible = true;
                }
                else
                {
                    //If nothing was found, tell the user

                    divNoneFound.Visible = true;
                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "FailureToast", "showDBError();", true);

            }

        } // end search

        protected void btnShowSearch_Click(object sender, EventArgs e)
        {
            divSearchOptions.Visible = true;
            divSearchControls.Visible = false;
        }
    } // end class
} // end namespace