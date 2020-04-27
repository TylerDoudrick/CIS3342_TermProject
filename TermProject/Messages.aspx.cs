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

                //Grab the users from the db for the send message modal
                try {
                    commandObj.CommandType = CommandType.StoredProcedure;
                    commandObj.CommandText = "TP_GetOtherUsers";
                    commandObj.Parameters.AddWithValue("@userID", Session["UserID"].ToString());
                    DataSet recipients = databaseObj.GetDataSetUsingCmdObj(commandObj);

                    //Remove any users from the list who are blocked.
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
                catch
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "FailureToast", "showDBError();", true);

                }
            }
            else
            {
                ddlRecipient.SelectedIndex = 0;

            }
        }



      
    }
}