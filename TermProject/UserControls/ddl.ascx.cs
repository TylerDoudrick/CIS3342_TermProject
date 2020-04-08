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

            WebRequest request = WebRequest.Create(profileWebAPI + "searchCriteria");
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
            lbReligion.DataBind();

            lbCommittment.DataSource = ds.Tables[1];
            lbCommittment.DataTextField = "CommitmentType"; lbCommittment.DataValueField = "CommitmentID";
            lbCommittment.DataBind();

            lbInterests.DataSource = ds.Tables[2];
            lbInterests.DataTextField = "InterestType"; lbInterests.DataValueField = "InterestID";
            lbInterests.DataBind();

            lbLikes.DataSource = ds.Tables[3];
            lbLikes.DataTextField = "LikeType"; lbLikes.DataValueField = "LikeID";
            lbLikes.DataBind();

            lbDislikes.DataSource = ds.Tables[4];
            lbDislikes.DataTextField = "DislikeType"; lbDislikes.DataValueField = "DislikeID";
            lbDislikes.DataBind();
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
            lblReligion.Style.Remove("color"); lblCommitment.Style.Remove("color");
            lblInterests.Style.Remove("color"); lblLikes.Style.Remove("color"); lblDislikes.Style.Remove("color");
        }

        public void SetReligion()
        {
            lblReligion.Attributes.Add("style", "color:red");
        }
        public void SetInterests()
        {
            lblInterests.Attributes.Add("style", "color:red");
        }
        public void SetLikes()
        {
            lblLikes.Attributes.Add("style", "color:red");
        }
        public void SetDislikes()
        {
            lblDislikes.Attributes.Add("style", "color:red");
        }
        public void SetCommitment()
        {
            lblCommitment.Attributes.Add("style", "color:red");
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