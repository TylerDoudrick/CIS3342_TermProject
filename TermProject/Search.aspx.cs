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
    public partial class Search : System.Web.UI.Page
    {
        DBConnect obj = new DBConnect();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                // the following gets the appropriate tables from the dataset and uses it to populate the ddl
                SqlCommand objSearchCriteria = new SqlCommand();
                objSearchCriteria.CommandType = System.Data.CommandType.StoredProcedure;
                objSearchCriteria.CommandText = "TP_GetSearchCriteria";
                DataSet ds = obj.GetDataSetUsingCmdObj(objSearchCriteria);

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
                lbReligion.Attributes.Add("disabled", ""); lbCommittment.Attributes.Add("disabled", "");
                lbDislikes.Attributes.Add("disabled", ""); lbLikes.Attributes.Add("disabled", ""); lbInterests.Attributes.Add("disabled", "");

            }

        } // end page load

        protected void btnReligion_Click(object sender, EventArgs e)
        {
            //lbReligion.Visible = true;
            lbReligion.Attributes.Remove("disabled");
            divSearch.Attributes.Add("style", "display:flex");
        } // end religion

        protected void lbCommitment_Click(object sender, EventArgs e)
        {
            //lbCommittment.Visible = true;
            lbCommittment.Attributes.Remove("disabled");
            divSearch.Attributes.Add("style", "display:flex");
        } // end commitment

        protected void btnLikes_Click(object sender, EventArgs e)
        {
            //lbLikes.Visible = true;
            lbLikes.Attributes.Remove("disabled");
            divSearch.Attributes.Add("style", "display:flex");
        } // end likes

        protected void btnDislikes_Click(object sender, EventArgs e)
        {
            //lbDislikes.Visible = true;
            lbDislikes.Attributes.Remove("disabled");
            divSearch.Attributes.Add("style", "display:flex");
        } //end dislikes

        protected void btnInterests_Click(object sender, EventArgs e)
        {
            //lbInterests.Visible = true;
            lbInterests.Attributes.Remove("disabled");
            divSearch.Attributes.Add("style", "display:flex");
        } // end interests

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            divResults.Attributes.Add("style", "display:flex");
        } // end search

    } // end class
} // end namespace