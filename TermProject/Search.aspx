<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="TermProject.Search" %>

<%@ Register Src="~/UserControls/ddl.ascx" TagPrefix="uc1" TagName="ddl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
       <div runat="server" id="divSearchOptions">

    <div class="row justify-content-center w-75 mx-auto">
        Something something OR and not AND
    </div>
    <uc1:ddl runat="server" ID="ddl" />
    <div class="row d-flex justify-content-center my-5 w-75 mx-auto" >
        <div class="col"  runat="server" id="divHaveKids">
            <label class="w-100 border-bottom d-block mb-3">Do they have kids?</label>
            <div class="row">
                <asp:DropDownList runat="server" ID="ddlHasKids" CssClass="selectpicker col-10">
                    <asp:ListItem Value="2" Selected="True">No Preference</asp:ListItem>
                    <asp:ListItem Value="1">Yes</asp:ListItem>
                    <asp:ListItem Value="0">No</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="col" runat="server" id="divWantKids">
            <asp:Label runat="server" ID="Label1" CssClass="w-100 border-bottom d-block mb-3">Do they want children?</asp:Label>
            <div class="row">
                <asp:DropDownList runat="server" ID="ddlWantKids" CssClass="selectpicker col-10">
                    <asp:ListItem Value="2" Selected="True">No Preference</asp:ListItem>
                    <asp:ListItem Value="1">Yes</asp:ListItem>
                    <asp:ListItem Value="0">No</asp:ListItem>
                </asp:DropDownList>
            </div>

        </div>
                <div class="col">
            <asp:Label runat="server" ID="Label3" CssClass="w-100 border-bottom d-block mb-3">Gender</asp:Label>
            <div class="row">
                <asp:DropDownList runat="server" ID="ddlGender" CssClass="selectpicker col-10">
                    <asp:ListItem Value="0" Selected="True">No Preference</asp:ListItem>
                    <asp:ListItem Value="Male">Male</asp:ListItem>
                    <asp:ListItem Value="Female">Female</asp:ListItem>
                </asp:DropDownList>
            </div>

        </div>
        <div class="col">
            <asp:Label runat="server" ID="Label2" CssClass="w-100 border-bottom d-block mb-3">Age Range:</asp:Label>
            <div class="row justify-content-start align-items-center">
                <div class="d-inline pl-3"><asp:TextBox runat="server" id="txtMinAge" TextMode="Number" CssClass="form-control" min="18" MaxLength="110" Columns="3">18</asp:TextBox></div>
                <div class="d-inline px-3"><span style="font-size:2em;font-weight:bold">-</span></div>
                <div class="d-inline"><asp:TextBox runat="server" TextMode="Number" ID="txtMaxAge" CssClass="form-control" min="18" max="110" Columns="3">24</asp:TextBox></div>
            </div>

        </div>
    </div>
           <div class="row my-5" runat="server" id="divWantMore" visible="false">
               <div class="col d-flex justify-content-center flex-column align-items-center">
                                  <h5>Want more search options?</h5>
<asp:LinkButton href="CreateAccount.aspx" runat="server" CssClass="btn btn-primary">Create an Account</asp:LinkButton>
               </div>
           </div>
    <div runat="server" class="row justify-content-center align-items-center" id="divSearch">
        <div>
            <asp:LinkButton type="button" ID="btnSearch" class="btn btn-info" runat="server" OnClick="btnSearch_Click">Search<i class="fas fa-search pl-2"></i></asp:LinkButton>
        </div>
    </div>
                </div>

    <div id="divSearchControls" runat="server" visible="false" class="row my-5 d-flex justify-content-center align-items-center text-right">
        <asp:Button ID="btnShowSearch" runat="server" CssClass="btn btn-info" Text="Show Search Options" OnClick="btnShowSearch_Click" />
    </div>
    <hr />
<%--    <div runat="server" id="divResults" class="hidden">
        <h4 class="text-info font-weight-bold">Result Set</h4>

        <asp:GridView runat="server" ID="gvTemp"></asp:GridView>
    </div>--%>
    <div id="searchResults" visible="false" runat="server">
    <div class="row justify-content-center w-75 mx-auto my-4">
        <h3>We've found some matches!</h3>
    </div>
    <div class="row justify-content-center w-100 mx-auto">
        <div class="col">
            <div class="owl-carousel owl-theme h-100">
                <asp:Repeater runat="server" ID="rpCarousel">
                    <ItemTemplate>
                                    <div class="col-sm d-flex flex-grow-1">
                        <div class="card flex-fill" style="width: 20rem;min-height:4em;">
                            <img class="card-img-top" src='<%#Eval("imageSRC") %>' alt="Card image cap">
                            <div class="card-body">
                                <h5 class="card-title h-100"><%#Eval("firstName") %> (<%#Eval("gender").ToString().ToUpper()[0] %>), <%#Eval("age") %></h5>
                                <p><%#Eval("occupation")%></p>
                                <p><%#Eval("city") %>, <%#Eval("state") %></p>
                                <p><%#Eval("tagline") %></p>
                                <a href="MemberProfile.aspx?memberID=<%#Eval("profileID") %>" class="btn btn-primary stretched-link">View Profile</a>
                            </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

            </div>
            <span id="searchAnchor"></span>
        </div>
    </div>
        </div>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="EndBodyPlaceHolder" runat="server">
    <script>
        $(document).ready(function () {
            $("#navlinkDashboard").removeClass("active");
            $("#navlinkSearch").addClass("active");



            $(".owl-carousel").owlCarousel({
                loop: true,

                nav: true,
                responsive: {
                    0: {
                        items: 1
                    },
                    600: {
                        items: 3
                    },
                    1000: {
                        items: 5
                    }
                }
            });
        });




    </script>
</asp:Content>
