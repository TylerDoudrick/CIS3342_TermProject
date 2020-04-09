<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="TermProject.Profile" %>

<%@ Register Src="~/UserControls/ddl.ascx" TagPrefix="uc1" TagName="ddl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <style>
        .required {
            color: red;
        }

        .hidden {
            display: none;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">


    <div class="row justify-content-center align-items-center w-75 my-5 mx-auto">
        <div class="col-2">
            <asp:Image runat="server" CssClass="img-thumbnail" ImageUrl="https://www.skymania.com/wp/wp-content/uploads/2011/06/sun_with_prominence.jpg" />
        </div>
        <div class="col-4">
            <h5 class="text-info font-weight-bold ">Sam Smith, 25</h5>
            <div class="my-2">
                <asp:Label runat="server" ID="lblLocation"> Philadelphia, PA</asp:Label>
            </div>
            <asp:TextBox runat="server" ID="txtTagline" ReadOnly="true" Text="This is the Tagline" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
        </div>


    </div>

    <hr class="w-75 mx-auto" />


    <div class="row justify-content-center align-items-center w-75 my-5 mx-auto">
        <div class="col text-right">
            <div class="font-weight-bold text-info h5 my-auto">Contact Information</div>
        </div>
        <div class="col my-auto mx-2 text-right">
            <span class="h3" id="btnEditContact"><i class="fas fa-pen-square"></i></span>
        </div>
    </div>
    <div class="row justify-content-center align-items-center w-75 my-5 mx-auto">
        <div class="col-4">

            <div class="row">
                <asp:Label runat="server" ID="lblPhoneNumber" for="<%= txtPhoneNumber.ClientID %>" CssClass="col-4 my-auto"> Phone Number</asp:Label>

                <div class="col -8">
                    <div class="input-group" id="txtFullNumber">
                        <asp:TextBox runat="server" ID="txtPhoneNumber" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row my-4">
                <asp:Label runat="server" ID="lblEmail" for="<%= txtEmail.ClientID %>" CssClass="col-4 my-auto">Email</asp:Label>

                <div class="col -8">
                    <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>


    <div class="row justify-content-center hidden" id="divEditContactControls">
        <asp:Button runat="server" Text="Update" CssClass="btn btn-success mr-2 h-50 " OnClick="btnEditContactSubmit_Click" />
        <button type="button" class="btn btn-secondary h-50" id="btnEditContactCancel">Cancel</button>
    </div>

    <div class="row justify-content-center align-items-center w-75 my-5 mx-auto">
        <div class="col text-right">
            <div class="font-weight-bold text-info h5 my-auto">Basic Information</div>
        </div>
        <div class="col my-auto mx-2 text-right">
            <span class="h3" id="btnEditBasic"><i class="fas fa-pen-square"></i></span>
        </div>
    </div>

    <div class="row justify-content-center align-items-center w-75 my-5 mx-auto">
        <div class="col-md-10">
            <asp:Label runat="server" ID="lblBio"> Bio </asp:Label>

            <asp:TextBox runat="server" ID="txtBio" CssClass="form-control" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
        </div>
    </div>

    <div class="row justify-content-center align-items-center w-75 my-5 mx-auto">
        <div class="col-2">
            <asp:Label runat="server" ID="lblSeekingGender"> Seeking Gender</asp:Label>

            <div class="form-group align-items-start d-flex flex-column justify-content-around" id="divSeeking">
                <asp:CheckBox runat="server" ID="chkSeekingFemale" Text="Female" Enabled="false" />
                <asp:CheckBox runat="server" ID="chkSeekingMale" Text="Male" Enabled="false" />
            </div>
        </div>
        <div class="col-2">
            <asp:Label runat="server" ID="lblNumKids"> Number of Kids</asp:Label>
            <asp:TextBox runat="server" ID="txtNumKids" CssClass="form-control" ReadOnly="true"> </asp:TextBox>
        </div>
        <div class="col-2">
            <asp:Label runat="server" ID="lblWantKids"> Do you want kids? </asp:Label>
            <asp:DropDownList ID="ddlWantKids" runat="server" CssClass="form-control">
                <asp:ListItem Value="yes">Yes</asp:ListItem>
                <asp:ListItem Value="no">No</asp:ListItem>
            </asp:DropDownList>

        </div>
        <div class="col-4">
            <asp:Label runat="server" ID="lblOccupation"> Occupation</asp:Label>

            <asp:DropDownList runat="server" ID="ddlOccupation" CssClass="form-control" disabled="disabled" EnableViewState="false">
                <asp:ListItem Value="-1"> Select your occupation..</asp:ListItem>
                <asp:ListItem Value="1">  Chiropractor</asp:ListItem>
                <asp:ListItem Value="2">  Dentist</asp:ListItem>
                <asp:ListItem Value="">  Dietitian or Nutritionist</asp:ListItem>
                <asp:ListItem Value="4">  Optometrist</asp:ListItem>
                <asp:ListItem Value="5">  Pharmacist</asp:ListItem>
                <asp:ListItem Value="6">  Physician</asp:ListItem>
                <asp:ListItem Value="7">  Physician Assistant</asp:ListItem>
                <asp:ListItem Value="8">  Podiatrist</asp:ListItem>
                <asp:ListItem Value="9">  Registered Nurse</asp:ListItem>
                <asp:ListItem Value="10">  Therapist</asp:ListItem>
                <asp:ListItem Value="11">  Veterinarian</asp:ListItem>
                <asp:ListItem Value="12">  Health Technologist or Technician</asp:ListItem>
                <asp:ListItem Value="1">  Other Healthcare or Technical Occupation</asp:ListItem>
                <asp:ListItem Value="14">  Nursing, Psychiatric, or Home Health Aide</asp:ListItem>
                <asp:ListItem Value="15">  Occupational and PT Assistant/Aide</asp:ListItem>
                <asp:ListItem Value="16">  Other Healthcare Support Occupation</asp:ListItem>
                <asp:ListItem Value="17">  Chief Executive</asp:ListItem>
                <asp:ListItem Value="18">  General and Operations Manager</asp:ListItem>
                <asp:ListItem Value="19">  Advertising</asp:ListItem>
                <asp:ListItem Value="19.2"> Promotion</asp:ListItem>
                <asp:ListItem Value="19."> Public Relations</asp:ListItem>
                <asp:ListItem Value="19.4"> Sales</asp:ListItem>
                <asp:ListItem Value="19.1"> Marketing</asp:ListItem>
                <asp:ListItem Value="20">  Operations Specialties Manager</asp:ListItem>
                <asp:ListItem Value="21">  Construction Manager</asp:ListItem>
                <asp:ListItem Value="22">  Engineering Manager</asp:ListItem>
                <asp:ListItem Value="2">  Accountant, Auditor</asp:ListItem>
                <asp:ListItem Value="24">  Business Operations or Financial Specialist</asp:ListItem>
                <asp:ListItem Value="25">  Business Owner</asp:ListItem>
                <asp:ListItem Value="26">  Other Business or Executive</asp:ListItem>
                <asp:ListItem Value="27">  Architect, Surveyor, or Cartographer</asp:ListItem>
                <asp:ListItem Value="28">  Engineer</asp:ListItem>
                <asp:ListItem Value="29">  Other Architecture and Engineering Occupation</asp:ListItem>
                <asp:ListItem Value="0">  Postsecondary Teacher</asp:ListItem>
                <asp:ListItem Value="1">  Primary, Secondary, or Special Education School Teacher</asp:ListItem>
                <asp:ListItem Value="2">  Other Teacher or Instructor</asp:ListItem>
                <asp:ListItem Value="">  Other Education, Training, and Library </asp:ListItem>
                <asp:ListItem Value="4">  Arts, Entertainment, Sports,  Occupations</asp:ListItem>
                <asp:ListItem Value="5">  Computer Specialist, Mathematical Science</asp:ListItem>
                <asp:ListItem Value="6">  Social Worker</asp:ListItem>
                <asp:ListItem Value="7">  Lawyer, Judge</asp:ListItem>
                <asp:ListItem Value="8">  Life Scientist </asp:ListItem>
                <asp:ListItem Value="9">  Physical Scientist </asp:ListItem>
                <asp:ListItem Value="40">  Religious Worker</asp:ListItem>
                <asp:ListItem Value="41">  Social Scientist and Related Worker</asp:ListItem>
                <asp:ListItem Value="42">  Other Professional Occupation</asp:ListItem>
                <asp:ListItem Value="4">  Supervisor of Administrative Support Workers</asp:ListItem>
                <asp:ListItem Value="44">  Financial Clerk</asp:ListItem>
                <asp:ListItem Value="45">  Secretary or Administrative Assistant</asp:ListItem>
                <asp:ListItem Value="46">  Material Recording, Scheduling, and Dispatching Worker</asp:ListItem>
                <asp:ListItem Value="47">  Other Office and Administrative Support </asp:ListItem>
                <asp:ListItem Value="48">  Fire Fighter </asp:ListItem>
                <asp:ListItem Value="49">  Chef or Head Cook</asp:ListItem>
                <asp:ListItem Value="50">  Cook or Food Preparation Worker</asp:ListItem>
                <asp:ListItem Value="51">  Food and Beverage Serving Worker </asp:ListItem>
                <asp:ListItem Value="52">  Building and Grounds Cleaning and Maintenance</asp:ListItem>
                <asp:ListItem Value="5">  Personal Care and Service </asp:ListItem>
                <asp:ListItem Value="54">  Sales Supervisor, Retail Sales</asp:ListItem>
                <asp:ListItem Value="55">  Retail Sales Worker</asp:ListItem>
                <asp:ListItem Value="56">  Insurance Sales Agent</asp:ListItem>
                <asp:ListItem Value="57">  Sales Representative</asp:ListItem>
                <asp:ListItem Value="58">  Real Estate Sales Agent</asp:ListItem>
                <asp:ListItem Value="59">  Other Services Occupation</asp:ListItem>
                <asp:ListItem Value="60">  Construction and Extraction </asp:ListItem>
                <asp:ListItem Value="61">  Farming, Fishing, and Forestry</asp:ListItem>
                <asp:ListItem Value="62">  Installation, Maintenance, and Repair</asp:ListItem>
                <asp:ListItem Value="6">  Production Occupations</asp:ListItem>
                <asp:ListItem Value="64">  Other Agriculture,  and Skilled Crafts </asp:ListItem>
                <asp:ListItem Value="65">  Aircraft Pilot or Flight Engineer</asp:ListItem>
                <asp:ListItem Value="66">  Motor Vehicle Operator </asp:ListItem>
                <asp:ListItem Value="67">  Other Transportation Occupation</asp:ListItem>
                <asp:ListItem Value="68">  Military</asp:ListItem>
                <asp:ListItem Value="69"> Police Officer or Correctional Officer</asp:ListItem>
                <asp:ListItem Value="70">  Homemaker</asp:ListItem>
                <asp:ListItem Value="74"> Student</asp:ListItem>
                <asp:ListItem Value="71">  Other Occupation</asp:ListItem>
                <asp:ListItem Value="72">  Don't Know</asp:ListItem>
                <asp:ListItem Value="7">  Not Applicable</asp:ListItem>

            </asp:DropDownList>
        </div>
    </div>
    <div class="row justify-content-center hidden" id="divEditBasicControls">
        <asp:Button runat="server" Text="Update" CssClass="btn btn-success mr-2 h-50 " ID="btnEditBasicSubmit" OnClick="btnEditBasicSubmit_Click" />
        <button type="button" class="btn btn-secondary h-50" id="btnEditBasicCancel">Cancel</button>
    </div>
    <hr class="mx-auto w-75" />
    <div class="row justify-content-center align-items-center w-75 my-5 mx-auto">
        <div class="col text-right">
            <div class="font-weight-bold text-info h5 my-auto">Miscellaneous</div>
        </div>
        <div class="col my-auto mx-2 text-right">
            <span class="h3" id="btnEditMisc"><i class="fas fa-pen-square"></i></span>
        </div>
    </div>
    <div class="row justify-content-center align-items-center w-75 my-5 mx-auto">
        <div class="col-4">
            <asp:Label runat="server" ID="lblFavSongs">Favorite Songs</asp:Label>
            <asp:TextBox runat="server" ID="txtFavSongs" CssClass="form-control" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
        </div>
        <div class="col-4">
            <asp:Label runat="server" ID="lblFavSayings">Favorite Sayings</asp:Label>
            <asp:TextBox runat="server" ID="txtFavSayings" CssClass="form-control" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
        </div>
        <div class="col-4">
            <asp:Label runat="server" ID="lblFavRestaurants">Favorite Restaurants</asp:Label>
            <asp:TextBox runat="server" ID="txtFavRestaurants" CssClass="form-control" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
        </div>
    </div>
    <div class="row justify-content-center align-items-center w-75 my-5 mx-auto">
        <div class="col-4">
            <asp:Label runat="server" ID="lblFavMovies">Favorite Movies</asp:Label>
            <asp:TextBox runat="server" ID="txtFavMovies" CssClass="form-control" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
        </div>
        <div class="col-4">
            <asp:Label runat="server" ID="lblFavBooks">Favorite Books</asp:Label>
            <asp:TextBox runat="server" ID="txtFavBooks" CssClass="form-control" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>

        </div>
    </div>
    <uc1:ddl runat="server" ID="ddl" />

    <div class="row justify-content-center hidden" id="divEditMiscControls">
        <asp:Button runat="server" Text="Update" CssClass="btn btn-success mr-2 h-50" OnClick="btnEditMiscSubmit_Click" />
        <button type="button" class="btn btn-secondary h-50" id="btnEditMiscCancel">Cancel</button>
    </div>
    <div class="lblMisc">
        <div class="row justify-content-center align-items-center w-75 my-5 mx-auto">
            <div class="col">
                <label class="w-100 border-bottom  d-block">Religion</label>
                <asp:Label runat="server" ID="lblReligion" CssClass="selectpicker col-10">Jainsim, Judaism, Sikhism</asp:Label>

            </div>
            <div class="col">
                <label class="w-100 border-bottom  d-block">Commitment</label>
                <asp:Label runat="server" ID="lblCommittment" CssClass="selectpicker col-10">Casual, Longterm</asp:Label>
            </div>
            <div class="col">
                <label class="w-100 border-bottom  d-block">Interests</label>
                <asp:Label runat="server" ID="lblInterests" CssClass="selectpicker col-10">Collections, DIY, Embroidery</asp:Label>
            </div>

        </div>
        <div class="row justify-content-center align-items-center w-75 my-5 mx-auto">
            <div class="col">
                <label class="w-100 border-bottom  d-block">Likes</label>
                <asp:Label runat="server" ID="lblLikes" CssClass="selectpicker col-7">Action, Dancing, Chocolate</asp:Label>
            </div>
            <div class="col">
                <label class="w-100 border-bottom  d-block">Dislikes</label>
                <asp:Label runat="server" ID="lblDislikes" CssClass="selectpicker col-7">Art, Beer, Bullies</asp:Label>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="EndBodyPlaceHolder" runat="server">
    <script>
        $(document).ready(function () {

            var currentVals = {};
            $(".ddlMisc").hide();
            HideAll();
            $("#navlinkDashboard").removeClass("active");
            $("#navlinkProfile").addClass("active");



            //Contact Information
            $("#btnEditContact").click(function () {
                curentVals = {};
                HideAll();
                currentVals["txtPhone"] = $("#<%= txtPhoneNumber.ClientID %>").val();
                currentVals["txtEmail"] = $("#<%= txtEmail.ClientID %>").val();
                $("#<%= txtPhoneNumber.ClientID %>").attr('readonly', false);
                $("#<%= txtEmail.ClientID %>").attr('readonly', false);
                $("#divEditContactControls").removeClass('hidden');
            });

            $("#btnEditContactCancel").click(function () {
                $("#<%= txtPhoneNumber.ClientID %>").val(currentVals["txtPhone"]);
                $("#<%= txtEmail.ClientID %>").val(currentVals["txtEmail"]);
                HideAll();
            });

            //Basic Information
            $("#btnEditBasic").click(function () {
                currentVals = {};
                HideAll();
                currentVals["txtBio"] = $("#<%= txtBio.ClientID %>").val();
                currentVals["txtNumKids"] = $("#<%= txtNumKids.ClientID %>").val();
                currentVals["ddlOccupation"] = $("#<%= ddlOccupation.ClientID %> option:selected").val();
                currentVals["ddlWantKids"] = $("#<%= ddlWantKids.ClientID %> option:selected").val();
                currentVals["chkSeekingMale"] = $("#<%= chkSeekingMale.ClientID %>").prop('checked');
                currentVals["chkSeekingFemale"] = $("#<%= chkSeekingFemale.ClientID %>").prop('checked');

                $("#<%= txtBio.ClientID %>").attr('readonly', false);
                $("#<%= txtNumKids.ClientID %>").attr('readonly', false);
                $("#<%= ddlOccupation.ClientID %>").prop('disabled', false);
                $("#<%= ddlWantKids.ClientID %>").prop('disabled', false);
                $("#<%= chkSeekingMale.ClientID %>").prop('disabled', false);
                $("#<%= chkSeekingFemale.ClientID %>").prop('disabled', false);
                $("#divEditBasicControls").removeClass('hidden');
                console.log(currentVals);

            });

            $("#btnEditBasicCancel").click(function () {
                $("#<%= txtBio.ClientID %>").val(currentVals["txtBio"]);
                $("#<%= txtNumKids.ClientID %>").val(currentVals["txtNumKids"]);
                $("#<%= ddlOccupation.ClientID %>").val(currentVals["ddlOccupation"]).change();
                $("#<%= ddlWantKids.ClientID %>").val(currentVals["ddlWantKids"]).change();
                $("#<%= chkSeekingMale.ClientID %>").prop('checked', currentVals["chkSeekingMale"]);
                $("#<%= chkSeekingFemale.ClientID %>").prop('checked', currentVals["chkSeekingFemale"]);

                HideAll();
            });

            //Misc Information

            $("#btnEditMisc").click(function () {
                $(".ddlMisc").show();
                $(".lblMisc").hide();
                curentVals = {};
                HideAll();
                currentVals["txtFavSongs"] = $("#<%= txtFavSongs.ClientID %>").val();
                currentVals["txtFavSayings"] = $("#<%= txtFavSayings.ClientID %>").val();
                currentVals["txtFavRestuarants"] = $("#<%= txtFavRestaurants.ClientID %>").val();
                currentVals["txtFavMovies"] = $("#<%= txtFavMovies.ClientID %>").val();
                currentVals["txtFavBooks"] = $("#<%= txtFavBooks.ClientID %>").val();
                currentVals["ddlReligion"] = $(".ddlReligion").selectpicker('val');
                currentVals["ddlCommitment"] = $(".ddlCommitment").selectpicker('val');
                currentVals["ddlInterests"] = $(".ddlInterests").selectpicker('val');
                currentVals["ddlLikes"] = $(".ddlLikes").selectpicker('val');
                currentVals["ddlDislikes"] = $(".ddlDislikes").selectpicker('val');
                $("#<%= txtFavSongs.ClientID %>").attr('readonly', false);
                $("#<%= txtFavSayings.ClientID %>").attr('readonly', false);
                $("#<%= txtFavRestaurants.ClientID %>").attr('readonly', false);
                $("#<%= txtFavMovies.ClientID %>").attr('readonly', false);
                $("#<%= txtFavBooks.ClientID %>").attr('readonly', false);
                $("#divEditMiscControls").removeClass('hidden');
                $(".lblReligion").addClass('d-none');
                $(".lblCommitment").addClass('d-none');
                $(".lblInterests").addClass('d-none');
                $(".lblLikes").addClass('d-none');
                $(".lblDislikes").addClass('d-none');
                $(".ddl").removeClass('d-none');
                console.log(currentVals);
            });

            $("#btnEditMiscCancel").click(function () {
                $("#<%= txtFavSongs.ClientID %>").val(currentVals["txtFavSongs"]);
                $("#<%= txtFavSayings.ClientID %>").val(currentVals["txtFavSayings"]);
                $("#<%= txtFavRestaurants.ClientID %>").val(currentVals["txtFavRestuarants"]);
                $("#<%= txtFavMovies.ClientID %>").val(currentVals["txtFavMovies"]);
                $("#<%= txtFavBooks.ClientID %>").val(currentVals["txtFavBooks"]);
                $(".ddlReligion").selectpicker('val', currentVals["ddlReligion"]);
                $(".ddlCommitment").selectpicker('val', currentVals["ddlCommitment"]);
                $(".ddlInterests").selectpicker('val', currentVals["ddlInterests"]);
                $(".ddlLikes").selectpicker('val', currentVals["ddlLikes"]);
                $(".ddlDislikes").selectpicker('val', currentVals["ddlDislikes"]);
                HideAll();

            });
            //Is when when any cancel button is hit to ensure proper functionality.
            function HideAll() {
                $("#<%= txtPhoneNumber.ClientID %>").attr('readonly', true);
                $("#<%= txtEmail.ClientID %>").attr('readonly', true);
                $("#divContactEditControls").addClass('hidden');

                $("#<%= txtBio.ClientID %>").attr('readonly', true);
                $("#<%= txtNumKids.ClientID %>").attr('readonly', true);
                $("#<%= ddlOccupation.ClientID %>").prop('disabled', true);
                $("#<%= ddlWantKids.ClientID %>").prop('disabled', true);
                $("#<%= chkSeekingMale.ClientID %>").prop('disabled', true);
                $("#<%= chkSeekingFemale.ClientID %>").prop('disabled', true);
                $("#divBasicEditControls").addClass('hidden');

                $("#<%= txtFavSongs.ClientID %>").attr('readonly', true);
                $("#<%= txtFavSayings.ClientID %>").attr('readonly', true);
                $("#<%= txtFavRestaurants.ClientID %>").attr('readonly', true);
                $("#<%= txtFavMovies.ClientID %>").attr('readonly', true);
                $("#<%= txtFavBooks.ClientID %>").attr('readonly', true);
                $("#divEditMiscControls").addClass('hidden');
            }

        });


    </script>
</asp:Content>

