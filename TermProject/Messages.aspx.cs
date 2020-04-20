using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TermProject
{
    public partial class Messages : System.Web.UI.Page
    {
        string interactionsWebAPI = "https://localhost:44375/api/datingservice/interactions/";
        string profileWebAPI = "https://localhost:44375/api/datingservice/profile/";

        DBConnect databaseObj = new DBConnect();
        SqlCommand commandObj = new SqlCommand();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null) Response.Redirect("LogIn.aspx?target=Messages");

            if (!IsPostBack)
            {
                commandObj.CommandType = CommandType.StoredProcedure;
                commandObj.CommandText = "TP_GetOtherUsers";
                commandObj.Parameters.AddWithValue("@userID", Session["UserID"].ToString());
                DataSet recipients = databaseObj.GetDataSetUsingCmdObj(commandObj);

                List<int> memberBlocks = (List<int>)Session["memberBlocks"];
                foreach (DataRow row in recipients.Tables[0].Rows)
                {
                    if ((memberBlocks.Contains(Int32.Parse(row["userID"].ToString()))))
                    {
                        row.Delete();
                    }
                }
                ddlRecipient.DataSource = recipients;
                ddlRecipient.DataValueField = "userID";
                ddlRecipient.DataTextField = "name";
                ddlRecipient.DataBind();
                ddlRecipient.Items.Insert(0, new ListItem("Please select a recipient...", String.Empty));
                ddlRecipient.SelectedIndex = 0;
            }
            else
            {
                ddlRecipient.SelectedIndex = 0;

            }
        }



        //protected void ddlRecipient_Change(object sender, EventArgs e)
        //{
        //    if (ddlRecipient.SelectedIndex != 0)
        //    {
        //        int selectedID = Int32.Parse(ddlRecipient.SelectedValue);

        //        bool found = false;

        //        commandObj.CommandType = CommandType.StoredProcedure;
        //        commandObj.CommandText = "TP_GetOtherUsers";
        //        commandObj.Parameters.AddWithValue("@userID", Session["UserID"].ToString());
        //        DataSet recipients = databaseObj.GetDataSetUsingCmdObj(commandObj);

        //        foreach (DataRow row in recipients.Tables[0].Rows)
        //        {
        //            if (Int32.Parse(row["userID"].ToString()) == selectedID)
        //            {
        //                found = true;
        //                Byte[] imgArray = (Byte[])row["image"];
        //                MemoryStream memorystreamd = new MemoryStream(imgArray);
        //                BinaryFormatter bfd = new BinaryFormatter();
        //                imgRecipient.ImageUrl = (bfd.Deserialize(memorystreamd)).ToString();
        //                lblRecipientName.Text = row["name"].ToString();
        //                lblRecipientLocation.Text = row["location"].ToString();
        //            }
        //        }
        //        if (found)
        //        {
        //            divProfile.Visible = true;
        //            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#modalSendMessage').removeClass('fade');$('#modalSendMessage').modal('show');", true);
        //        }
        //        else
        //        {
        //            divProfile.Visible = false;
        //        }
        //    }
        //    else
        //    {
        //        divProfile.Visible = false;
        //    }
        //}
    }
}