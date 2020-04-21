using System;
using Newtonsoft.Json;
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
using System.Runtime.Serialization.Formatters.Binary;
using Classess;

namespace TermProject
{
    public partial class Registration : System.Web.UI.Page
    {
        string interactionsWebAPI = "https://localhost:44375/api/datingservice/interactions/";
        string profileWebAPI = "https://localhost:44375/api/datingservice/profile/";
        DBConnect obj = new DBConnect();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RegisteringUserID"] == null) Response.Redirect("Dashboard.aspx");
            //ddl.ShowInterestLikesDis();
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
                Boolean opt = AddRecords();
                if (opt)
                {

                    DBConnect databaseObj = new DBConnect();
                    SqlCommand commandObj = new SqlCommand();
                    commandObj.Parameters.Clear();
                    commandObj.CommandType = CommandType.StoredProcedure;
                    commandObj.CommandText = "TP_LookupUserRecordByID";

                    SqlParameter inputUsername = new SqlParameter("@UserID", Session["RegisteringUserID"])
                    {
                        Direction = ParameterDirection.Input,

                        SqlDbType = SqlDbType.VarChar
                    };

                    commandObj.Parameters.Add(inputUsername);


                    DataSet dsUser = databaseObj.GetDataSetUsingCmdObj(commandObj);
                    if (dsUser.Tables[0].Rows.Count > 0)
                    {
                        DataRow foundAccount = dsUser.Tables[0].Rows[0];
                        Session["email"] = foundAccount["emailAddress"];
                        Session["UserID"] = foundAccount["userID"];
                        if (dsUser.Tables[1].Rows.Count > 0) Session["seeking"] = dsUser.Tables[1].Rows[0]["seekingGender"];
                        Session["firstName"] = foundAccount["firstName"];
                        Session["lastName"] = foundAccount["lastName"];
                        getPrefs(Int32.Parse(foundAccount["userID"].ToString()));
                        GetAcceptedDates(Int32.Parse(foundAccount["userID"].ToString()));
                        Response.Redirect("Dashboard.aspx");
                    }
                }
            } // end if
            else return;
        } // end save event handler

        List<string> religions = new List<string>();
        List<string> commitments = new List<string>();
        List<string> interests = new List<string>();
        List<string> likes = new List<string>();
        List<string> dislikes = new List<string>();
        

        private Boolean validateForm()
        {
            // clear all validation
            txtTagline.CssClass = txtTagline.CssClass.Replace("is-invalid", "").Trim();
            txtBio.CssClass = txtBio.CssClass.Replace("is-invalid", "").Trim();
            ddlGender.CssClass = ddlGender.CssClass.Replace("is-invalid", "").Trim();
            txtBirthday.CssClass = txtBirthday.CssClass.Replace("is-invalid", "").Trim();
            ddlSeeking.CssClass = ddlSeeking.CssClass.Replace("is-invalid", "").Trim();
            txtNumber1.CssClass = txtNumber1.CssClass.Replace("is-invalid", "").Trim();
            txtNumber2.CssClass = txtNumber2.CssClass.Replace("is-invalid", "").Trim();
            txtNumber3.CssClass = txtNumber3.CssClass.Replace("is-invalid", "").Trim();
            ddlOccupation.CssClass = ddlOccupation.CssClass.Replace("is-invalid", "").Trim();
            photoUpload.CssClass = photoUpload.CssClass.Replace("is-invalid", "").Trim();

            Boolean check = false;

            foreach (ListItem item in ddl.LBReligion.Items)
            {
                if (item.Selected)
                {
                    religions.Add(item.Value);
                }
            }
            foreach (ListItem item in ddl.LBCommitment.Items)
            {
                if (item.Selected)
                {
                    commitments.Add(item.Value);
                }
            }
            foreach (ListItem item in ddl.LBInterest.Items)
            {
                if (item.Selected)
                {
                    interests.Add(item.Value);
                }
            }
            foreach (ListItem item in ddl.LBLikes.Items)
            {
                if (item.Selected)
                {
                    likes.Add(item.Value);
                }
            }
            foreach (ListItem item in ddl.LBDislikes.Items)
            {
                if (item.Selected)
                {
                    dislikes.Add(item.Value);
                }
            }

            // check if at least one item was selected from each ddl
            if (religions.Count == 0)
            {
                check = true; ddl.SetReligion();
            }
            if (commitments.Count==0)
            {
                check = true; ddl.SetCommitment();
            }
            if (interests.Count == 0)
            {
                check = true; ddl.SetInterests();
            }
            if (likes.Count==0)
            {
                check = true; ddl.SetLikes();
            }
            if (dislikes.Count==0)
            {
                check = true; ddl.SetDislikes();
            }

            // validate other fields
            if (txtTagline.Text=="")
            {
                check = true; txtTagline.CssClass += " is-invalid";
            }
            if (txtBio.Text== "" )
            {
                check = true; txtBio.CssClass += " is-invalid";
            }
            if (ddlGender.SelectedValue== "-1")
            {
                check = true; ddlGender.CssClass += " is-invalid";
            }
            if (txtBirthday.Text=="")
            {
                check = true; txtBirthday.CssClass += " is-invalid";
            }
            if (ddlSeeking.SelectedValue== "-1")
            {
                check = true; ddlSeeking.CssClass += " is-invalid";
            }
            if (txtNumber1.Text.Length!=3 || txtNumber2.Text.Length!=3 || txtNumber3.Text.Length!=4)
            { // if the lengths of each textbox aren't 3-3-4
                check = true;
                txtNumber1.CssClass += " is-invalid"; txtNumber2.CssClass += " is-invalid"; txtNumber3.CssClass += " is-invalid";
            }
            else
            { // else check to see if they're all ints
                int n1; bool bN1 = Int32.TryParse(txtNumber1.Text, out n1);
                int n2; bool bN2 = Int32.TryParse(txtNumber2.Text, out n2);
                int n3; bool bN3 = Int32.TryParse(txtNumber3.Text, out n3);
                if (!bN1)
                {
                    txtNumber1.CssClass += " is-invalid";
                    txtNumber1.Text = "";
                }
                if (!bN2)
                {
                    txtNumber2.CssClass += " is-invalid";
                    txtNumber2.Text = "";
                }
                if (!bN3)
                {
                    txtNumber3.CssClass += " is-invalid"; txtNumber3.Text = "";
                }              
            } // end outter else
            if (ddlOccupation.SelectedValue=="-1")
            {
                check = true; ddlOccupation.CssClass += " is-invalid";
            }
            if (photoUpload.FileName == "")
            {
                check = true; photoUpload.CssClass += "is-invalid";
            }
            return check;
        }

        private Boolean AddRecords()
        {

            // search criteria --> all 5 tables
            IDictionary<string, List<string>> newValues = new Dictionary<string, List<string>>
            {
                ["religions"] = religions,
                ["commitments"] = commitments,
                ["interests"] = interests,
                ["likes"] = likes,
                ["dislikes"] = dislikes,
            };
            JavaScriptSerializer js = new JavaScriptSerializer();
            String jsonValues = js.Serialize(newValues);
          
                WebRequest request = WebRequest.Create(profileWebAPI + "update/details/" + Session["RegisteringUserID"].ToString());
                request.Headers.Add("Authorization", "Bearer " + Session["token"].ToString());

                request.Method = "POST";
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
                if (data == "true")
                {
                    showSuccessToast();
                }
                else
                {
                    showFailureToast();
                }
            

            


            RegistrationObj reg = new RegistrationObj();
            //reg.id = 10031;
            reg.id = Convert.ToInt32(Session["RegisteringUserID"].ToString());
            // profile photo table - serialized image
            string p = photoUpload.FileName;
        //    string ph = "images/person49.jpg";
            BinaryFormatter serializer = new BinaryFormatter();
            MemoryStream memStream = new MemoryStream();
            serializer.Serialize(memStream, p);
            Byte[] imgArray = memStream.ToArray();
            reg.photo = imgArray;

            //profile private table
            string phoneNumber = txtNumber1.Text.ToString().Trim() + "-" + txtNumber2.Text.ToString().Trim() + "-" + txtNumber3.Text.ToString().Trim();
            reg.phone = phoneNumber;

            int totalHeight;
            if (txtHeightFT.Text != "")
            {
                int n; bool bN3 = Int32.TryParse(txtHeightFT.Text, out n);
                if (bN3 && txtHeightIn.Text != "")
                {
                    int inch; bN3 = Int32.TryParse(txtHeightIn.Text, out inch);
                    if (bN3)
                    {
                        totalHeight = (12 * n) + inch; reg.height = totalHeight;
                    }
                }
                else if (bN3)
                {
                    totalHeight = (12 * n); reg.height = totalHeight; // height is stored in inches
                }
            }
            else
            {
                reg.height = 0;
            }

            int weight;
            if (txtWeight.Text != "")
            {
                int n; bool bN3 = Int32.TryParse(txtHeightFT.Text, out n);
                if (bN3)
                {
                    weight = n; reg.weight = weight;
                }
            }
            else
            {
                reg.weight = 0;
            }
            int numChildren;
            if (txtNumKids.Text != "")
            {
                int n; bool bN3 = Int32.TryParse(txtHeightFT.Text, out n);
                if (bN3)
                {
                    numChildren = n; reg.numChildren = numChildren;
                }
            }
            else
            {
                reg.numChildren = 0;
            }
            int wantKids;
            if (ddlWantChildren.SelectedValue == "1")
            {
                wantKids = 1; reg.wantKids = wantKids;
            }
            else
            {
                wantKids = 0; reg.wantKids = wantKids;
            }

            string bio = txtBio.Text; reg.bio = bio;
            string favMovies = txtFavMovies.Text; reg.movies = favMovies;
            string favSayings = txtFavSayings.Text; reg.sayings = favSayings;
            string favRestaurants = txtFavRestaurants.Text; reg.restaurants = favRestaurants;
            string favBooks = txtFavBooks.Text; reg.books = favBooks;
            string favSongs = txtFavSongs.Text; reg.songs = favSongs;

            // profile public table
            string birthday = txtBirthday.Text; reg.birthday = birthday;
            string gender = ddlGender.SelectedValue; reg.gender = gender;
            string occupation = ddlOccupation.SelectedValue; reg.occupation = occupation;
            string seekingGender = ddlSeeking.SelectedValue; reg.seekingGender = seekingGender;
            string tagline = txtTagline.Text; reg.tagline = tagline;

            string jsonR = js.Serialize(reg);

            
                WebRequest r = WebRequest.Create(profileWebAPI + "insert/registrationInfo");
                r.Headers.Add("Authorization", "Bearer " + Session["token"].ToString());

                r.Method = "POST";
                r.ContentLength = jsonR.Length;
                r.ContentType = "application/json";

                // Write the JSON data to the Web Request           
                StreamWriter w = new StreamWriter(r.GetRequestStream());
                w.Write(jsonR);
                w.Flush();
                w.Close();

                WebResponse rps = r.GetResponse();
                Stream tDS = rps.GetResponseStream();
                StreamReader rder = new StreamReader(tDS);
                String d = rder.ReadToEnd();
                rder.Close(); rps.Close();

                if (d == "pass")
                {
                    return true;
                }
                return false;
            
            
             
        }
        protected void getPrefs(int userID)
        {
            SqlCommand commandObj = new SqlCommand();
            commandObj.Parameters.Clear();
            commandObj.CommandType = CommandType.StoredProcedure;
            commandObj.CommandText = "TP_GetPreferences";

            SqlParameter uid = new SqlParameter("@userID", userID)
            {
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.VarChar
            };

            commandObj.Parameters.Add(uid);

            DBConnect OBJ = new DBConnect();
            DataSet ds = OBJ.GetDataSetUsingCmdObj(commandObj);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow test = ds.Tables[0].Rows[0];
                //Response.Write(test["memberLikes"]);
                Byte[] testarray = (Byte[])test["memberLikes"];
                MemoryStream memorystreamd = new MemoryStream(testarray);
                BinaryFormatter bfd = new BinaryFormatter();
                List<int> memberLikes = bfd.Deserialize(memorystreamd) as List<int>;

                Byte[] test2 = (Byte[])ds.Tables[0].Rows[0][1];
                MemoryStream m2 = new MemoryStream(test2);
                BinaryFormatter bfd2 = new BinaryFormatter();
                List<int> memberDislikes = bfd2.Deserialize(m2) as List<int>;

                Byte[] test3 = (Byte[])ds.Tables[0].Rows[0][1];
                MemoryStream m3 = new MemoryStream(test3);
                BinaryFormatter bfd3 = new BinaryFormatter();
                List<int> memberBlocks = bfd3.Deserialize(m3) as List<int>;
                Session["memberLikes"] = memberLikes;
                Session["memberDislikes"] = memberDislikes;
                Session["memberBlocks"] = memberBlocks;
            } // end if
        } // end method

        protected void GetAcceptedDates(int userID)
        { // if there's a successeful login, this will get all accepted dates so personal information can be made avaiable for those users.
            WebRequest request = WebRequest.Create(interactionsWebAPI + "getAcceptedDates/" + userID);
            request.Headers.Add("Authorization", "Bearer " + Session["token"].ToString());
            WebResponse response = request.GetResponse();
            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            String data = reader.ReadToEnd();
            reader.Close(); response.Close();

            JavaScriptSerializer js = new JavaScriptSerializer();
            DataSet ds = JsonConvert.DeserializeObject<DataSet>(data);
            DataTable one = ds.Tables[0]; DataTable two = ds.Tables[1];

            List<int> acceptedDates = new List<int>();

            for (int i = 0; i < one.Rows.Count; i++)
            {
                int id = Convert.ToInt32(one.Rows[i]["userID"]);
                acceptedDates.Add(id);
            }
            for (int i = 0; i < two.Rows.Count; i++)
            {
                int id = Convert.ToInt32(two.Rows[i]["userID"]);
                acceptedDates.Add(id);
            }
            Session["acceptedDates"] = acceptedDates;
        } // end get accepted dates
        protected void showSuccessToast()
        {
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessToast", "showSuccess();", true);
        }

        protected void showFailureToast()
        {
            ClientScript.RegisterStartupScript(this.GetType(), "FailureToast", "showFailed();", true);

        }
    } // end class
} // end namespace