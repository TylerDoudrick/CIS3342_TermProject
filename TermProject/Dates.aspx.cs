using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TermProject
{
    public partial class Dates : System.Web.UI.Page
    {
        string interactionsWebAPI = "https://localhost:44375/api/datingservice/interactions/";
        string profileWebAPI = "https://localhost:44375/api/datingservice/profile/";
        int userID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null) Response.Redirect("Default.aspx");
            else
            {
                userID = Convert.ToInt32(Session["UserID"]);
                Boolean p = true; Boolean a = true; Boolean s = true; Boolean d = true;
                bind(p, a ,s ,d );
            }

        } // end page load

        protected void bind(Boolean pend, Boolean approve, Boolean Sch, Boolean dates)
        {
            WebRequest request; WebResponse response; Stream theDataStream;
            StreamReader reader; string data; JavaScriptSerializer js;
            if (pend || approve)
            {
                request = WebRequest.Create(interactionsWebAPI + "getAllDates/" + userID);
                response = request.GetResponse();
                theDataStream = response.GetResponseStream();
                reader = new StreamReader(theDataStream);
                data = reader.ReadToEnd();
                reader.Close(); response.Close();

                js = new JavaScriptSerializer();
                DataSet dsPending = JsonConvert.DeserializeObject<DataSet>(data);
                DataTable pending = dsPending.Tables[0];
                rptPending.DataSource = pending; rptPending.DataBind();

                DataTable approveDT = dsPending.Tables[1];
                rptAcceptReqs.DataSource = approveDT; rptAcceptReqs.DataBind();
            }


        }

        protected void lblDeleteReq_Command(object sender, CommandEventArgs e)
        { // this will delete the date request
            int memID = Convert.ToInt32(e.CommandName);
            IDictionary<string, string> newValues = new Dictionary<string, string>
            {
                ["sendingID"] = userID.ToString(),
                ["recID"] = memID.ToString(),
            };

            sendReq(newValues, "deleteDateReq");
       
            Boolean p = true; Boolean a = false; Boolean s = false; Boolean d = false;
            bind(p, a, s, d); // rebind that repeater
        }

        protected void lbAccept_Command(object sender, CommandEventArgs e)
        { // accept a dating request
            int memID = Convert.ToInt32(e.CommandName);
            IDictionary<string, string> newValues = new Dictionary<string, string>
            {
                ["sendingID"] = memID.ToString(),
                ["recID"] = userID.ToString(),
            };

            sendReq(newValues , "acceptReq");
        
            Boolean p = false; Boolean a = true; Boolean s = false; Boolean d = false;
            bind(p, a, s, d); // rebind that repeater
        }

        protected void lbDeny_Command(object sender, CommandEventArgs e)
        { // deny request
            int memID = Convert.ToInt32(e.CommandName);
            IDictionary<string, string> newValues = new Dictionary<string, string>
            {
                ["sendingID"] = memID.ToString(),
                ["recID"] = userID.ToString(),
            };

            sendReq(newValues, "denyReq");

            Boolean p = false; Boolean a = true; Boolean s = false; Boolean d = false;
            bind(p, a, s, d); // rebind that repeater
        } // end deny

        protected void lbIgnore_Command(object sender, CommandEventArgs e)
        {

        }

        protected void sendReq(IDictionary<string, string> vals, string method)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            String jsonValues = js.Serialize(vals);

            WebRequest request = WebRequest.Create(interactionsWebAPI + method + "/");
            request.Method = "PUT";
            request.ContentLength = jsonValues.Length;
            request.ContentType = "application/json";

            StreamWriter writer = new StreamWriter(request.GetRequestStream());
            writer.Write(jsonValues);
            writer.Flush();
            writer.Close();

            WebResponse response = request.GetResponse();
            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            String data = reader.ReadToEnd();
            reader.Close();
            response.Close();
        }

        protected void lbGotoProf_Command(object sender, CommandEventArgs e)
        { // takes you to that person's profile
            int memID = Convert.ToInt32(e.CommandName);
            Response.Redirect("MemberProfile.aspx?memberID=" + memID);
        }

        protected void lbGotoProf2_Command(object sender, CommandEventArgs e)
        {// takes you to that person's profile
            int memID = Convert.ToInt32(e.CommandName);
            Response.Redirect("MemberProfile.aspx?memberID=" + memID);
        }
    } // end class
} // end namespace