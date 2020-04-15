<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="TermProject.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <style>
        .card{
            height: 31em;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:Button runat="server" Text="clickme" OnClick="Unnamed_Click"/>
    <div class="row justify-content-center align-items-center w-75 my-5 mx-auto">
        <div class="col-6 h-100">
            <div class="card text-center h-100">
                <h4 class="display-4">Messages</h4>
                <div class="card-body">
                    <i class="fas fa-envelope" style="font-size: 13em;"></i>
                    <h3>3 Unread Messages</h3>
                    <a href="Messages.aspx" class="btn btn-primary stretched-link my-2">Go to Inbox</a>
                </div>
            </div>
        </div>
        <div class="col-6 h-100">
            <div class="card text-center h-100">
                <h3 class="display-4">Dates</h3>
                <div class="card-body">
                    <i class="fas fa-heart" style="font-size: 13em;"></i>
                    <h3>3 Planned Dates</h3>
                    <a href="Dates.aspx" class="btn btn-primary stretched-link my-2">Go to Dates</a>
                </div>
            </div>

        </div>
    </div>
    <div class="row justify-content-center w-75 mx-auto my-4">
        <h3>Meet some hot singles in your area!</h3>
    </div>

    <div class="col">
            <div class="owl-carousel owl-theme">
                <asp:Repeater runat="server" ID="rptPeople">
                    <ItemTemplate>
                        <div>
                            <div class="card">
                                <div runat="server">
                                    <img class="card-img-top" src='<%#Eval("imageSRC") %>'> </img>
                                </div>

                                <div class="card-body">
                                    <asp:Label CssClass="card-text font-weight-bold" ID="lblFirstName" runat="server" Text='<%#Eval("name") %>' ></asp:Label>
                                    <br />
                                    <asp:Label ID="lblTagline" CssClass="card-text text-left" runat="server" Text='<%#Eval("tagline") %>'></asp:Label>
                                </div>
                                <div class="card-footer">
                                    <asp:LinkButton runat="server" CommandName= ' <%#DataBinder.Eval(Container.DataItem, "userID") %>' CssClass="btn btn-secondary " ID="lbGoToProfile" OnCommand="lbGoToProfile_Command"> Go to Profile </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>


    <!-- 
    <div class="row justify-content-center w-75 mx-auto">
        <div class="col">
            <div class="owl-carousel owl-theme">
                <div class="card" style="width: 13rem;">
                    <img class="card-img-top" src="https://dummyimage.com/300x300/ffffff/a84a4a.jpg&text=Lizzie" alt="Card image cap">
                    <div class="card-body">
                        <h5 class="card-title">Card title</h5>
                        <a href="MemberProfile.aspx" class="btn btn-primary stretched-link">Go somewhere</a>
                    </div>
                </div>
                <div class="card" style="width: 13rem;">
                    <img class="card-img-top" src="https://dummyimage.com/300x300/ffffff/a84a4a.jpg&text=John" alt="Card image cap">
                    <div class="card-body">
                        <h5 class="card-title">Card title</h5>
                        <a href="MemberProfile.aspx" class="btn btn-primary stretched-link">Go somewhere</a>
                    </div>
                </div>
                <div class="card" style="width: 13rem;">
                    <img class="card-img-top" src="https://dummyimage.com/300x300/ffffff/a84a4a.jpg&text=Mary" alt="Card image cap">
                    <div class="card-body">
                        <h5 class="card-title">Card title</h5>
                        <a href="MemberProfile.aspx" class="btn btn-primary stretched-link">Go somewhere</a>
                    </div>
                </div>
                <div class="card" style="width: 13rem;">
                    <img class="card-img-top" src="https://dummyimage.com/300x300/ffffff/a84a4a.jpg&text=Kyle" alt="Card image cap">
                    <div class="card-body">
                        <h5 class="card-title">Card title</h5>
                        <a href="MemberProfile.aspx" class="btn btn-primary stretched-link">Go somewhere</a>
                    </div>
                </div>
                <div class="card" style="width: 13rem;">
                    <img class="card-img-top" src="https://dummyimage.com/300x300/ffffff/a84a4a.jpg&text=Karen" alt="Card image cap">
                    <div class="card-body">
                        <h5 class="card-title">Card title</h5>
                        <a href="MemberProfile.aspx" class="btn btn-primary stretched-link">Go somewhere</a>
                    </div>
                </div>
                <div class="card" style="width: 13rem;">
                    <img class="card-img-top" src="https://dummyimage.com/300x300/ffffff/a84a4a.jpg&text=Molly" alt="Card image cap">
                    <div class="card-body">
                        <h5 class="card-title">Card title</h5>
                        <a href="MemberProfile.aspx" class="btn btn-primary stretched-link">Go somewhere</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
        -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="EndBodyPlaceHolder" runat="server">
    <script>
        $(document).ready(function () {
            $(".owl-carousel").owlCarousel({
                loop: true,
                
                nav: true,
                    responsive:{
        0:{
            items:1
        },
        600:{
            items:3
        },
        1000:{
            items:5
        }
    }
            });
        });
        $('.card-img-top').each(function () {
            $(this).attr("src", );
        });
    </script>
</asp:Content>
