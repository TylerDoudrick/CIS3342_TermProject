<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="TermProject.Profile" MaintainScrollPositionOnPostback="true" %>

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
    <script>
        function showSuccess() {
            toastr.options = {
                "closeButton": true,
                "debug": false,
                "newestOnTop": false,
                "progressBar": true,
                "positionClass": "toast-bottom-right",
                "preventDuplicates": false,
                "onclick": null,
                "showDuration": "300",
                "hideDuration": "1000",
                "timeOut": "2500",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            }
            toastr["success"]("Changes successfully saved!", "Success")


        }
        function showFailed() {
            toastr.options = {
                "closeButton": true,
                "debug": false,
                "newestOnTop": false,
                "progressBar": true,
                "positionClass": "toast-bottom-right",
                "preventDuplicates": false,
                "onclick": null,
                "showDuration": "300",
                "hideDuration": "1000",
                "timeOut": "2500",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            }
            toastr["error"]("Unable to save changes. Please check your inputs.", "Failed")
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">


    <div class="row justify-content-center align-items-center w-75 my-5 mx-auto">
        <div class="col-2">
            <asp:Image runat="server" CssClass="img-thumbnail" ImageUrl="https://www.skymania.com/wp/wp-content/uploads/2011/06/sun_with_prominence.jpg" />
        </div>
        <div class="col-4">
            <asp:Label class="text-info font-weight-bold" ID="lblName" runat="server">Sam Smith, 25</asp:Label>
            <div class="my-2">
                <asp:Label runat="server" ID="lblLocation"> Philadelphia, PA</asp:Label>
            </div>
            <span class="h3 text-right" id="btnEditTagLine"><i class="fas fa-pen-square"></i></span>

            <asp:TextBox runat="server" ID="txtTagline" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>

        </div>

    </div>
    <div class="row justify-content-center hidden" id="divEditTagLineControls">
        <asp:Button runat="server" Text="Update" CssClass="btn btn-success mr-2 h-50 " OnClick="btnEditTagLineSubmit_Click" />
        <button type="button" class="btn btn-secondary h-50" id="btnEditTagLineCancel">Cancel</button>
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
                        <asp:TextBox runat="server" ID="txtPhoneNumber" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row my-4">
                <asp:Label runat="server" ID="lblEmail" for="<%= txtEmail.ClientID %>" CssClass="col-4 my-auto">Email</asp:Label>

                <div class="col -8">
                    <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control"></asp:TextBox>
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

            <asp:TextBox runat="server" ID="txtBio" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>

    <div class="row justify-content-center align-items-center w-75 my-5 mx-auto">
        <div class="col-2">
            <asp:Label runat="server" ID="lblSeekingGender"> Seeking Gender</asp:Label>

            <asp:DropDownList ID="ddlSeeking" runat="server" CssClass="form-control" Enabled="false">
                <asp:ListItem Value="Female">Female</asp:ListItem>
                <asp:ListItem Value="Male">Male</asp:ListItem>
                <asp:ListItem Value="Both">Both</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-2">
            <asp:Label runat="server" ID="lblNumKids"> Number of Kids</asp:Label>
            <asp:TextBox runat="server" ID="txtNumKids" CssClass="form-control"> </asp:TextBox>
        </div>
        <div class="col-2">
            <asp:Label runat="server" ID="lblWantChildren"> Do you want kids? </asp:Label>
            <asp:DropDownList ID="ddlWantChildren" runat="server" CssClass="form-control">
                <asp:ListItem Value="1">Yes</asp:ListItem>
                <asp:ListItem Value="0">No</asp:ListItem>
            </asp:DropDownList>

        </div>
        <div class="col-4">
            <asp:Label runat="server" ID="lblOccupation"> Occupation</asp:Label>

            <asp:DropDownList runat="server" ID="ddlOccupation" CssClass="form-control">
                <asp:ListItem Value="-1"> Select your occupation..</asp:ListItem>
                <asp:ListItem>Chiropractor</asp:ListItem>
                <asp:ListItem>Dentist</asp:ListItem>
                <asp:ListItem>Dietitian/Nutritionist</asp:ListItem>
                <asp:ListItem>Optometrist</asp:ListItem>
                <asp:ListItem>Pharmacist</asp:ListItem>
                <asp:ListItem>Physician</asp:ListItem>
                <asp:ListItem>Physician Assistant</asp:ListItem>
                <asp:ListItem>Podiatrist</asp:ListItem>
                <asp:ListItem>Registered Nurse</asp:ListItem>
                <asp:ListItem>Therapist</asp:ListItem>
                <asp:ListItem>Veterinarian</asp:ListItem>
                <asp:ListItem>Health Technologist or Technician</asp:ListItem>
                <asp:ListItem>Other Healthcare or Technical Occupation</asp:ListItem>
                <asp:ListItem>Nursing, Psychiatric, or Home Health Aide</asp:ListItem>
                <asp:ListItem>Occupational and PT Assistant/Aide</asp:ListItem>
                <asp:ListItem>Other Healthcare Support Occupation</asp:ListItem>
                <asp:ListItem>Chief Executive</asp:ListItem>
                <asp:ListItem>General and Operations Manager</asp:ListItem>
                <asp:ListItem>Advertising</asp:ListItem>
                <asp:ListItem>Promotion</asp:ListItem>
                <asp:ListItem>Public Relations</asp:ListItem>
                <asp:ListItem>Sales</asp:ListItem>
                <asp:ListItem>Marketing</asp:ListItem>
                <asp:ListItem>Operations Specialties Manager</asp:ListItem>
                <asp:ListItem>Construction Manager</asp:ListItem>
                <asp:ListItem>Engineering Manager</asp:ListItem>
                <asp:ListItem>Accountant, Auditor</asp:ListItem>
                <asp:ListItem>Business Operations or Financial Specialist</asp:ListItem>
                <asp:ListItem>Business Owner</asp:ListItem>
                <asp:ListItem>Other Business or Executive</asp:ListItem>
                <asp:ListItem>Architect, Surveyor, or Cartographer</asp:ListItem>
                <asp:ListItem>Engineer</asp:ListItem>
                <asp:ListItem>Other Architecture and Engineering Occupation</asp:ListItem>
                <asp:ListItem>Postsecondary Teacher</asp:ListItem>
                <asp:ListItem>Primary, Secondary, or Special Education School Teacher</asp:ListItem>
                <asp:ListItem>Other Teacher or Instructor</asp:ListItem>
                <asp:ListItem>Other Education, Training, and Library </asp:ListItem>
                <asp:ListItem>Arts, Entertainment, Sports,  Occupations</asp:ListItem>
                <asp:ListItem>Computer Specialist, Mathematical Science</asp:ListItem>
                <asp:ListItem>Social Worker</asp:ListItem>
                <asp:ListItem>Lawyer, Judge</asp:ListItem>
                <asp:ListItem>Life Scientist </asp:ListItem>
                <asp:ListItem>Physical Scientist </asp:ListItem>
                <asp:ListItem>Religious Worker</asp:ListItem>
                <asp:ListItem>Social Scientist and Related Worker</asp:ListItem>
                <asp:ListItem>Other Professional Occupation</asp:ListItem>
                <asp:ListItem>Supervisor of Administrative Support Workers</asp:ListItem>
                <asp:ListItem>Financial Clerk</asp:ListItem>
                <asp:ListItem>Secretary or Administrative Assistant</asp:ListItem>
                <asp:ListItem>Material Recording, Scheduling, and Dispatching Worker</asp:ListItem>
                <asp:ListItem>Other Office and Administrative Support </asp:ListItem>
                <asp:ListItem>Fire Fighter </asp:ListItem>
                <asp:ListItem>Chef or Head Cook</asp:ListItem>
                <asp:ListItem>Cook or Food Preparation Worker</asp:ListItem>
                <asp:ListItem>Food and Beverage Serving Worker </asp:ListItem>
                <asp:ListItem>Building and Grounds Cleaning and Maintenance</asp:ListItem>
                <asp:ListItem>Personal Care and Service </asp:ListItem>
                <asp:ListItem>Sales Supervisor, Retail Sales</asp:ListItem>
                <asp:ListItem>Retail Sales Worker</asp:ListItem>
                <asp:ListItem>Insurance Sales Agent</asp:ListItem>
                <asp:ListItem>Sales Representative</asp:ListItem>
                <asp:ListItem>Real Estate Sales Agent</asp:ListItem>
                <asp:ListItem>Other Services Occupation</asp:ListItem>
                <asp:ListItem>Construction and Extraction </asp:ListItem>
                <asp:ListItem>Farming, Fishing, and Forestry</asp:ListItem>
                <asp:ListItem>Installation, Maintenance, and Repair</asp:ListItem>
                <asp:ListItem>Production Occupations</asp:ListItem>
                <asp:ListItem>Other Agriculture,  and Skilled Crafts </asp:ListItem>
                <asp:ListItem>Aircraft Pilot or Flight Engineer</asp:ListItem>
                <asp:ListItem>Motor Vehicle Operator </asp:ListItem>
                <asp:ListItem>Other Transportation Occupation</asp:ListItem>
                <asp:ListItem>Military</asp:ListItem>
                <asp:ListItem>Police Officer or Correctional Officer</asp:ListItem>
                <asp:ListItem>Homemaker</asp:ListItem>
                <asp:ListItem>Student</asp:ListItem>
                <asp:ListItem>Other Occupation</asp:ListItem>
                <asp:ListItem>Don't Know</asp:ListItem>
                <asp:ListItem>Not Applicable</asp:ListItem>

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
            <div class="font-weight-bold text-info h5 my-auto">About You</div>
        </div>
        <div class="col my-auto mx-2 text-right">
            <span class="h3" id="btnEditAboutYou"><i class="fas fa-pen-square"></i></span>
        </div>
    </div>
    <div class="row justify-content-center align-items-center w-75 my-5 mx-auto">
        <div class="col-4">
            <asp:Label runat="server" ID="lblFavSongs">Favorite Songs</asp:Label>
            <asp:TextBox runat="server" ID="txtFavSongs" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
        </div>
        <div class="col-4">
            <asp:Label runat="server" ID="lblFavSayings">Favorite Sayings</asp:Label>
            <asp:TextBox runat="server" ID="txtFavSayings" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
        </div>
        <div class="col-4">
            <asp:Label runat="server" ID="lblFavRestaurants">Favorite Restaurants</asp:Label>
            <asp:TextBox runat="server" ID="txtFavRestaurants" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>
    <div class="row justify-content-center align-items-center w-75 my-5 mx-auto">
        <div class="col-4">
            <asp:Label runat="server" ID="lblFavMovies">Favorite Movies</asp:Label>
            <asp:TextBox runat="server" ID="txtFavMovies" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
        </div>
        <div class="col-4">
            <asp:Label runat="server" ID="lblFavBooks">Favorite Books</asp:Label>
            <asp:TextBox runat="server" ID="txtFavBooks" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>

        </div>
    </div>
    <div class="row justify-content-center hidden" id="divEditAboutYouControls">
        <asp:Button runat="server" Text="Update" CssClass="btn btn-success mr-2 h-50" OnClick="btnEditAboutYouSubmit_Click" />
        <button type="button" class="btn btn-secondary h-50" id="btnEditAboutYouCancel">Cancel</button>
    </div>

    <div class="row justify-content-center align-items-center w-75 my-5 mx-auto">
        <div class="col text-right">
            <div class="font-weight-bold text-info h5 my-auto">Your Details</div>
        </div>
        <div class="col my-auto mx-2 text-right">
            <asp:LinkButton CssClass="h3" ID="btnEditYourDetails" runat="server" OnClick="btnEditYourDetails_Click"><i class="fas fa-pen-square"></i></asp:LinkButton>
        </div>
    </div>

    <uc1:ddl runat="server" ID="ddl" />


    <div runat="server" id="lblYourDetails">
        <div class="row justify-content-center align-items-center w-75 my-5 mx-auto">
            <div class="col">
                <label class="w-100 border-bottom">Religion</label>
                <asp:Label runat="server" ID="lblReligion" CssClass="col-10"></asp:Label>

            </div>
            <div class="col">
                <label class="w-100 border-bottom">Commitment</label>
                <asp:Label runat="server" ID="lblCommittment" CssClass="col-10"></asp:Label>
            </div>
            <div class="col">
                <label class="w-100 border-bottom">Interests</label>
                <asp:Label runat="server" ID="lblInterests" CssClass="col-10"></asp:Label>
            </div>

        </div>
        <div class="row justify-content-center align-items-center w-75 my-5 mx-auto">
            <div class="col">
                <label class="w-100 border-bottom">Likes</label>
                <asp:Label runat="server" ID="lblLikes" CssClass="col-7"></asp:Label>
            </div>
            <div class="col">
                <label class="w-100 border-bottom">Dislikes</label>
                <asp:Label runat="server" ID="lblDislikes" CssClass="col-7"></asp:Label>
            </div>
        </div>
    </div>
    <div class="row justify-content-center" id="divEditYourDetailsControls" runat="server">
        <asp:Button runat="server" Text="Update" CssClass="btn btn-success mr-2 h-50" OnClick="btnEditYourDetailsSubmit_Click" />
        <asp:Button type="button" class="btn btn-secondary h-50" ID="btnEditYourDetailsCancel" runat="server" Text="Cancel" OnClick="btnEditYourDetailsCancel_Click"></asp:Button>
    </div>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="EndBodyPlaceHolder" runat="server">
    <script>
        $(document).ready(function () {
           

            var currentVals = {};
            HideAll();
            $("#navlinkDashboard").removeClass("active");
            $("#navlinkProfile").addClass("active");

            $("#btnEditTagLine").click(function () {
                $(".fa-pen-square").each(function () { $(this).hide() });
                curentVals = {};
                HideAll();
                currentVals["txtTagLine"] = $("#<%= txtTagline.ClientID %>").val();
                $("#<%= txtTagline.ClientID %>").attr('readonly', false);
                $("#divEditTagLineControls").removeClass('hidden');
            });

            $("#btnEditTagLineCancel").click(function () {
                $(".fa-pen-square").each(function () { $(this).show() });

                $("#<%= txtTagline.ClientID %>").val(currentVals["txtTagLine"]);
                HideAll();
            });
            //Contact Information
            $("#btnEditContact").click(function () {
                $(".fa-pen-square").each(function () { $(this).hide() });

                curentVals = {};
                HideAll();
                currentVals["txtPhone"] = $("#<%= txtPhoneNumber.ClientID %>").val();
                currentVals["txtEmail"] = $("#<%= txtEmail.ClientID %>").val();
                $("#<%= txtPhoneNumber.ClientID %>").attr('readonly', false);
                $("#<%= txtEmail.ClientID %>").attr('readonly', false);
                $("#divEditContactControls").removeClass('hidden');
            });

            $("#btnEditContactCancel").click(function () {
                $(".fa-pen-square").each(function () { $(this).show() });

                $("#<%= txtPhoneNumber.ClientID %>").val(currentVals["txtPhone"]);
                $("#<%= txtEmail.ClientID %>").val(currentVals["txtEmail"]);
                HideAll();
            });

            //Basic Information
            $("#btnEditBasic").click(function () {
                $(".fa-pen-square").each(function () { $(this).hide() });

                currentVals = {};
                HideAll();
                currentVals["txtBio"] = $("#<%= txtBio.ClientID %>").val();
                currentVals["txtNumKids"] = $("#<%= txtNumKids.ClientID %>").val();
                currentVals["ddlOccupation"] = $("#<%= ddlOccupation.ClientID %> option:selected").val();
                currentVals["ddlWantChildren"] = $("#<%= ddlWantChildren.ClientID %> option:selected").val();
                currentVals["ddlSeeking"] = $("#<%= ddlSeeking.ClientID %> option:selected").val();


                $("#<%= txtBio.ClientID %>").attr('readonly', false);
                $("#<%= txtNumKids.ClientID %>").attr('readonly', false);
                $("#<%= ddlOccupation.ClientID %>").prop('disabled', false);
                $("#<%= ddlWantChildren.ClientID %>").prop('disabled', false);
                $("#<%= ddlSeeking.ClientID %>").prop('disabled', false);
                $("#divEditBasicControls").removeClass('hidden');

            });

            $("#btnEditBasicCancel").click(function () {
                $(".fa-pen-square").each(function () { $(this).show() });

                $("#<%= txtBio.ClientID %>").val(currentVals["txtBio"]);
                $("#<%= txtNumKids.ClientID %>").val(currentVals["txtNumKids"]);
                $("#<%= ddlOccupation.ClientID %>").val(currentVals["ddlOccupation"]).change();
                $("#<%= ddlWantChildren.ClientID %>").val(currentVals["ddlWantChildren"]).change();
                $("#<%= ddlSeeking.ClientID %>").val(currentVals["ddlSeeking"]).change();


                HideAll();
            });

            //Misc Information

            $("#btnEditAboutYou").click(function () {
                $(".fa-pen-square").each(function () { $(this).hide() });

                curentVals = {};
                HideAll();
                currentVals["txtFavSongs"] = $("#<%= txtFavSongs.ClientID %>").val();
                currentVals["txtFavSayings"] = $("#<%= txtFavSayings.ClientID %>").val();
                currentVals["txtFavRestuarants"] = $("#<%= txtFavRestaurants.ClientID %>").val();
                currentVals["txtFavMovies"] = $("#<%= txtFavMovies.ClientID %>").val();
                currentVals["txtFavBooks"] = $("#<%= txtFavBooks.ClientID %>").val();





                $("#<%= txtFavSongs.ClientID %>").attr('readonly', false);
                $("#<%= txtFavSayings.ClientID %>").attr('readonly', false);
                $("#<%= txtFavRestaurants.ClientID %>").attr('readonly', false);
                $("#<%= txtFavMovies.ClientID %>").attr('readonly', false);
                $("#<%= txtFavBooks.ClientID %>").attr('readonly', false);
                $("#divEditAboutYouControls").removeClass('hidden');

            });

            $("#btnEditAboutYouCancel").click(function () {
                $(".fa-pen-square").each(function () { $(this).show() });
                $("#<%= txtFavSongs.ClientID %>").val(currentVals["txtFavSongs"]);
                $("#<%= txtFavSayings.ClientID %>").val(currentVals["txtFavSayings"]);
                $("#<%= txtFavRestaurants.ClientID %>").val(currentVals["txtFavRestuarants"]);
                $("#<%= txtFavMovies.ClientID %>").val(currentVals["txtFavMovies"]);
                $("#<%= txtFavBooks.ClientID %>").val(currentVals["txtFavBooks"]);

                HideAll();

            });
            //Is when when any cancel button is hit to ensure proper functionality.
            function HideAll() {

                $("#<%= txtTagline.ClientID %>").attr('readonly', true);
                $("#divEditTagLineControls").addClass('hidden');


                $("#<%= txtPhoneNumber.ClientID %>").attr('readonly', true);
                $("#<%= txtEmail.ClientID %>").attr('readonly', true);
                $("#divEditContactControls").addClass('hidden');

                $("#<%= txtBio.ClientID %>").attr('readonly', true);
                $("#<%= txtNumKids.ClientID %>").attr('readonly', true);
                $("#<%= ddlOccupation.ClientID %>").prop('disabled', true);
                $("#<%= ddlWantChildren.ClientID %>").prop('disabled', true);
                $("#<%= ddlSeeking.ClientID %>").prop('disabled', true);
                $("#divEditBasicControls").addClass('hidden');

                $("#<%= txtFavSongs.ClientID %>").attr('readonly', true);
                $("#<%= txtFavSayings.ClientID %>").attr('readonly', true);
                $("#<%= txtFavRestaurants.ClientID %>").attr('readonly', true);
                $("#<%= txtFavMovies.ClientID %>").attr('readonly', true);
                $("#<%= txtFavBooks.ClientID %>").attr('readonly', true);

                $("#divEditAboutYouControls").addClass('hidden');
            }

        });


    </script>
</asp:Content>

