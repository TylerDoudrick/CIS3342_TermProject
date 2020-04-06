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
        string webapiURL = "https://localhost:44394/api/profile/";
        protected void Page_Load(object sender, EventArgs e)
        {
            WebRequest request = WebRequest.Create(webapiURL + "searchcriteria");
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
    }
}