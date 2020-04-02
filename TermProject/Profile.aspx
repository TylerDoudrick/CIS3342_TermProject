<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="TermProject.Profile" %>

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
    <br />
    <br />

    <div>
        <div class="row justify-content-center my-5">
            <div class="col-2">
                <asp:Image runat="server" CssClass="img-thumbnail" ImageUrl="https://www.skymania.com/wp/wp-content/uploads/2011/06/sun_with_prominence.jpg" />
            </div>
            <div class="col-4 ">
                <h5 class="text-info font-weight-bold ">Sam Smith, 25</h5>
                <asp:Label runat="server" ID="lblLocation"> Philadelphia, PA</asp:Label>
                <br />
                <asp:TextBox runat="server" ID="txtTagline" ReadOnly="true" Text="This is the Tagline" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>
    </div>
    <hr />
    <asp:LinkButton runat="server" ID="lblEditContact" OnClick="lblEditContact_Click">
            <span> <i class="fas fa-pen-square fa-2x"></i></span>
    </asp:LinkButton>
    <br />
    <br />
    <div class="row">
        <h5 class="font-weight-bold text-info">Contact Information</h5>
    </div>
    <div class="row justify-content-center my-5">
        <div class="col-4">

            <div class="row">
                <asp:Label runat="server" ID="lblPhoneNumber" for="<%= txtFullNumber.ClientID %>"> Phone Number</asp:Label>
                <span class="required ">*</span>
                <div class="col">
                    <div class="input-group" id="txtFullNumber">
                        <asp:TextBox runat="server" ID="txtNumber1" CssClass="form-control" MaxLength="3" ReadOnly="true"></asp:TextBox>
                        &nbsp;
                            - 
                    </div>
                </div>
                <div class="col">
                    <div class="input-group">
                        <asp:TextBox runat="server" ID="txtNumber2" CssClass="form-control" MaxLength="3" ReadOnly="true"></asp:TextBox>
                        &nbsp;
                        -                       
                    </div>
                </div>
                <div class="col">
                    <asp:TextBox runat="server" ID="txtNumber3" CssClass="form-control" MaxLength="4" ReadOnly="true"></asp:TextBox>

                </div>
            </div>
            <br />
            <div class="row">
                <asp:Label runat="server" ID="lblEmail" for="<%= txtEmail.ClientID %>">Email </asp:Label>
                <span class="required ">*</span>
                <div class="col">
                    <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
    <div class="row justify-content-center hidden" runat="server" id="divBtnUpdate1">
        <asp:Button runat="server" Text="Update" CssClass="btn btn-success mr-2 h-50 " ID="btnUpdate1" OnClick="btnUpdate1_Click" />
                <asp:Button runat="server" Text="Cancel" CssClass="btn btn-danger h-50" ID="btnCancel1" OnClick="btnCancel1_Click"/>
    </div>

    <hr />
    <asp:LinkButton runat="server" ID="lbEditBasicInfo" OnClientClick="showHiddenOne()" OnClick="lbEditBasicInfo_Click">
            <span> <i class="fas fa-pen-square fa-2x"></i></span>
    </asp:LinkButton>

    <br />
    <br />
    <h5 class="text-info font-weight-bold ">Basic Information</h5>
    <div class="row justify-content-center my-5">
        <div class="col-md-9">
            <asp:Label runat="server" ID="lblBio"> Bio </asp:Label>
            <span class="required ">*</span>
            <asp:TextBox runat="server" ID="txtBio" CssClass="form-control" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
        </div>
    </div>

    <div class="row justify-content-center my-5">
        <div class="col-2 form-group">
            <asp:Label runat="server" ID="lblReligion"> Religion</asp:Label>
            <span class="required ">*</span>
            <asp:DropDownList runat="server" ID="ddlReligion" CssClass="form-control">
            </asp:DropDownList>
        </div>
        <div class="col-2">
            <asp:Label runat="server" ID="lblCommitment">Commitment Type</asp:Label>
            <span class="required ">*</span>
            <asp:DropDownList runat="server" ID="ddlCommittment" CssClass="form-control">
            </asp:DropDownList>
        </div>
        <div class="col-2">
            <asp:Label runat="server" ID="lblOccupation"> Occupation</asp:Label>
            <span class="required ">*</span>
            <asp:DropDownList runat="server" ID="ddlOccupation" CssClass="form-control">
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
    <div class="row justify-content-center my-5">
        <div class="col-2">
            <asp:Label runat="server" ID="lblSeekingGender"> Seeking Gender</asp:Label>
            <span class="required ">*</span>
            <div class="form-group align-items-end ">
                <asp:CheckBox runat="server" ID="chkSeekingFemale" Text="Female" />
                &nbsp;&nbsp;
                <asp:CheckBox runat="server" ID="chkSeekingMale" Text="Male" />
            </div>
        </div>
        <div class="col-2">
            <asp:Label runat="server" ID="lblNumKids"> Number of Kids</asp:Label>
            <asp:TextBox runat="server" ID="txtNumKids" CssClass="form-control" ReadOnly="true"> </asp:TextBox>
        </div>
        <div class="col-">
            <asp:Label runat="server" ID="lblWantKids"> Do you want kids? </asp:Label>
            <div class="form-group">
                <asp:RadioButton runat="server" ID="rWantKidsYes" Text="Yes" />&nbsp;&nbsp; &nbsp;
                <asp:RadioButton runat="server" ID="rWantKidsNo" Text="No" />
            </div>
        </div>
    </div>

    <div class="row justify-content-center hidden" runat="server" id="divBtnUpdate2">
        <asp:Button runat="server" Text="Update" CssClass="btn btn-success mr-2 h-50 " ID="btnUpdate2" OnClick="btnUpdate2_Click" />
                <asp:Button runat="server" Text="Cancel" CssClass="btn btn-danger h-50" ID="btnCancel2" OnClick="btnCancel2_Click"/>
    </div>
    <hr />
    <asp:LinkButton runat="server" ID="lbEdit" OnClick="lbEdit_Click">
            <span> <i class="fas fa-pen-square fa-2x"></i></span>
    </asp:LinkButton>
    <br />
    <br />
    <h5 class="text-info font-weight-bold ">Favorite Things</h5>
    <div class="row justify-content-center my-5">
        <div class="col-3">
            <asp:Label runat="server" ID="lblInterests"> Interests </asp:Label>
            <span class="required hidden">*</span>
            <asp:ListBox runat="server" ID="lbInterests" CssClass="form-control" SelectionMode="Multiple" ReadOnly="true"></asp:ListBox>
        </div>
        <div class="col-3">
            <asp:Label runat="server" ID="lblLikes"> Likes </asp:Label>
            <span class="required hidden">*</span>
            <asp:ListBox runat="server" ID="lbLikes" CssClass="form-control" SelectionMode="Multiple" ReadOnly="true"></asp:ListBox>
        </div>
        <div class="col-3">
            <asp:Label runat="server" ID="lblDislikes"> Dislikes </asp:Label>
            <span class="required hidden">*</span>
            <asp:ListBox runat="server" ID="lbDislikes" CssClass="form-control" SelectionMode="Multiple" ReadOnly="true"></asp:ListBox>
        </div>
    </div>

    <div class="row justify-content-center hidden" runat="server" id="divBtnUpdate3">
        <asp:Button runat="server" Text="Update" CssClass="btn btn-success mr-2 h-50  " ID="btnUpdate3" OnClick="btnUpdate3_Click" />
        <asp:Button runat="server" Text="Cancel" CssClass="btn btn-danger h-50" ID="btnCancel3" OnClick="btnCancel3_Click" />
    </div>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="EndBodyPlaceHolder" runat="server">
    <script>
        $(document).ready(function () {
            $("#navlinkDashboard").removeClass("active");
            $("#navlinkProfile").addClass("active");

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

        });


    </script>
</asp:Content>

