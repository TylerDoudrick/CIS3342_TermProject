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
    public partial class Profile : System.Web.UI.Page
    {
        DBConnect obj = new DBConnect();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedIn"] == null) Response.Redirect("Default.aspx");

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

        }
    }
}