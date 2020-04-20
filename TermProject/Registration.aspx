<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="TermProject.Registration" %>

<%@ Register Src="~/UserControls/ddl.ascx" TagPrefix="uc1" TagName="ddl" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <br />
    <br />
    <div class=" justify-content-center w-100">
     <!--   <h3 runat="server" class="font-weight-bold">Jenny James</h3> -->
    </div>
    <br />
    <asp:Label runat="server" ID="lblError" CssClass="text-danger font-weight-bold"></asp:Label>
    <h5 class="text-info font-weight-bold text-center">Bio</h5>
    <div class="row justify-content-center my-5">
        <div class="col-md-10">
            <asp:Label runat="server" ID="lblTagline"> Tagline </asp:Label>
            <span class="required">*</span>
            <asp:TextBox runat="server" ID="txtTagline" CssClass="form-control" MaxLength="200"></asp:TextBox>
        </div>
    </div>
    <div class="row justify-content-center my-5">
        <div class="col-md-10">
            <asp:Label runat="server" ID="lblBio"> Bio </asp:Label>
            <span class="required">*</span>
            <asp:TextBox runat="server" ID="txtBio" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>
    <hr />
    <div class="row justify-content-center align-items-center w-75 my-5 mx-auto">
         <div class="font-weight-bold text-info h5 my-auto">Basic Information</div>
    </div>
   
    <div class="row justify-content-center my-5">
        <div class="col-2">
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
                <asp:ListItem Value="-1"> Select Gender.. </asp:ListItem>
                <asp:ListItem Value="female"> Female </asp:ListItem>
                <asp:ListItem Value="male"> Male</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-2 form-group">
            <asp:Label runat="server" ID="lblBirthday" for="<%= txtBirthday.ClientID %>"> Birthday</asp:Label>
            <span class="required">*</span>
            <asp:TextBox type="date" CssClass="form-control" ID="txtBirthday" placeholder="Date" runat="server"></asp:TextBox>
        </div>
        <div class="col-2">
            <asp:Label runat="server" ID="lblPhotos"> Photos</asp:Label>
            <span class="required">*</span>
            <asp:FileUpload class="d-flex align-items-end" runat="server" ID="photoUpload" />
        </div>

    </div>
    <div class="row justify-content-center my-5">
        <div class="col-3">
            <asp:Label runat="server" ID="lblOccupation"> Occupation</asp:Label>
            <span class="required">*</span>
            <asp:DropDownList runat="server" ID="ddlOccupation" CssClass="form-control">
                <asp:ListItem Value="-1"> Select your occupation..</asp:ListItem>
                <asp:ListItem Value="Chiropractor">  Chiropractor</asp:ListItem>
                <asp:ListItem Value="Dentist">  Dentist</asp:ListItem>
                <asp:ListItem Value="Dietitian or Nutritionist">  Dietitian or Nutritionist</asp:ListItem>
                <asp:ListItem Value="Optometrist">  Optometrist</asp:ListItem>
                <asp:ListItem Value="Pharmacist">  Pharmacist</asp:ListItem>
                <asp:ListItem Value="Physician">  Physician</asp:ListItem>
                <asp:ListItem Value="Physician Assistant">  Physician Assistant</asp:ListItem>
                <asp:ListItem Value="Podiatrist">  Podiatrist</asp:ListItem>
                <asp:ListItem Value=" Registered Nurse">  Registered Nurse</asp:ListItem>
                <asp:ListItem Value="Therapist">  Therapist</asp:ListItem>
                <asp:ListItem Value="Veterinarian">  Veterinarian</asp:ListItem>
                <asp:ListItem Value="Health Technologist or Technician">  Health Technologist or Technician</asp:ListItem>
                <asp:ListItem Value="Other Healthcare or Technical Occupation">  Other Healthcare or Technical Occupation</asp:ListItem>
                <asp:ListItem Value="Nursing, Psychiatric, or Home Health Aide">  Nursing, Psychiatric, or Home Health Aide</asp:ListItem>
                <asp:ListItem Value="Occupational and PT Assistant/Aide">  Occupational and PT Assistant/Aide</asp:ListItem>
                <asp:ListItem Value="Other Healthcare Support Occupation">  Other Healthcare Support Occupation</asp:ListItem>
                <asp:ListItem Value="Chief Executive">  Chief Executive</asp:ListItem>
                <asp:ListItem Value="General and Operations Manager">  General and Operations Manager</asp:ListItem>
                <asp:ListItem Value="Advertising">  Advertising</asp:ListItem>
                <asp:ListItem Value="Promotion"> Promotion</asp:ListItem>
                <asp:ListItem Value="Public Relations"> Public Relations</asp:ListItem>
                <asp:ListItem Value="Sales"> Sales</asp:ListItem>
                <asp:ListItem Value="Marketing"> Marketing</asp:ListItem>
                <asp:ListItem Value="Operations Specialties Manager">  Operations Specialties Manager</asp:ListItem>
                <asp:ListItem Value=" Construction Manager">  Construction Manager</asp:ListItem>
                <asp:ListItem Value="Engineering Manager">  Engineering Manager</asp:ListItem>
                <asp:ListItem Value="Accountant, Auditor">  Accountant, Auditor</asp:ListItem>
                <asp:ListItem Value="Business Operations or Financial Specialist">  Business Operations or Financial Specialist</asp:ListItem>
                <asp:ListItem Value="Business Owner">  Business Owner</asp:ListItem>
                <asp:ListItem Value="Other Business or Executive">  Other Business or Executive</asp:ListItem>
                <asp:ListItem Value="Architect, Surveyor, or Cartographer">  Architect, Surveyor, or Cartographer</asp:ListItem>
                <asp:ListItem Value="Engineer">  Engineer</asp:ListItem>
                <asp:ListItem Value=" Other Architecture and Engineering Occupation">  Other Architecture and Engineering Occupation</asp:ListItem>
                <asp:ListItem Value=" Postsecondary Teacher">  Postsecondary Teacher</asp:ListItem>
                <asp:ListItem Value="Primary, Secondary, or Special Education School Teacher">  Primary, Secondary, or Special Education School Teacher</asp:ListItem>
                <asp:ListItem Value="Other Teacher or Instructor">  Other Teacher or Instructor</asp:ListItem>
                <asp:ListItem Value="Other Education, Training, and Library ">  Other Education, Training, and Library </asp:ListItem>
                <asp:ListItem Value="Arts, Entertainment, Sports,  Occupations">  Arts, Entertainment, Sports,  Occupations</asp:ListItem>
                <asp:ListItem Value="Computer Specialist, Mathematical Science">  Computer Specialist, Mathematical Science</asp:ListItem>
                <asp:ListItem Value="Social Worker">  Social Worker</asp:ListItem>
                <asp:ListItem Value="  Lawyer, Judge">  Lawyer, Judge</asp:ListItem>
                <asp:ListItem Value="Life Scientist">  Life Scientist </asp:ListItem>
                <asp:ListItem Value=" Physical Scientist">  Physical Scientist </asp:ListItem>
                <asp:ListItem Value=" Religious Worker">  Religious Worker</asp:ListItem>
                <asp:ListItem Value=" Social Scientist and Related Worker">  Social Scientist and Related Worker</asp:ListItem>
                <asp:ListItem Value=" Other Professional Occupation">  Other Professional Occupation</asp:ListItem>
                <asp:ListItem Value="Supervisor of Administrative Support Workers">  Supervisor of Administrative Support Workers</asp:ListItem>
                <asp:ListItem Value="Financial Clerk">  Financial Clerk</asp:ListItem>
                <asp:ListItem Value=" Secretary or Administrative Assistant">  Secretary or Administrative Assistant</asp:ListItem>
                <asp:ListItem Value=" Material Recording, Scheduling, and Dispatching Worker">  Material Recording, Scheduling, and Dispatching Worker</asp:ListItem>
                <asp:ListItem Value=" Other Office and Administrative Support">  Other Office and Administrative Support </asp:ListItem>
                <asp:ListItem Value="Fire Fighter">  Fire Fighter </asp:ListItem>
                <asp:ListItem Value=" Chef or Head Cook">  Chef or Head Cook</asp:ListItem>
                <asp:ListItem Value="Cook or Food Preparation Worker">  Cook or Food Preparation Worker</asp:ListItem>
                <asp:ListItem Value="Food and Beverage Serving Worker ">  Food and Beverage Serving Worker </asp:ListItem>
                <asp:ListItem Value="Building and Grounds Cleaning and Maintenance">  Building and Grounds Cleaning and Maintenance</asp:ListItem>
                <asp:ListItem Value="Personal Care and Service">  Personal Care and Service </asp:ListItem>
                <asp:ListItem Value=" Sales Supervisor, Retail Sales">  Sales Supervisor, Retail Sales</asp:ListItem>
                <asp:ListItem Value="Retail Sales Worker">  Retail Sales Worker</asp:ListItem>
                <asp:ListItem Value="Insurance Sales Agent">  Insurance Sales Agent</asp:ListItem>
                <asp:ListItem Value="Sales Representative">  Sales Representative</asp:ListItem>
                <asp:ListItem Value="Real Estate Sales Agent">  Real Estate Sales Agent</asp:ListItem>
                <asp:ListItem Value="Other Services Occupation">  Other Services Occupation</asp:ListItem>
                <asp:ListItem Value="Construction and Extraction">  Construction and Extraction </asp:ListItem>
                <asp:ListItem Value="Farming, Fishing, and Forestry">  Farming, Fishing, and Forestry</asp:ListItem>
                <asp:ListItem Value="Installation, Maintenance, and Repair">  Installation, Maintenance, and Repair</asp:ListItem>
                <asp:ListItem Value=" Production Occupations">  Production Occupations</asp:ListItem>
                <asp:ListItem Value="Other Agriculture,  and Skilled Crafts">  Other Agriculture,  and Skilled Crafts </asp:ListItem>
                <asp:ListItem Value=" Aircraft Pilot or Flight Engineer">  Aircraft Pilot or Flight Engineer</asp:ListItem>
                <asp:ListItem Value="Motor Vehicle Operator">  Motor Vehicle Operator </asp:ListItem>
                <asp:ListItem Value="Other Transportation Occupation">  Other Transportation Occupation</asp:ListItem>
                <asp:ListItem Value="Military">  Military</asp:ListItem>
                <asp:ListItem Value="Police Officer or Correctional Officer"> Police Officer or Correctional Officer</asp:ListItem>
                <asp:ListItem Value="Homemaker">  Homemaker</asp:ListItem>
                <asp:ListItem Value="Student"> Student</asp:ListItem>
                <asp:ListItem Value=" Other Occupation">  Other Occupation</asp:ListItem>
                <asp:ListItem Value=" Don't Know">  Don't Know</asp:ListItem>
                <asp:ListItem Value=" Not Applicable">  Not Applicable</asp:ListItem>

            </asp:DropDownList>
        </div>
        <div class="col-2 form-group">
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
       
    </div>
    <div class="row justify-content-center my-5">
        
        <div class="col-2">
            <asp:Label runat="server" ID="lblSeekingGender"> Seeking Gender</asp:Label>
            <span class="required">*</span>
            <asp:DropDownList ID="ddlSeeking" runat="server" CssClass="form-control">
                <asp:ListItem Value="-1">Select Seeking Gender..</asp:ListItem>
                <asp:ListItem Value="Female">Female</asp:ListItem>
                <asp:ListItem Value="Male">Male</asp:ListItem>
                <asp:ListItem Value="Both">Both</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-2">
            <asp:Label runat="server" ID="lblNumKids"> Number of Kids</asp:Label>
            <asp:TextBox runat="server" ID="txtNumKids" CssClass="form-control"> </asp:TextBox>

        </div>&nbsp; &nbsp;
        <div class="col-2">
            <asp:Label runat="server" ID="lblWantKids"> Do you want kids? </asp:Label>
            <asp:DropDownList ID="ddlWantChildren" runat="server" CssClass="form-control">
                <asp:ListItem Value="-1">Select Choice..</asp:ListItem>
                <asp:ListItem Value="1">Yes</asp:ListItem>
                <asp:ListItem Value="0">No</asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
    <hr /> 

   <div class="row justify-content-center align-items-center w-75 my-5 mx-auto">
        <div class="col text-center">
            <div class="font-weight-bold text-info h5 my-auto">About You</div>
        </div>
       </div>
    
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
    <uc1:ddl runat="server" id="ddl" />
    <br />
    <div class="row d-flex justify-content-center">
        <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn btn-success" OnClick="btnSave_Click" />
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="EndBodyPlaceHolder" runat="server">
    
<script>
       $('[id*=lbInterests]').multiselect
                ({
                    includeSelectAllOption: false,
                    nonSelectedText: 'Select atleast 1 interest...',
                    maxHeight: 200
                });
            $('[id*=lbLikes]').multiselect
                ({
                    includeSelectAllOption: false,
                    nonSelectedText: 'Select atleast 1 like...',
                    maxHeight: 200
                });
            $('[id*=lbDislikes]').multiselect
                ({
                    includeSelectAllOption: false,
                    nonSelectedText: 'Select atleast 1 dislike...',
                    maxHeight: 200
                });
            $('[id*=lbCommittment]').multiselect
                ({
                    includeSelectAllOption: false,
                    nonSelectedText: 'Select commitment type...',
                    maxHeight: 200
                });
            $('[id*=lbReligion]').multiselect
                ({
                    includeSelectAllOption: false,
                    nonSelectedText: 'Select a religion...',
                    maxHeight: 200
        });

</script>

</asp:Content>
