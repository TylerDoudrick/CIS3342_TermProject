<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="TermProject.Registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <style>
        .required {
            color: red;
        }

        h5 {
            color: darkblue;
            font-weight: bold;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <br />
    <br />
    <div class=" justify-content-center w-100">
        <h3 runat="server" class="font-weight-bold">Jenny James</h3>
    </div>
    <br />
    <asp:Label runat="server" ID="lblError" CssClass="text-danger font-weight-bold"></asp:Label>

    <h5>Basic Info</h5>
    <div class="row">
        <div class="col-3">
            <asp:Label runat="server" ID="lblPhoneNumber"> Phone Number</asp:Label>
            <span class="required">*</span>
            <div class="input-group">
                <asp:TextBox runat="server" ID="txtNumber1" CssClass="form-control" MaxLength="3"></asp:TextBox>
                &nbsp;
                <div class="input-group-append">
                    <span runat="server" class="d-flex align-items-end">- </span>&nbsp;
                </div>
                <asp:TextBox runat="server" ID="txtNumber2" CssClass="form-control" MaxLength="3"></asp:TextBox>
                &nbsp;
                <div class="input-group-append">
                    <span runat="server" class="d-flex align-items-end">- </span>&nbsp;
                </div>
                <asp:TextBox runat="server" ID="txtNumber3" CssClass="form-control" MaxLength="4"></asp:TextBox>
            </div>
        </div>
        <div class="col-2">
            <asp:Label runat="server" ID="lblGender">Gender</asp:Label>
            <span class="required">*</span>
            <asp:DropDownList runat="server" ID="ddlGender" CssClass="form-control">
                <asp:ListItem Value="-1"> Select.. </asp:ListItem>
                <asp:ListItem Value="female"> Female </asp:ListItem>
                <asp:ListItem Value="male"> Male</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-2 form-group">
            <asp:Label runat="server" ID="lblBirthday" for="<%= txtBirthday.ClientID %>"> Birthday</asp:Label>
            <span class="required">*</span>
            <asp:TextBox type="date" CssClass="form-control" ID="txtBirthday" placeholder="Date" runat="server"></asp:TextBox>
        </div>
        <div class="col-5">
            <asp:Label runat="server" ID="lblPhotos"> Photos</asp:Label>
            <span class="required">*</span>
            <asp:FileUpload class="d-flex align-items-end" runat="server" ID="photoUpload" />
        </div>

    </div>
    <div class="row my-5">
        <div class="col-3 form-group">
            <asp:Label runat="server" ID="lblHeight"> Height</asp:Label>
            <div class="input-group">
                <asp:TextBox runat="server" ID="txtHeightFT" CssClass="form-control"></asp:TextBox>
                &nbsp;&nbsp;
                <div class="input-group-append">
                    <span runat="server" class="d-flex align-items-end">ft.</span>&nbsp;&nbsp;
                </div>
                <asp:TextBox runat="server" ID="txtHeightIn" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;
                <div class="input-group-append">
                    <span runat="server" class="d-flex align-items-end">in.</span>
                </div>
            </div>
        </div>
        <div class="col-1 form-group">
            <asp:Label runat="server" ID="lblWeight"> Weight</asp:Label>
            <div class="input-group">
                <asp:TextBox runat="server" ID="txtWeight" CssClass="form-control"></asp:TextBox>
                &nbsp;&nbsp;
                <div class="input-group-append">
                    <span runat="server" class="d-flex align-items-end">lbs. </span>
                </div>
            </div>
        </div>
        <div class="col-3 form-group">
            <asp:Label runat="server" ID="lblReligion"> Religion</asp:Label>
            <span class="required">*</span>
            <asp:DropdownList runat="server" ID="ddlReligion" CssClass="form-control" AppendDataBoundItems="true">
                <asp:ListItem Value="-1"> Select Religion...</asp:ListItem>
            </asp:DropdownList>
        </div>
        <div class="col-3">
            <asp:Label runat="server" ID="lblCommitment">Commitment Type</asp:Label>
            <span class="required">*</span>
            <asp:DropdownList runat="server" ID="ddlCommittment" CssClass="form-control" AppendDataBoundItems="true">
                <asp:ListItem Value="-1"> Select Commitment Type...</asp:ListItem>
            </asp:DropdownList>
        </div>
    </div>
    <div class="row justify-content-center my-5">
        <div class="col-5">
            <asp:Label runat="server" ID="lblOccupation"> Occupation</asp:Label>
            <span class="required">*</span>
            <asp:DropDownList runat="server" ID="ddlOccupation" CssClass="form-control">
                <asp:ListItem Value="-1"> Select your occupation..</asp:ListItem>
                <asp:ListItem Value="1">  Chiropractor</asp:ListItem>
                <asp:ListItem Value="2">  Dentist</asp:ListItem>
                <asp:ListItem Value="3">  Dietitian or Nutritionist</asp:ListItem>
                <asp:ListItem Value="4">  Optometrist</asp:ListItem>
                <asp:ListItem Value="5">  Pharmacist</asp:ListItem>
                <asp:ListItem Value="6">  Physician</asp:ListItem>
                <asp:ListItem Value="7">  Physician Assistant</asp:ListItem>
                <asp:ListItem Value="8">  Podiatrist</asp:ListItem>
                <asp:ListItem Value="9">  Registered Nurse</asp:ListItem>
                <asp:ListItem Value="10">  Therapist</asp:ListItem>
                <asp:ListItem Value="11">  Veterinarian</asp:ListItem>
                <asp:ListItem Value="12">  Health Technologist or Technician</asp:ListItem>
                <asp:ListItem Value="13">  Other Healthcare or Technical Occupation</asp:ListItem>
                <asp:ListItem Value="14">  Nursing, Psychiatric, or Home Health Aide</asp:ListItem>
                <asp:ListItem Value="15">  Occupational and PT Assistant/Aide</asp:ListItem>
                <asp:ListItem Value="16">  Other Healthcare Support Occupation</asp:ListItem>
                <asp:ListItem Value="17">  Chief Executive</asp:ListItem>
                <asp:ListItem Value="18">  General and Operations Manager</asp:ListItem>
                <asp:ListItem Value="19">  Advertising</asp:ListItem>
                <asp:ListItem Value="19.2"> Promotion</asp:ListItem>
                <asp:ListItem Value="19.3"> Public Relations</asp:ListItem>
                <asp:ListItem Value="19.4"> Sales</asp:ListItem>
                <asp:ListItem Value="19.1"> Marketing</asp:ListItem>
                <asp:ListItem Value="20">  Operations Specialties Manager</asp:ListItem>
                <asp:ListItem Value="21">  Construction Manager</asp:ListItem>
                <asp:ListItem Value="22">  Engineering Manager</asp:ListItem>
                <asp:ListItem Value="23">  Accountant, Auditor</asp:ListItem>
                <asp:ListItem Value="24">  Business Operations or Financial Specialist</asp:ListItem>
                <asp:ListItem Value="25">  Business Owner</asp:ListItem>
                <asp:ListItem Value="26">  Other Business or Executive</asp:ListItem>
                <asp:ListItem Value="27">  Architect, Surveyor, or Cartographer</asp:ListItem>
                <asp:ListItem Value="28">  Engineer</asp:ListItem>
                <asp:ListItem Value="29">  Other Architecture and Engineering Occupation</asp:ListItem>
                <asp:ListItem Value="30">  Postsecondary Teacher</asp:ListItem>
                <asp:ListItem Value="31">  Primary, Secondary, or Special Education School Teacher</asp:ListItem>
                <asp:ListItem Value="32">  Other Teacher or Instructor</asp:ListItem>
                <asp:ListItem Value="33">  Other Education, Training, and Library </asp:ListItem>
                <asp:ListItem Value="34">  Arts, Entertainment, Sports,  Occupations</asp:ListItem>
                <asp:ListItem Value="35">  Computer Specialist, Mathematical Science</asp:ListItem>
                <asp:ListItem Value="36">  Social Worker</asp:ListItem>
                <asp:ListItem Value="37">  Lawyer, Judge</asp:ListItem>
                <asp:ListItem Value="38">  Life Scientist </asp:ListItem>
                <asp:ListItem Value="39">  Physical Scientist </asp:ListItem>
                <asp:ListItem Value="40">  Religious Worker</asp:ListItem>
                <asp:ListItem Value="41">  Social Scientist and Related Worker</asp:ListItem>
                <asp:ListItem Value="42">  Other Professional Occupation</asp:ListItem>
                <asp:ListItem Value="43">  Supervisor of Administrative Support Workers</asp:ListItem>
                <asp:ListItem Value="44">  Financial Clerk</asp:ListItem>
                <asp:ListItem Value="45">  Secretary or Administrative Assistant</asp:ListItem>
                <asp:ListItem Value="46">  Material Recording, Scheduling, and Dispatching Worker</asp:ListItem>
                <asp:ListItem Value="47">  Other Office and Administrative Support </asp:ListItem>
                <asp:ListItem Value="48">  Fire Fighter </asp:ListItem>
                <asp:ListItem Value="49">  Chef or Head Cook</asp:ListItem>
                <asp:ListItem Value="50">  Cook or Food Preparation Worker</asp:ListItem>
                <asp:ListItem Value="51">  Food and Beverage Serving Worker </asp:ListItem>
                <asp:ListItem Value="52">  Building and Grounds Cleaning and Maintenance</asp:ListItem>
                <asp:ListItem Value="53">  Personal Care and Service </asp:ListItem>
                <asp:ListItem Value="54">  Sales Supervisor, Retail Sales</asp:ListItem>
                <asp:ListItem Value="55">  Retail Sales Worker</asp:ListItem>
                <asp:ListItem Value="56">  Insurance Sales Agent</asp:ListItem>
                <asp:ListItem Value="57">  Sales Representative</asp:ListItem>
                <asp:ListItem Value="58">  Real Estate Sales Agent</asp:ListItem>
                <asp:ListItem Value="59">  Other Services Occupation</asp:ListItem>
                <asp:ListItem Value="60">  Construction and Extraction </asp:ListItem>
                <asp:ListItem Value="61">  Farming, Fishing, and Forestry</asp:ListItem>
                <asp:ListItem Value="62">  Installation, Maintenance, and Repair</asp:ListItem>
                <asp:ListItem Value="63">  Production Occupations</asp:ListItem>
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
                <asp:ListItem Value="73">  Not Applicable</asp:ListItem>

            </asp:DropDownList>
        </div>
        <div class="col-2">
            <asp:Label runat="server" ID="lblSeekingGender"> Seeking Gender</asp:Label>
            <span class="required">*</span>
            <div class="form-group align-items-end ">
                <asp:CheckBox runat="server" ID="chkSeekingFemale" Text="Female" />
                &nbsp;&nbsp;
                <asp:CheckBox runat="server" ID="chkSeekingMale" Text="Male" />
            </div>
        </div>
        <div class="col-2">
            <asp:Label runat="server" ID="lblNumKids"> Number of Kids</asp:Label>
            <asp:TextBox runat="server" ID="txtNumKids" CssClass="form-control"> </asp:TextBox>
        </div>
        <div class="col-3">
            <asp:Label runat="server" ID="lblWantKids"> Do you want kids? </asp:Label>
            <div class="form-group">
                <asp:RadioButton runat="server" ID="rYes" Text="Yes" />&nbsp;&nbsp; &nbsp;
                <asp:RadioButton runat="server" ID="rNo" Text="No" />
            </div>
        </div>
    </div>

    <h5>Favorite Things </h5>
    <div class="row justify-content-center my-5">
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
    <div class="row justify-content-center my-5">
        <div class="col-4">
            <asp:Label runat="server" ID="lblFavMovies">Favorite Movies</asp:Label>
            <asp:TextBox runat="server" ID="txtFavMovies" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
        </div>
        <div class="col-4">
            <asp:Label runat="server" ID="lblFavBooks">Favorite Books</asp:Label>
            <asp:TextBox runat="server" ID="txtFavBooks" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>

        </div>
    </div>
    <div class="row justify-content-center my-5">
        <div class="col-3">
            <asp:Label runat="server" ID="lblInterests"> Interests</asp:Label>
            <span class="required">*</span>
            <asp:ListBox runat="server" ID="lbInterests" CssClass="form-control" SelectionMode="Multiple">
            </asp:ListBox>
        </div>
        <div class="col-3">
            <asp:Label runat="server" ID="lblLikes"> Likes</asp:Label>
            <span class="required">*</span>
            <asp:ListBox runat="server" ID="lbLikes" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
        </div>
        <div class="col-3">
            <asp:Label runat="server" ID="lblDislikes"> Dislikes</asp:Label>
            <span class="required">*</span>
            <asp:ListBox runat="server" ID="lbDislikes" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
        </div>
    </div>

    <h5>Bio</h5>
    <div class="row">
        <div class="col-md-10">
            <asp:Label runat="server" ID="lblTagline"> Tagline </asp:Label>
            <span class="required">*</span>
            <asp:TextBox runat="server" ID="txtTagline" CssClass="form-control" MaxLength="200"></asp:TextBox>
        </div>
    </div>
    <div class="row my-5">
        <div class="col-md-10">
            <asp:Label runat="server" ID="lblBio"> Bio </asp:Label>
            <span class="required">*</span>
            <asp:TextBox runat="server" ID="txtBio" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>
    <br />
    <div class="row d-flex justify-content-center">
        <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn btn-success" OnClick="btnSave_Click" />
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="EndBodyPlaceHolder" runat="server">
    >
    <script>
        $(function () {
            $('[id*=lbInterests]').multiselect
                ({
                    includeSelectAllOption: false,
                    nonSelectedText: 'Select atleast 1 interest...' // Here you can change with your desired text as per your requirement.
                });
            $('[id*=lbLikes]').multiselect
                ({
                    includeSelectAllOption: false,
                    nonSelectedText: 'Select atleast 1 like...' // Here you can change with your desired text as per your requirement.
                });
            $('[id*=lbDislikes]').multiselect
                ({
                    includeSelectAllOption: false,
                    nonSelectedText: 'Select atleast 1 dislike...' // Here you can change with your desired text as per your requirement.
                });
        });
    </script>
</asp:Content>
