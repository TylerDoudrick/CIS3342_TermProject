﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="LikeandPass.aspx.cs" Inherits="TermProject.LikeandPass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <style>
        .card{
            height:auto;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="row w-100 my-5 justify-content-center">
        <h5 class="text-info font-weight-bold ">Like </h5>
    </div>
    <div class="row justify-content-center w-75 mx-auto">
        <!-- Result set could be carousal that's in dashboard  -->
        <div class="col">
            <div class="owl-carousel owl-theme">
                <asp:Repeater runat="server" ID="rptmemLikes">
                    <ItemTemplate>
                        <div>
                            <div class="card">
                                <div runat="server">
                                    <img class="card-img-top" src='<%#Eval("imageSRC") %>'> </img>
                                </div>

                                <div class="card-body">
                                   <p>
                                        <asp:Label CssClass="card-text font-weight-bold text-center" ID="lblName" runat="server" Text='<%#Eval("heading") %>'></asp:Label><br />
                                        <asp:Label ID="lblOccupation" CssClass="card-text text-left" runat="server" Text='<%#Eval("occuption") %>'></asp:Label>
                                    </p>
                                                                                                        <p><%#Eval("city") %>, <%#Eval("state") %></p>
                                    <asp:Label ID="lblTagline" CssClass="card-text text-left" runat="server" Text='<%#Eval("tagline") %>'></asp:Label>
                                </div>
                                <div class="card-footer text-center">
                                    <asp:LinkButton runat="server" CommandName= ' <%#DataBinder.Eval(Container.DataItem, "userID") %>' CssClass="btn btn-secondary w-100" ID="lbUnlike" OnCommand="lbUnlike_Command"> Unlike</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>


    <hr />

    <div class="row my-5 w-100 justify-content-center">
        <h5 class="text-info font-weight-bold ">Pass</h5>
    </div>
    <div class="row justify-content-center w-75 mx-auto">
        <!-- Result set could be carousal that's in dashboard  -->

        <div class="col">
            <div class="owl-carousel owl-theme">
                <asp:Repeater runat="server" ID="rptDislikes">
                    <ItemTemplate>
                        <div>
                            <div class="card">
                                <div runat="server">
                                    <img class="card-img-top" src='<%#Eval("imageSRC") %>'/>
                                </div>

                                <div class="card-body">
                                    <p>
                                        <asp:Label CssClass="card-text font-weight-bold text-center" ID="headingPass" runat="server" Text='<%#Eval("heading") %>'></asp:Label>
                                      <br />  <asp:Label ID="occupationPass" CssClass="card-text text-left" runat="server" Text='<%#Eval("occuption") %>'></asp:Label>
                                    </p>
                                                                                                        <p><%#Eval("city") %>, <%#Eval("state") %></p>
                                    <asp:Label ID="tasglinePass" CssClass="card-text text-left" runat="server" Text='<%#Eval("tagline") %>'></asp:Label>
                                </div>
                                <div class="card-footer">
                                    <asp:LinkButton runat="server" CommandName= ' <%#DataBinder.Eval(Container.DataItem, "userID") %>' CssClass="btn btn-secondary w-100 mb-1" ID="lbLikeFromPass" OnCommand="lbLikeFromPass_Command"> Like </asp:LinkButton>
                                    <asp:LinkButton runat="server" CommandName= ' <%#DataBinder.Eval(Container.DataItem, "userID") %>' CssClass="btn btn-warning w-100" ID="removePass" OnCommand="removePass_Command"> Remove from Pass </asp:LinkButton>
                                </div>
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
            $(".owl-carousel").owlCarousel({
                loop: false,

                nav: true,
                responsive: {
                    0: {
                        items: 1
                    },
                    600: {
                        items: 3
                    },
                    1000: {
                        items: 4
                    }
                }
            });
        });
        $('.card-img-top').each(function () {
            $(this).attr("src");
        });
    </script>
</asp:Content>
