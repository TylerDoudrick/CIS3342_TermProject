using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.Script.Serialization;

namespace TermProject
{
    public partial class Profile : System.Web.UI.Page
    {
        DBConnect obj = new DBConnect();
        string interactionsWebAPI = "https://localhost:44375/api/datingservice/interactions/";
        string profileWebAPI = "https://localhost:44375/api/datingservice/profile/";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null) Response.Redirect("Default.aspx");
            if (!IsPostBack)
            {
                ddl.Visible = false;

                divEditYourDetailsControls.Visible = false;

                WebRequest request = WebRequest.Create(profileWebAPI + "1");
                WebResponse response = request.GetResponse();
                Stream theDataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(theDataStream);

                String data = reader.ReadToEnd();
                reader.Close();
                response.Close();

                DataSet result = JsonConvert.DeserializeObject<DataSet>(data);
               // Response.Write(data);

                if (result.Tables.Count > 0)
                {
                    if (result.Tables[0].Rows.Count != 1)
                    {
                        //WTF?
                    }
                    else
                    {
                        ddl = new UserControls.ddl();
                        //                userID profileID   birthday gender  occupation seekingGender   tagline profileID   phoneNumber height  weight numChildren wantChildren bio favMovies favSayings  favRestaurants favBooks    favSongs
                        //1   1   1998 - 02 - 05  Male Student Female The best in the city    1   215 - 888 - 9632    70  154 0   1   You know who else likes food and travel? Everyone else.	Bad Boys, Return of the Jedi YOLO    Outback Steakhouse  Blowout, Rogue Lawyer   Circles, Heartless
                        DataRow profile = result.Tables[0].Rows[0];
                        DataTable religion = result.Tables[1];
                        DataTable commitments = result.Tables[2];
                        DataTable interests = result.Tables[3];
                        DataTable likes = result.Tables[4];
                        DataTable dislikes = result.Tables[5];
                        txtPhoneNumber.Text = profile["phoneNumber"].ToString();
                        //txtEmail.Text = profile["email"].ToString();
                        txtBio.Text = profile["bio"].ToString();

                        //Seeking Gender
                        txtNumKids.Text = profile["numChildren"].ToString();
                        ddlWantChildren.Items.FindByValue(profile["wantChildren"].ToString()).Selected = true;
                        ddlWantChildren.Items.FindByValue(profile["wantChildren"].ToString()).Selected = true;
                        ddlSeeking.Items.FindByValue(profile["seekingGender"].ToString()).Selected = true;
                        //ddlOccupation.Items.FindByText(profile["occupation"].ToString()).Selected = true;
                        ddlOccupation.SelectedIndex = ddlOccupation.Items.IndexOf(ddlOccupation.Items.FindByText(profile["occupation"].ToString()));
                        txtFavSongs.Text = profile["favSongs"].ToString();
                        txtFavSayings.Text = profile["favSayings"].ToString();
                        txtFavRestaurants.Text = profile["favRestaurants"].ToString();
                        txtFavMovies.Text = profile["favMovies"].ToString();
                        txtFavBooks.Text = profile["favBooks"].ToString();

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

                        lblReligion.Text = strReligions.Remove(strReligions.Length - 2, 2);
                        lblCommittment.Text = strCommitments.Remove(strCommitments.Length - 2, 2);
                        lblInterests.Text = strInterests.Remove(strInterests.Length - 2, 2);
                        lblLikes.Text = strLikes.Remove(strLikes.Length - 2, 2);
                        lblDislikes.Text = strDislikes.Remove(strDislikes.Length - 2, 2);

                    }
                }
                else
                {
                    //Response.Write(data);
                }
            }
            // disable listboxes, checkboxes, and radio buttons
            //ddlOccupation.Enabled = false;
            //chkSeekingFemale.Enabled = false; chkSeekingMale.Enabled = false; rWantKidsNo.Enabled = false; rWantKidsYes.Enabled = false;
        } // end pageload

        protected void btnEditContactSubmit_Click(object sender, EventArgs e)
        {
            string phone = txtPhoneNumber.Text;
            string email = txtEmail.Text;
        }
        protected void btnEditBasicSubmit_Click(object sender, EventArgs e)
        {
            string bio = txtBio.Text;
            string numKids = txtNumKids.Text;
            string seeking = ddlSeeking.SelectedValue;
            string wantKids = ddlWantChildren.SelectedValue;
            string occupation = ddlOccupation.SelectedValue;
        }
        protected void btnEditMiscSubmit_Click(object sender, EventArgs e)
        {
            string favSongs = txtFavSongs.Text;
            string favSayings = txtFavSayings.Text;
            string favRestuarants = txtFavRestaurants.Text;
            string favMovies = txtFavMovies.Text;
            string favBooks = txtFavBooks.Text;
            string religions = "";
            string commitments = "";
            string interests = "";
            string likes = "";
            string dislikes = "";

            foreach (ListItem item in ddl.LBReligion.Items)
            {
                if (item.Selected)
                {
                    religions += item.Value;
                }
            }
            foreach (ListItem item in ddl.LBCommitment.Items)
            {
                if (item.Selected)
                {
                    commitments += item.Value;
                }
            }
            foreach (ListItem item in ddl.LBInterest.Items)
            {
                if (item.Selected)
                {
                    interests += item.Value;
                }
            }
            foreach (ListItem item in ddl.LBLikes.Items)
            {
                if (item.Selected)
                {
                    likes += item.Value;
                }
            }
            foreach (ListItem item in ddl.LBDislikes.Items)
            {
                if (item.Selected)
                {
                    dislikes += item.Value;
                }
            }
        }

        protected void btnEditYourDetails_Click(object sender, EventArgs e)
        {
            lblYourDetails.Visible = false;
            ddl.Visible = true;
            btnEditYourDetails.Visible = false;
            divEditYourDetailsControls.Visible = true;

            WebRequest request = WebRequest.Create(profileWebAPI + "1");
            WebResponse response = request.GetResponse();
            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);

            String data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            DataSet result = JsonConvert.DeserializeObject<DataSet>(data);
            // Response.Write(data);

            if (result.Tables.Count > 0)
            {
                    DataTable religion = result.Tables[1];
                    DataTable commitments = result.Tables[2];
                    DataTable interests = result.Tables[3];
                    DataTable likes = result.Tables[4];
                    DataTable dislikes = result.Tables[5];

                    //Table 1 is religion, Table 2 is Commitments, Table 3 is Interests, Table 4 is Likes, Table 5 is Dislikes

                    foreach (DataRow row in religion.Rows)
                    {
                        ddl.LBReligion.Items[ddl.LBReligion.Items.IndexOf(ddl.LBReligion.Items.FindByValue(row["religionID"].ToString()))].Selected = true;
                    }
                    foreach (DataRow row in commitments.Rows)
                    {
                        ddl.LBCommitment.Items[ddl.LBCommitment.Items.IndexOf(ddl.LBCommitment.Items.FindByValue(row["commitmentID"].ToString()))].Selected = true;
                    }

                    foreach (DataRow row in interests.Rows)
                    {
                        ddl.LBInterest.Items[ddl.LBInterest.Items.IndexOf(ddl.LBInterest.Items.FindByValue(row["interestID"].ToString()))].Selected = true;

                    }

                    foreach (DataRow row in likes.Rows)
                    {
                        ddl.LBLikes.Items[ddl.LBLikes.Items.IndexOf(ddl.LBLikes.Items.FindByValue(row["likesID"].ToString()))].Selected = true;

                    }

                    foreach (DataRow row in dislikes.Rows)
                    {
                        ddl.LBDislikes.Items[ddl.LBDislikes.Items.IndexOf(ddl.LBDislikes.Items.FindByValue(row["dislikeID"].ToString()))].Selected = true;

                    } 
            }
        }

        protected void btnEditYourDetailsCancel_Click(object sender, EventArgs e)
        {
            lblYourDetails.Visible = true;
            ddl.Visible = false;
            btnEditYourDetails.Visible = true;
            divEditYourDetailsControls.Visible = false;
        }
        protected void btnEditYourDetailsSubmit_Click(object sender, EventArgs e)
        {


            lblYourDetails.Visible = true;
            ddl.Visible = false;
            btnEditYourDetails.Visible = true;
            divEditYourDetailsControls.Visible = false;
        }
            //protected void btnEditMisc_Click(object sender, EventArgs e)
            //{ // will enable contents in favorite things + tagline
            //    txtFavBooks.ReadOnly = false; txtFavMovies.ReadOnly = false; txtFavRestaurants.ReadOnly = false;
            //    txtFavSayings.ReadOnly = false; txtFavSongs.ReadOnly = false;
            //    ddl.EnableControl();
            //    divBtnUpdate3.Attributes.Add("style", "display:flex");
            //} // end link button edit btn click

            //protected void btnEditContact_Click(object sender, EventArgs e)
            //{ // this will enable the content in contact info
            //    txtPhoneNumber.ReadOnly = false;
            //    txtEmail.ReadOnly = false;
            //    divBtnUpdate1.Attributes.Add("style", "display:flex");
            //    //btnUpdate1.CssClass.Replace("d-none", "d-block");
            //} // end edit contact

            //protected void btnEditBasicInfo_Click(object sender, EventArgs e)
            //{ // this will make the contents in basic info editable
            //    txtTagline.ReadOnly = false;
            //    txtBio.ReadOnly = false;
            //    chkSeekingFemale.Enabled = true; chkSeekingMale.Enabled = true; rWantKidsNo.Enabled = true; rWantKidsYes.Enabled = true;
            //    txtNumKids.ReadOnly = false; ddlOccupation.Enabled = true;
            //    divBtnUpdate2.Attributes.Add("style", "display:flex");
            //} // end edit basic info

            //protected void btnUpdate1_Click(object sender, EventArgs e)
            //{ // updates contact information

            //} // end update 1

            //protected void btnUpdate2_Click(object sender, EventArgs e)
            //{ // updates basic information

            //} // end update2 

            //protected void btnUpdate3_Click(object sender, EventArgs e)
            //{ // updates favorite things

            //} // end update 3

            //protected void btnCancel3_Click(object sender, EventArgs e)
            //{ // cancels editing of favorite things
            //    txtFavBooks.ReadOnly = true; txtFavMovies.ReadOnly = true; txtFavRestaurants.ReadOnly = true;
            //    txtFavSayings.ReadOnly = true; txtFavSongs.ReadOnly = true;
            //    divBtnUpdate3.Attributes.Add("style", "display:none");
            //    // disable everything
            //} // end cancel 3

            //protected void btnCancel2_Click(object sender, EventArgs e)
            //{ // cancels editing of basic information
            //    divBtnUpdate2.Attributes.Add("style", "display:none");
            //    // disable everything
            //    txtTagline.ReadOnly = true;
            //    txtBio.ReadOnly = true;
            //    chkSeekingFemale.Enabled = false; chkSeekingMale.Enabled = false; rWantKidsNo.Enabled = false; rWantKidsYes.Enabled = false;
            //    txtNumKids.ReadOnly = true;
            //} // end cancel 2

            //protected void btnCancel1_Click(object sender, EventArgs e)
            //{ // cancels editing of contact info
            //    divBtnUpdate1.Attributes.Add("style", "display:none");
            //    // disable everything
            //    txtPhoneNumber.ReadOnly = true;
            //    txtEmail.ReadOnly = true;
            //} // end cancel 1 
        } // end class
} // end namespace