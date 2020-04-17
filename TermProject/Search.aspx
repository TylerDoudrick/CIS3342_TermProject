<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="TermProject.Search" %>

<%@ Register Src="~/UserControls/ddl.ascx" TagPrefix="uc1" TagName="ddl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="row justify-content-center w-75 mx-auto">
        Something something OR and not AND
    </div>
    <uc1:ddl runat="server" ID="ddl" />
    <div class="row d-flex justify-content-center my-5 w-75 mx-auto">
        <div class="col">
            <label class="w-100 border-bottom d-block mb-3">Do they have kids?</label>
            <div class="row">
                <asp:DropDownList runat="server" ID="ddlHasKids" CssClass="selectpicker col-10">
                    <asp:ListItem Value="2" Selected="True">No Preference</asp:ListItem>
                    <asp:ListItem Value="1">Yes</asp:ListItem>
                    <asp:ListItem Value="0">No</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="col">
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
            <asp:Label runat="server" ID="Label2" CssClass="w-100 border-bottom d-block mb-3">Age Range:</asp:Label>
            <div class="row">
                <div class="col-3"><asp:TextBox runat="server" TextMode="Number" CssClass="width:50%;"></asp:TextBox></div>
                <div class="col-3"></div>
                <div class="col-3"><asp:TextBox runat="server" TextMode="Number" ></asp:TextBox></div>
            </div>
            <%--            <div class="d-inline-block"><asp:TextBox runat="server" TextMode="Number" CssClass="d-inline-block"></asp:TextBox></div>
            <div class="d-inline-block px-5">-</div>
            <div class="d-inline-block"><asp:TextBox runat="server" TextMode="Number" CssClass="d-inline-block"></asp:TextBox></div>--%>
        </div>
    </div>
    <div runat="server" class="row justify-content-center align-items-center" id="divSearch">
        <div>
            <asp:LinkButton type="button" ID="btnSearch" class="btn btn-info" runat="server" OnClick="btnSearch_Click">Search<i class="fas fa-search pl-2"></i></asp:LinkButton>
        </div>
    </div>

    <hr />
    <div runat="server" id="divResults" class="hidden">
        <h4 class="text-info font-weight-bold">Result Set</h4>

        <!-- Result set could be carousal that's in dashboard  -->
        <asp:GridView runat="server" ID="gvTemp"></asp:GridView>
    </div>
    <div class="row justify-content-center w-75 mx-auto my-4">
        <h3>Meet some hot singles in your area!</h3>
    </div>
    <div class="row justify-content-center w-75 mx-auto">
        <div class="col">
            <div class="owl-carousel owl-theme">
                <asp:Repeater runat="server" ID="rpCarousel">
                    <ItemTemplate>
                        <div class="card" style="width: 13rem;">
                            <img class="card-img-top" src="https://dummyimage.com/300x300/ffffff/a84a4a.jpg&text=<%#Eval("firstName") %>" alt="Card image cap">
                            <div class="card-body">
                                <h5 class="card-title"><%#Eval("firstName") %></h5>
                                <p><%#Eval("city") %>, <%#Eval("state") %></p>
                                <a href="MemberProfile.aspx" class="btn btn-primary stretched-link">Go somewhere</a>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

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
