using MusicStoreLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TermProject
{
    public partial class Registration : System.Web.UI.Page
    {
        DBConnect obj = new DBConnect();
        protected void Page_Load(object sender, EventArgs e)
        {
            // the following gets the appropriate tables from the dataset and uses it to populate the ddl
            SqlCommand objSearchCriteria = new SqlCommand();
            objSearchCriteria.CommandType = System.Data.CommandType.StoredProcedure;
            objSearchCriteria.CommandText = "TP_GetSearchCriteria";
            DataSet ds = obj.GetDataSetUsingCmdObj(objSearchCriteria);

            ddlReligion.DataSource = ds.Tables[0];
            ddlReligion.DataTextField = "ReligionType"; ddlReligion.DataValueField = "ReligionID";
            ddlReligion.DataBind();

            ddlCommittment.DataSource = ds.Tables[1];
            ddlCommittment.DataTextField = "CommitmentType"; ddlCommittment.DataValueField = "CommitmentID";
            ddlCommittment.DataBind();

            lbInterests.DataSource = ds.Tables[2];
            lbInterests.DataTextField = "InterestType"; lbInterests.DataValueField = "InterestID";
            lbInterests.DataBind();

            lbLikes.DataSource = ds.Tables[3];
            lbLikes.DataTextField = "LikeType"; lbLikes.DataValueField = "LikeID";
            lbLikes.DataBind();

            lbDislikes.DataSource = ds.Tables[4];
            lbDislikes.DataTextField = "DislikeType"; lbDislikes.DataValueField = "DislikeID";
            lbDislikes.DataBind(); 
        } // end page load

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // clear styling previously applied
            lblTagline.Style.Remove("color"); lblBio.Style.Remove("color"); lblGender.Style.Remove("color");
            lblBirthday.Style.Remove("color"); lblPhotos.Style.Remove("color"); lblReligion.Style.Remove("color");
            lblCommitment.Style.Remove("color"); lblOccupation.Style.Remove("color"); lblSeekingGender.Style.Remove("color");
            lblInterests.Style.Remove("color"); lblLikes.Style.Remove("color"); lblDislikes.Style.Remove("color");

            Boolean check =validateForm(); // call method to validate input

            if (!check)
            { // if everything was entered correctly, transfer to participant profile
                Server.Transfer("/Profile.aspx");
            } // end if
            else
            { // else display an error messafe
                lblError.Text = "Please correct the following inputs.";
            } // end else
        } // end save event handler

        private Boolean validateForm()
        { 
            Boolean check = false;
            if (lbInterests.SelectedValue=="")
            {
                check = true; lblInterests.Attributes.Add("style", "color:red");
            }
            if (lbLikes.SelectedValue == "")
            {
                check = true; lblLikes.Attributes.Add("style", "color:red");
            }
            if (lbDislikes.SelectedValue == "")
            {
                check = true; lblDislikes.Attributes.Add("style", "color:red");
            }
            if (ddlCommittment.SelectedValue == "-1")
            {
                check = true; lblDislikes.Attributes.Add("style", "color:red");
            }
            if (ddlReligion.SelectedValue == "-1")
            {
                check = true; lblDislikes.Attributes.Add("style", "color:red");
            }
            if (txtTagline.Text=="")
            {
                check = true; lblTagline.Attributes.Add("style", "color:red");
            }
            if (txtBio.Text== "" )
            {
                check = true; lblBio.Attributes.Add("style", "color:red");
            }
            if (ddlGender.SelectedValue== "-1")
            {
                check = true; lblGender.Attributes.Add("style", "color:red");
            }
            if (txtBirthday.Text=="")
            {
                check = true; lblBirthday.Attributes.Add("style", "color:red");
            }
            if (!(chkSeekingFemale.Checked)  && !(chkSeekingMale.Checked))
            {
                check = true; lblSeekingGender.Attributes.Add("style", "color:red");
            }
            if (txtNumber1.Text.Length!=3 || txtNumber2.Text.Length!=3 || txtNumber3.Text.Length!=4)
            { // if the lengths of each textbox aren't 3-3-4
                check = true; lblPhoneNumber.Attributes.Add("style", "color:red");
            }
            else
            { // else check to see if they're all ints
                int n1; bool bN1 = Int32.TryParse(txtNumber1.Text, out n1);
                int n2; bool bN2 = Int32.TryParse(txtNumber2.Text, out n2);
                int n3; bool bN3 = Int32.TryParse(txtNumber3.Text, out n3);
                if (!(bN1 && bN2 && bN3))
                { // if any of the bools are false = invalid input
                    check = true; lblPhoneNumber.Attributes.Add("style", "color:red");
                    txtNumber1.Text= ""; txtNumber2.Text = ""; txtNumber3.Text = ""; // reset the values to empty strings
                } // end inner if 
            } // end outter else
            if (ddlOccupation.SelectedValue=="-1")
            {
                check = true; lblOccupation.Attributes.Add("style", "color:red");
            }
            return check;
        } 
    } // end class
} // end namespace