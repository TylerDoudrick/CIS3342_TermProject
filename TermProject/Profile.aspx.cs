using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace TermProject
{
    public partial class Profile : System.Web.UI.Page
    {
        DBConnect obj = new DBConnect();
        string interactionsWebAPI = "https://localhost:44375/api/datingservice/interactions/";
        string profileWebAPI = "https://localhost:44375/api/datingservice/profile/";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null) Response.Redirect("LogIn.aspx?target=Profile");
            if (!IsPostBack)
            {
                //
                //Set some attributes for controls
                //
                ddl.Visible = false;

                txtTagline.Attributes.Add("readonly", "readonly");

                txtEmail.Attributes.Add("readonly", "readonly");
                txtPhoneNumber.Attributes.Add("readonly", "readonly");

                txtBio.Attributes.Add("readonly", "readonly");
                txtNumKids.Attributes.Add("readonly", "readonly");

                txtFavSongs.Attributes.Add("readonly", "readonly");
                txtFavSayings.Attributes.Add("readonly", "readonly");
                txtFavRestaurants.Attributes.Add("readonly", "readonly");
                txtFavMovies.Attributes.Add("readonly", "readonly");
                txtFavBooks.Attributes.Add("readonly", "readonly");

                divEditYourDetailsControls.Visible = false;
                try
                {
                    grabPersonalProfile();
                }
                catch
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "FailureToast", "showDBError();", true);

                }
            }
        } // end pageload

        protected void grabPersonalProfile()
        {
            //Grab the profile from the api

            WebRequest request = WebRequest.Create(profileWebAPI + Session["UserID"].ToString());
            request.Headers.Add("Authorization", "Bearer " + Session["token"].ToString());
            WebResponse response = request.GetResponse();
            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);

            String data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            DataSet result = JsonConvert.DeserializeObject<DataSet>(data);

            if (result.Tables.Count > 0)
            {
                if (result.Tables[0].Rows.Count != 1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "FailureToast", "showDBError();", true);
                }
                else
                {
                    //
                    //Fill the controls with the profile information
                    //
                    ddl = new UserControls.ddl();
                    DataRow profile = result.Tables[0].Rows[0];
                    DataTable religion = result.Tables[1];
                    DataTable commitments = result.Tables[2];
                    DataTable interests = result.Tables[3];
                    DataTable likes = result.Tables[4];
                    DataTable dislikes = result.Tables[5];

                    DataRow image = result.Tables[6].Rows[0]; // get image 
                    string x = image[0] as string;
                    //    Byte[] imgArray = Convert.FromBase64String(base64);
                    Byte[] imgArray = Encoding.ASCII.GetBytes(x);

                    //printing characters with byte values
                    MemoryStream memorystreamd = new MemoryStream(imgArray);
                    BinaryFormatter bfd = new BinaryFormatter();
                    string url = (bfd.Deserialize(memorystreamd)).ToString();
                    img.ImageUrl = url;

                    DateTime now = DateTime.Now;
                    DateTime birthday = Convert.ToDateTime(profile["birthday"].ToString());
                    TimeSpan timelived = now.Subtract(birthday);
                    int age = timelived.Days / 365;
                    lblName.Text = profile["firstName"].ToString() + " " + profile["lastName"].ToString() + ", " + age.ToString();
                    lblLocation.Text = profile["city"].ToString() + ", " + profile["state"].ToString();
                    txtTagline.Text = profile["tagline"].ToString();


                    txtPhoneNumber.Text = profile["phoneNumber"].ToString();
                    txtEmail.Text = profile["emailAddress"].ToString();
                    txtBio.Text = profile["bio"].ToString();

                    //Seeking Gender
                    txtNumKids.Text = profile["numChildren"].ToString();
                    ddlWantChildren.Items.FindByValue(profile["wantChildren"].ToString()).Selected = true;
                    ddlWantChildren.Items.FindByValue(profile["wantChildren"].ToString()).Selected = true;
                    ddlSeeking.Items.FindByValue(profile["seekingGender"].ToString()).Selected = true;

                    ddlOccupation.SelectedIndex = ddlOccupation.Items.IndexOf(ddlOccupation.Items.FindByText(profile["occupation"].ToString()));
                    txtFavSongs.Text = profile["favSongs"].ToString();
                    txtFavSayings.Text = profile["favSayings"].ToString();
                    txtFavRestaurants.Text = profile["favRestaurants"].ToString();
                    txtFavMovies.Text = profile["favMovies"].ToString();
                    txtFavBooks.Text = profile["favBooks"].ToString();

                    //Table 1 is religion, Table 2 is Commitments, Table 3 is Interests, Table 4 is Likes, Table 5 is Dislikes
                    string strReligions = "";
                    string strCommitments = "";
                    string strInterests = "";
                    string strLikes = "";
                    string strDislikes = "";

                    foreach (DataRow row in religion.Rows)
                    {
                        strReligions += row["ReligionType"].ToString();
                        strReligions += ", ";

                    }
                    foreach (DataRow row in commitments.Rows)
                    {
                        strCommitments += row["CommitmentType"].ToString();
                        strCommitments += ", ";
                    }

                    foreach (DataRow row in interests.Rows)
                    {
                        strInterests += row["InterestType"].ToString();
                        strInterests += ", ";
                    }

                    foreach (DataRow row in likes.Rows)
                    {
                        strLikes += row["LikeType"].ToString();
                        strLikes += ", ";
                    }

                    foreach (DataRow row in dislikes.Rows)
                    {
                        strDislikes += row["DislikeType"].ToString();
                        strDislikes += ", ";
                    }


                    if (strReligions.Length > 0) lblReligion.Text = strReligions.Remove(strReligions.Length - 2, 2);
                    if (strCommitments.Length > 0) lblCommittment.Text = strCommitments.Remove(strCommitments.Length - 2, 2);
                    if (strInterests.Length > 0) lblInterests.Text = strInterests.Remove(strInterests.Length - 2, 2);
                    if (strLikes.Length > 0) lblLikes.Text = strLikes.Remove(strLikes.Length - 2, 2);
                    if (strDislikes.Length > 0) lblDislikes.Text = strDislikes.Remove(strDislikes.Length - 2, 2);

                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "FailureToast", "showDBError();", true);
            }
        }

        protected void showSuccessToast()
        {
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessToast", "showSuccess();", true);
        }

        protected void showFailureToast()
        {
            ClientScript.RegisterStartupScript(this.GetType(), "FailureToast", "showFailed();", true);

        }
        protected void btnEditTagLineSubmit_Click(object sender, EventArgs e)
        {
            string tagLine = txtTagline.Text;

            IDictionary<string, string> newValues = new Dictionary<string, string>
            {
                ["tagLine"] = tagLine,
            };

            JavaScriptSerializer js = new JavaScriptSerializer();

            String jsonValues = js.Serialize(newValues);

            try

            {

                WebRequest request = WebRequest.Create(profileWebAPI + "update/tagline/" + Session["UserID"].ToString());
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
            }

            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "FailureToast", "showDBError();", true);
            }

        }
        protected void btnEditContactSubmit_Click(object sender, EventArgs e)
        {
            string phone = txtPhoneNumber.Text;
            string email = txtEmail.Text;

            Regex regexPhone = new Regex(@"^[2-9]\d{2}-\d{3}-\d{4}$");
            Regex regexEmail = new Regex(@"^([\w\d\-\.]+)@{1}(([\w\d\-]{1,67})|([\w\d\-]+\.[\w\d\-]{1,67}))\.(([a-zA-Z\d]{2,4})(\.[a-zA-Z\d]{2})?)$");

            if (regexPhone.IsMatch(phone) && regexEmail.IsMatch(email))
            {
                IDictionary<string, string> newValues = new Dictionary<string, string>
                {
                    ["phone"] = phone,
                    ["email"] = email
                };

                JavaScriptSerializer js = new JavaScriptSerializer();

                String jsonValues = js.Serialize(newValues);

                try

                {

                    WebRequest request = WebRequest.Create(profileWebAPI + "update/contact/" + Session["UserID"].ToString());
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
                }

                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "FailureToast", "showDBError();", true);
                }
            }
            else
            {
                showFailureToast();
            }
        }
        protected void btnEditBasicSubmit_Click(object sender, EventArgs e)
        {
            string bio = txtBio.Text;
            string numChildren = txtNumKids.Text;
            string seeking = ddlSeeking.SelectedValue;
            string wantChildren = ddlWantChildren.SelectedValue;
            string occupation = ddlOccupation.SelectedItem.Text;

            if (Int32.TryParse(numChildren, out int numChildrenParsed) && (seeking == "Male" || seeking == "Female" || seeking == "Both") &&
                Int32.TryParse(wantChildren, out int wantChildrenParsed) && (wantChildrenParsed == 0 || wantChildrenParsed == 1))
            {

                // Create an object of the Customer class which is avaialable through the web service reference and WSDL

                IDictionary<string, string> newValues = new Dictionary<string, string>
                {
                    ["bio"] = bio,
                    ["numChildren"] = numChildren,
                    ["seeking"] = seeking,
                    ["wantChildren"] = wantChildren,
                    ["occupation"] = occupation
                };



                JavaScriptSerializer js = new JavaScriptSerializer();

                String jsonValues = js.Serialize(newValues);



                try

                {

                    WebRequest request = WebRequest.Create(profileWebAPI + "update/basic/" + Session["UserID"].ToString());
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
                }

                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "FailureToast", "showDBError();", true);
                }
            }
            else
            {
                showFailureToast();
            }


        }
        protected void btnEditAboutYouSubmit_Click(object sender, EventArgs e)
        {
            string songs = txtFavSongs.Text;
            string sayings = txtFavSayings.Text;
            string restuarants = txtFavRestaurants.Text;
            string movies = txtFavMovies.Text;
            string books = txtFavBooks.Text;

            // Create an object of the Customer class which is avaialable through the web service reference and WSDL

            IDictionary<string, string> newValues = new Dictionary<string, string>
            {
                ["songs"] = songs,
                ["sayings"] = sayings,
                ["restuarants"] = restuarants,
                ["movies"] = movies,
                ["books"] = books
            };

            JavaScriptSerializer js = new JavaScriptSerializer();

            String jsonValues = js.Serialize(newValues);



            try

            {

                WebRequest request = WebRequest.Create(profileWebAPI + "update/about/" + Session["UserID"].ToString());
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
            }

            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "FailureToast", "showDBError();", true);
            }
        }

        protected void btnEditYourDetails_Click(object sender, EventArgs e)
        {
            lblYourDetails.Visible = false;
            ddl.Visible = true;
            btnEditYourDetails.Visible = false;
            divEditYourDetailsControls.Visible = true;

            WebRequest request = WebRequest.Create(profileWebAPI + Session["UserID"].ToString());
            request.Headers.Add("Authorization", "Bearer " + Session["token"].ToString());

            WebResponse response = request.GetResponse();
            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);

            String data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            DataSet result = JsonConvert.DeserializeObject<DataSet>(data);

            if (result.Tables.Count > 0)
            {
                DataTable religion = result.Tables[1];
                DataTable commitments = result.Tables[2];
                DataTable interests = result.Tables[3];
                DataTable likes = result.Tables[4];
                DataTable dislikes = result.Tables[5];

                //Table 1 is religion, Table 2 is Commitments, Table 3 is Interests, Table 4 is Likes, Table 5 is Dislikes

                foreach (DataRow row in religion.Rows)
                {
                    ddl.LBReligion.Items[ddl.LBReligion.Items.IndexOf(ddl.LBReligion.Items.FindByValue(row["religionID"].ToString()))].Selected = true;
                }
                foreach (DataRow row in commitments.Rows)
                {
                    ddl.LBCommitment.Items[ddl.LBCommitment.Items.IndexOf(ddl.LBCommitment.Items.FindByValue(row["commitmentID"].ToString()))].Selected = true;
                }

                foreach (DataRow row in interests.Rows)
                {
                    ddl.LBInterest.Items[ddl.LBInterest.Items.IndexOf(ddl.LBInterest.Items.FindByValue(row["interestID"].ToString()))].Selected = true;

                }

                foreach (DataRow row in likes.Rows)
                {
                    ddl.LBLikes.Items[ddl.LBLikes.Items.IndexOf(ddl.LBLikes.Items.FindByValue(row["likesID"].ToString()))].Selected = true;

                }

                foreach (DataRow row in dislikes.Rows)
                {
                    ddl.LBDislikes.Items[ddl.LBDislikes.Items.IndexOf(ddl.LBDislikes.Items.FindByValue(row["dislikeID"].ToString()))].Selected = true;

                }
            }
        }

        protected void btnEditYourDetailsCancel_Click(object sender, EventArgs e)
        {
            lblYourDetails.Visible = true;
            ddl.Visible = false;
            btnEditYourDetails.Visible = true;
            divEditYourDetailsControls.Visible = false;
        }
        protected void btnEditYourDetailsSubmit_Click(object sender, EventArgs e)
        {
            List<string> religions = new List<string>();
            List<string> commitments = new List<string>();
            List<string> interests = new List<string>();
            List<string> likes = new List<string>();
            List<string> dislikes = new List<string>();

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
            IDictionary<string, List<string>> newValues = new Dictionary<string, List<string>>
            {
                ["religions"] = religions,
                ["commitments"] = commitments,
                ["interests"] = interests,
                ["likes"] = likes,
                ["dislikes"] = dislikes
            };

            JavaScriptSerializer js = new JavaScriptSerializer();

            String jsonValues = js.Serialize(newValues);

            try

            {
                WebRequest request = WebRequest.Create(profileWebAPI + "update/details/" + Session["UserID"].ToString());
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
            }

            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "FailureToast", "showDBError();", true);
            }

            lblYourDetails.Visible = true;
            ddl.Visible = false;
            btnEditYourDetails.Visible = true;
            divEditYourDetailsControls.Visible = false;

            grabPersonalProfile();

        }
      
    } // end class
} // end namespace