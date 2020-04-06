using MusicStoreLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TermProject
{
    public partial class Profile : System.Web.UI.Page
    {
        DBConnect obj = new DBConnect();
        string webapiURL = "https://localhost:44394/api/profile/";
        protected void Page_Load(object sender, EventArgs e)
        {
            ddl.DisableControl();
            // disable lsitboxes, checkboxes, and radio buttons
            ddlOccupation.Enabled = false;
            chkSeekingFemale.Enabled = false; chkSeekingMale.Enabled = false; rWantKidsNo.Enabled = false; rWantKidsYes.Enabled = false;
        } // end pageload

        protected void lbEdit_Click(object sender, EventArgs e)
        { // will enable contents in favorite things + tagline
            txtFavBooks.ReadOnly = false; txtFavMovies.ReadOnly = false; txtFavRestaurants.ReadOnly = false;
            txtFavSayings.ReadOnly = false; txtFavSongs.ReadOnly = false;
            ddl.EnableControl();
            divBtnUpdate3.Attributes.Add("style", "display:flex");
        } // end link button edit btn click

        protected void lblEditContact_Click(object sender, EventArgs e)
        { // this will enable the content in contact info
            txtNumber1.ReadOnly = false; txtNumber2.ReadOnly = false; txtNumber3.ReadOnly = false;
            txtEmail.ReadOnly = false;
            divBtnUpdate1.Attributes.Add("style", "display:flex");
            //btnUpdate1.CssClass.Replace("d-none", "d-block");
        } // end edit contact

        protected void lbEditBasicInfo_Click(object sender, EventArgs e)
        { // this will make the contents in basic info editable
            txtTagline.ReadOnly = false;
            txtBio.ReadOnly = false;
            chkSeekingFemale.Enabled = true; chkSeekingMale.Enabled = true; rWantKidsNo.Enabled = true; rWantKidsYes.Enabled = true;
            txtNumKids.ReadOnly = false; ddlOccupation.Enabled = true;
            divBtnUpdate2.Attributes.Add("style", "display:flex");
        } // end edit basic info

        protected void btnUpdate1_Click(object sender, EventArgs e)
        { // updates contact information

        } // end update 1

        protected void btnUpdate2_Click(object sender, EventArgs e)
        { // updates basic information

        } // end update2 

        protected void btnUpdate3_Click(object sender, EventArgs e)
        { // updates favorite things

        } // end update 3

        protected void btnCancel3_Click(object sender, EventArgs e)
        { // cancels editing of favorite things
            txtFavBooks.ReadOnly = true; txtFavMovies.ReadOnly = true; txtFavRestaurants.ReadOnly = true;
            txtFavSayings.ReadOnly = true; txtFavSongs.ReadOnly = true;
            divBtnUpdate3.Attributes.Add("style", "display:none");
            // disable everything
        } // end cancel 3

        protected void btnCancel2_Click(object sender, EventArgs e)
        { // cancels editing of basic information
            divBtnUpdate2.Attributes.Add("style", "display:none");
            // disable everything
            txtTagline.ReadOnly = true;
            txtBio.ReadOnly = true;
            chkSeekingFemale.Enabled = false; chkSeekingMale.Enabled = false; rWantKidsNo.Enabled = false; rWantKidsYes.Enabled = false;
            txtNumKids.ReadOnly = true;
        } // end cancel 2

        protected void btnCancel1_Click(object sender, EventArgs e)
        { // cancels editing of contact info
            divBtnUpdate1.Attributes.Add("style", "display:none");
            // disable everything
            txtNumber1.ReadOnly = true; txtNumber2.ReadOnly = true; txtNumber3.ReadOnly = true;
            txtEmail.ReadOnly = true;
        } // end cancel 1 
    } // end class
} // end namespace