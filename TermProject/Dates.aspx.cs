﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
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
                userID = Convert.ToInt32(Session["UserID"].ToString());

                if (!IsPostBack)
                {
                    Boolean p = true; Boolean a = true; Boolean s = true; Boolean d = true;
                    bind(p, a, s, d);

                }
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


                DataTable ignoreDT = dsPending.Tables[2];
              //  rptIgnoredReqs.DataSource = ignoreDT; rptIgnoredReqs.DataBind();
            }
            
            if (Sch || dates)
            { // display the dates that need to be scheduled
                request = WebRequest.Create(interactionsWebAPI + "getAcceptedDates/" + userID);
                response = request.GetResponse();
                theDataStream = response.GetResponseStream();
                reader = new StreamReader(theDataStream);
                data = reader.ReadToEnd();
                reader.Close(); response.Close();

                js = new JavaScriptSerializer();
                DataSet dsAccepted = JsonConvert.DeserializeObject<DataSet>(data);
                DataTable a = dsAccepted.Tables[0];

                lvSchedule.DataSource = a;
                //String[] names = new String[2];  names[0] = "userID"; names[1] = "userName";
                //  lvSchedule.DataKeyNames = names;
                lvSchedule.DataBind();

                DataTable scheduledDates = dsAccepted.Tables[1];
                String[] keys = new string[1]; keys[0] = "userName";
                lvPlannedDates.DataSource = scheduledDates; lvPlannedDates.DataBind();
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

            sendReq(newValues, "acceptReq");

            Boolean p = true; Boolean a = true; Boolean s = true; Boolean d = false;
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
            int memID = Convert.ToInt32(e.CommandName);
            IDictionary<string, string> newValues = new Dictionary<string, string>
            {
                ["sendingID"] = memID.ToString(),
                ["recID"] = userID.ToString(),
            };

            sendReq(newValues, "ignoreReq");

            Boolean p = true; Boolean a = true; Boolean s = false; Boolean d = false;
            bind(p, a, s, d); // rebind that repeater
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

        protected void btnShowDate_Command(object sender, CommandEventArgs e)
        {
           // divDates.Attributes.Add("style", "display:flex");
            int memID = Convert.ToInt32(e.CommandName);
            string name = "";
            Session["memID"] = memID;
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#modalScheduleDate').modal('show');", true);

        }

        protected void Unnamed_Click(object sender, EventArgs e)
        { // hides the set up date card
            //divDates.Attributes.Add("style","display:none");
        }


        protected void btnSave_Click(object sender, EventArgs e)
        { // saves the 
            int memberID = Convert.ToInt16(Session["memID"]);

            string dt = txtWhen.Text;
            string location = txtLocation.Text;
            string desc = txtDesc.Text;

            // remove validation class 
            txtWhen.CssClass = txtWhen.CssClass.Replace("is-invalid", "").Trim();
            txtLocation.CssClass = txtLocation.CssClass.Replace("is-invalid", "").Trim();
            txtDesc.CssClass = txtDesc.CssClass.Replace("is-invalid", "").Trim();

            // validate
            Boolean trigger=false;

            if (dt == "")
            {
                trigger = true;
                txtWhen.CssClass += " is-invalid";
            }

            if (location=="")
            {
                trigger = true;
                txtLocation.CssClass += " is-invalid"; 
            }
            if (desc == "")

            {
                trigger = true;
                txtDesc.CssClass += " is-invalid";
            }

            if (!trigger)
            { // if everything is entered - update the db
                IDictionary<string, string> newValues = new Dictionary<string, string>
                {
                    ["sendingID"] = userID.ToString(),
                    ["recID"] = memberID.ToString(),
                    ["dt"] = dt,
                    ["location"] = location,
                    ["desc"] = desc
                };

                JavaScriptSerializer js = new JavaScriptSerializer();
                string values = js.Serialize(newValues);

                WebRequest request = WebRequest.Create(interactionsWebAPI + "insertDate/");
                request.Method = "POST";
                request.ContentLength = values.Length;
                request.ContentType = "application/json";

                StreamWriter writer = new StreamWriter(request.GetRequestStream());
                writer.Write(values);
                writer.Flush();
                writer.Close();

                WebResponse response = request.GetResponse();
                Stream theDataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(theDataStream);
                String data = reader.ReadToEnd();
                reader.Close();
                response.Close();

                Boolean p = false; Boolean a = false; Boolean s = true; Boolean d = true;
                bind(p, a, s, d); // rebind that repeater

                //divDates.Attributes.Add("style", "display:none");

            }
        } // end save button click

        protected void lvPlannedDates_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            lvPlannedDates.EditIndex = e.NewEditIndex;

            Label id = lvPlannedDates.Items[lvPlannedDates.EditIndex].FindControl("lblIDHidden") as Label;
            int memberID = Convert.ToInt16(id.Text);
            Session["memID"] = memberID;
            Boolean p = false; Boolean a = false; Boolean s = false; Boolean d = true;
            bind(p, a, s, d);
        }

        protected void lvPlannedDates_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            lvPlannedDates.EditIndex = -1;
            Boolean p = false; Boolean a = false; Boolean s = false; Boolean d = true;
            bind(p, a, s, d);
        }

        protected void lvPlannedDates_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            int rowIndex = e.ItemIndex;
            int memberID = Convert.ToInt16(Session["memID"]);

            TextBox Date = lvPlannedDates.Items[rowIndex].FindControl("txtWhenEdit") as TextBox ;
            string dt = Date.Text;
            TextBox loc = lvPlannedDates.Items[rowIndex].FindControl("txtLocationEdit") as TextBox;
            string location = loc.Text;
            TextBox description = lvPlannedDates.Items[rowIndex].FindControl("txtDescEdit") as TextBox;
            string desc = description.Text;

            Date.CssClass = txtWhen.CssClass.Replace("is-invalid", "").Trim();
            loc.CssClass = txtLocation.CssClass.Replace("is-invalid", "").Trim();
            description.CssClass = txtDesc.CssClass.Replace("is-invalid", "").Trim();

            // validate text
            Boolean trigger = false;
            if (dt == "")
            {
                trigger = true;
                Date.CssClass += " is-invalid";
            }

            if (location == "")
            {
                trigger = true;
                loc.CssClass += " is-invalid"; 
            }
            if (desc == "")
            {
                trigger = true;
                description.CssClass += " is-invalid";
            }

            //  update request
            if (!trigger)
            { // if everything is entered correctly
                IDictionary<string, string> newValues = new Dictionary<string, string>
                {
                    ["sendingID"] = userID.ToString(),
                    ["recID"] = memberID.ToString(),
                    ["dt"] = dt,
                    ["location"] = location,
                    ["desc"] = desc
                };

                sendReq(newValues, "updateDate");

            }
            //Boolean p = false; Boolean a = false; Boolean s = false; Boolean d = true;
            //bind(p, a, s, d);
        } // end updating command for lvScheduled

        protected void lbShowPendingISent_Click(object sender, EventArgs e)
        { // show responses I am waiting for
            divpendingreqs.Attributes.Add("style", "display:flex");
            divReqsToApprove.Attributes.Add("style", "display:none");
            divplanneddates.Attributes.Add("style", "display:none");
            divScheduleDates.Attributes.Add("style", "display:none");

            lbShowPendingISent.CssClass = "btn theme-red";
            lbShowDates.CssClass = "btn theme-white";
            lbShowDatestbScheduled.CssClass = "btn theme-white";
            lbShowPendingRecieved.CssClass = "btn theme-white";
        }

        protected void lbShowPendingRecieved_Click(object sender, EventArgs e)
        { // show requests I have received
            divReqsToApprove.Visible=true;
            divplanneddates.Attributes.Add("style", "display:none");
            divScheduleDates.Attributes.Add("style", "display:none");
            divpendingreqs.Attributes.Add("style", "display:none");

            lbShowPendingRecieved.CssClass = "btn theme-red";
            lbShowPendingISent.CssClass = "btn theme-white";
            lbShowDates.CssClass = "btn theme-white";
            lbShowDatestbScheduled.CssClass = "btn theme-white";
        }

        protected void lbShowDatestbScheduled_Click(object sender, EventArgs e)
        { // show dates that need to be scheduled
            divScheduleDates.Attributes.Add("style", "display:flex");
            divReqsToApprove.Attributes.Add("style", "display:none");
            divpendingreqs.Attributes.Add("style", "display:none");
            divplanneddates.Attributes.Add("style", "display:none");

            lbShowDatestbScheduled.CssClass = "btn theme-red";
            lbShowPendingRecieved.CssClass = "btn theme-white";
            lbShowPendingISent.CssClass = "btn theme-white";
            lbShowDates.CssClass = "btn theme-white";
        }

        protected void lbShowDates_Click(object sender, EventArgs e)
        { // show scheduled dates
            divplanneddates.Attributes.Add("style", "display:flex");
            divReqsToApprove.Attributes.Add("style", "display:none");
            divpendingreqs.Attributes.Add("style", "display:none");
            divScheduleDates.Attributes.Add("style", "display:none");

            lbShowDates.CssClass = "btn theme-red";
            lbShowPendingISent.CssClass = "btn theme-white";
            lbShowPendingISent.CssClass = "btn theme-white";
            lbShowDatestbScheduled.CssClass = "btn theme-white";
        }

    } // end class
} // end namespace