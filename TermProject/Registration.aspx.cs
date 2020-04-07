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
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TermProject
{
    public partial class Registration : System.Web.UI.Page
    {
        string webapiURL = "https://localhost:44394/api/profile/";
        DBConnect obj = new DBConnect();
        protected void Page_Load(object sender, EventArgs e)
        {
            List<int> memberLikes = new List<int>();
            List<int> memberDislikes = new List<int>();
            JavaScriptSerializer js = new JavaScriptSerializer();
            string mLikes = js.Serialize(memberLikes) ;
            string mDislikes =js.Serialize(memberDislikes);

          /*  WebRequest request = WebRequest.Create(webapiURL + "insertPreferences/");
            request.Method = "POST";
            request.ContentLength = mLikes.Length + mDislikes.Length;
            request.ContentType = "application/json";
            // Write the JSON data to the Web Request

            StreamWriter writer = new StreamWriter(request.GetRequestStream());
            writer.Write(mLikes);
            writer.Write(mDislikes);
            writer.Flush();
            writer.Close();
            // Read the data from the Web Response, which requires working with streams.

            WebResponse response = request.GetResponse();
            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            String data = reader.ReadToEnd();
            reader.Close();
            response.Close(); */
        } // end page load

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // clear styling previously applied
            lblTagline.Style.Remove("color"); lblBio.Style.Remove("color"); lblGender.Style.Remove("color");
            lblBirthday.Style.Remove("color"); lblPhotos.Style.Remove("color");
            lblOccupation.Style.Remove("color"); lblSeekingGender.Style.Remove("color");
            ddl.RemoveColor();
            Boolean check =validateForm(); // call method to validate input

            if (!check)
            { // if everything was entered correctly, transfer to participant profile
                Server.Transfer("/Profile.aspx");
            } // end if
            else
            { // else display an error messafe
                lblError.Text = "Please correct the following inputs.";
            } // end else
        } // end save event handler

        private Boolean validateForm()
        {
            Boolean check = false;
            if (ddl.LBInterest.SelectedIndex>0)
            {
                check = true; ddl.SetInterests();
            }
             if (ddl.LBLikes.SelectedIndex > 0)
            {
                check = true; ddl.SetLikes();
            }
            if (ddl.LBDislikes.SelectedIndex > 0)
            {
                check = true; ddl.SetDislikes();
            }
            if (ddl.LBCommitment.SelectedIndex > 0)
            {
                check = true; ddl.SetCommitment();
            } 
            if (ddl.LBReligion.SelectedIndex > 0)
            {
                check = true; ddl.SetReligion();
            } 
            if (txtTagline.Text=="")
            {
                check = true; lblTagline.Attributes.Add("style", "color:red");
            }
            if (txtBio.Text== "" )
            {
                check = true; lblBio.Attributes.Add("style", "color:red");
            }
            if (ddlGender.SelectedValue== "-1")
            {
                check = true; lblGender.Attributes.Add("style", "color:red");
            }
            if (txtBirthday.Text=="")
            {
                check = true; lblBirthday.Attributes.Add("style", "color:red");
            }
            if (!(chkSeekingFemale.Checked)  && !(chkSeekingMale.Checked))
            {
                check = true; lblSeekingGender.Attributes.Add("style", "color:red");
            }
            if (txtNumber1.Text.Length!=3 || txtNumber2.Text.Length!=3 || txtNumber3.Text.Length!=4)
            { // if the lengths of each textbox aren't 3-3-4
                check = true; lblPhoneNumber.Attributes.Add("style", "color:red");
            }
            else
            { // else check to see if they're all ints
                int n1; bool bN1 = Int32.TryParse(txtNumber1.Text, out n1);
                int n2; bool bN2 = Int32.TryParse(txtNumber2.Text, out n2);
                int n3; bool bN3 = Int32.TryParse(txtNumber3.Text, out n3);
                if (!(bN1 && bN2 && bN3))
                { // if any of the bools are false = invalid input
                    check = true; lblPhoneNumber.Attributes.Add("style", "color:red");
                    txtNumber1.Text= ""; txtNumber2.Text = ""; txtNumber3.Text = ""; // reset the values to empty strings
                } // end inner if 
            } // end outter else
            if (ddlOccupation.SelectedValue=="-1")
            {
                check = true; lblOccupation.Attributes.Add("style", "color:red");
            }
            return check;
        } 
    } // end class
} // end namespace