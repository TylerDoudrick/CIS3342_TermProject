using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TermProject.UserControls
{
    public partial class ddl : System.Web.UI.UserControl
    {
        string interactionsWebAPI = "https://localhost:44375/api/datingservice/interactions/";
        string profileWebAPI = "https://localhost:44375/api/datingservice/profile/";

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["UserID"] == null)
            {
                divInterests.Visible = false;
                divLikesDislikes.Visible = false;
            }
            if (!IsPostBack)
            {
                WebRequest request = WebRequest.Create(profileWebAPI + "searchCriteria");
                request.Headers.Add("Authorization", "Bearer " + Session["token"].ToString());
                WebResponse response = request.GetResponse();
                // Read the data from the Web Response, which requires working with streams.
                Stream theDataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(theDataStream);
                String data = reader.ReadToEnd();
                reader.Close();
                response.Close();
                //JavaScriptSerializer js = new JavaScriptSerializer();
                DataSet ds = JsonConvert.DeserializeObject<DataSet>(data);

                lbReligion.DataSource = ds.Tables[0];
                lbReligion.DataTextField = "ReligionType"; lbReligion.DataValueField = "ReligionID";
                lbReligion.SelectionMode = ListSelectionMode.Multiple;
                lbReligion.DataBind();

                lbCommittment.DataSource = ds.Tables[1];
                lbCommittment.DataTextField = "CommitmentType"; lbCommittment.DataValueField = "CommitmentID";
                lbCommittment.SelectionMode = ListSelectionMode.Multiple;
                lbCommittment.DataBind();

                lbInterests.DataSource = ds.Tables[2];
                lbInterests.DataTextField = "InterestType"; lbInterests.DataValueField = "InterestID";
                lbInterests.SelectionMode = ListSelectionMode.Multiple;
                lbInterests.DataBind();

                lbLikes.DataSource = ds.Tables[3];
                lbLikes.DataTextField = "LikeType"; lbLikes.DataValueField = "LikeID";
                lbLikes.SelectionMode = ListSelectionMode.Multiple;
                lbLikes.DataBind();

                lbDislikes.DataSource = ds.Tables[4];
                lbDislikes.DataTextField = "DislikeType"; lbDislikes.DataValueField = "DislikeID";
                lbDislikes.SelectionMode = ListSelectionMode.Multiple;
                lbDislikes.DataBind();
            }
        }

        public void DisableControl()
        { // disables controls for profile page
            lbCommittment.Attributes.Add("disabled", "");
            lbReligion.Attributes.Add("disabled","");
            lbInterests.Attributes.Add("disabled", "");
            lbLikes.Attributes.Add("disabled", ""); 
            lbDislikes.Attributes.Add("disabled", "");
        } // end disable control

        public void EnableControl()
        { // enables controls when user wants to edit
            lbCommittment.Attributes.Remove("disabled");
            lbReligion.Attributes.Remove("disabled");
            lbInterests.Attributes.Remove("disabled");
            lbLikes.Attributes.Remove("disabled");
            lbDislikes.Attributes.Remove("disabled");
        } // end enable control
        
        public void RemoveColor()
        { // removes styling done for validation
            LBReligion.CssClass = LBReligion.CssClass.Replace("is-invalid", "").Trim();
            LBLikes.CssClass = LBLikes.CssClass.Replace("is-invalid", "").Trim();
            LBInterest.CssClass = LBInterest.CssClass.Replace("is-invalid", "").Trim();
            LBDislikes.CssClass = LBDislikes.CssClass.Replace("is-invalid", "").Trim();
            LBCommitment.CssClass = LBCommitment.CssClass.Replace("is-invalid", "").Trim();
        }

        public void SetReligion()
        {
            LBReligion.CssClass += " is-invalid";
        }
        public void SetInterests()
        {
           LBInterest.CssClass += " is-invalid";
        }
        public void SetLikes()
        {
           LBLikes.CssClass += " is-invalid";
        }
        public void SetDislikes()
        {
          lbDislikes.CssClass += " is-invalid";
        }
        public void SetCommitment()
        {
            LBCommitment.CssClass += " is-invalid";
        }

        public ListBox LBReligion
        {
            get { return this.lbReligion; }
        }


        public ListBox LBCommitment
        {
            get {return this.lbCommittment; }
        }
        public ListBox LBInterest
        {
            get { return this.lbInterests; }
        }
        public ListBox LBLikes
        {
            get { return this.lbLikes; }
        }
        public ListBox LBDislikes
        {
            get { return this.lbDislikes; }
        }

    }
}