<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Dates.aspx.cs" Inherits="TermProject.Dates" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">

    <style>
        .message:hover {
            background: rgb(250,250,250);
            cursor: pointer;
            text-decoration: none;
            color: black;
        }

        .message div:hover {
            background: rgb(250,250,250);
        }

        .message {
            color: black;
        }


        .theme-red {
            background: #BC3440;
            color: white;
        }

            .theme-red:hover {
                color: white;
            }

        .theme-white {
            color: #BC3440;
            background: #fff;
            border: 1px solid #BC3440;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">

    <div class="row justify-content-center align-items-center w-75 my-5 mx-auto ">
        <div class="btn-group btn-group-toggle" data-toggle="buttons" id="divMessageListControls" runat="server">
            <asp:LinkButton runat="server" CssClass="btn theme-red" ID="lbShowPendingISent">
                    <input type="radio" name="options" id="option1" autocomplete="off" checked>
                    Awaiting Response
            </asp:LinkButton>

            <asp:LinkButton runat="server" CssClass="btn theme-white" ID="lbShowPendingRecieved">
                    <input type="radio" name="options" id="option4" autocomplete="off" checked>
                    Incoming Requests
            </asp:LinkButton>

            <asp:LinkButton runat="server" CssClass="btn theme-white" ID="lbShowDatestbScheduled">
                    <input type="radio" name="options" id="option2" autocomplete="off">
                    Accepted Dates
            </asp:LinkButton>


            <asp:LinkButton runat="server" CssClass="btn theme-white" ID="lbShowDates">
                    <input type="radio" name="options" id="option3" autocomplete="off">
                    Planned Dates
            </asp:LinkButton>
        </div>

    </div>
    <div runat="server" id="divpendingreqs" class="justify-content-center align-items-center w-75 my-5 mx-auto">
        <br />
        <div class="col-12">
            <div class="card-body p-0">
                <asp:Repeater runat="server" ID="rptPending">
                    <ItemTemplate>
                        <div class="card w-100 rounded">
                            <div class="card-body px-5 py-3">
                                <div class="d-flex flex-row justify-content-between">
                                    <!-- Name and time goes here-->
                                    <div>
                                        <asp:Label CssClass="font-weight-bold " runat="server" Text='<%#Eval("userName") %>'></asp:Label>
                                        | 
                                        <asp:LinkButton runat="server" CssClass="text-danger" ID="lbGotoProf" CommandName='<%#DataBinder.Eval(Container.DataItem, "userID") %>' OnCommand="lbGotoProf_Command"> Go to Profile</asp:LinkButton>
                                    </div>
                                    <asp:Label runat="server" Text='<%#Eval("time") %>' CssClass="float-right"></asp:Label>
                                </div>
                                <div class="text-truncate text-muted text-wrap">
                                    <asp:Label runat="server" Text='<%#Eval("message") %>'> </asp:Label>
                                </div>
                                <br />
                                <div>
                                    <asp:LinkButton runat="server" ID="lblDeleteReq" CommandName='<%#DataBinder.Eval(Container.DataItem, "userID") %>' OnCommand="lblDeleteReq_Command"> Delete Request</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
    <div runat="server" id="divReqsToApprove" class="justify-content-center align-items-center w-75 my-5 mx-auto">
        <br />
        <div class="col-12 w-100">
            <div class="card-body p-0">
                <asp:Repeater runat="server" ID="rptAcceptReqs">
                    <ItemTemplate>
                        <div class="card w-100 rounded">
                            <div class="card-body px-5 py-3">
                                <div class="d-flex flex-row justify-content-between">
                                    <!-- Name and time goes here-->
                                    <div>
                                        <asp:Label CssClass="font-weight-bold " runat="server" Text='<%#Eval("userName") %>'></asp:Label>
                                        | 
                                        <asp:LinkButton runat="server" CssClass="text-danger" ID="lbGotoProf" CommandName='<%#DataBinder.Eval(Container.DataItem, "userID") %>' OnCommand="lbGotoProf_Command"> Go to Profile</asp:LinkButton>
                                    </div>
                                    <asp:Label runat="server" Text='<%#Eval("time") %>' CssClass="float-right"></asp:Label>
                                </div>
                                <div class="text-truncate text-muted text-wrap">
                                    <asp:Label runat="server" Text='<%#Eval("message") %>'> </asp:Label>
                                </div>
                                <br />
                                <div class="d-flex justify-content-around">
                                    <asp:LinkButton runat="server" ID="lbAccept" CommandName='<%#DataBinder.Eval(Container.DataItem, "userID") %>' OnCommand="lbAccept_Command"> Accept Request</asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="lbDeny" CommandName='<%#DataBinder.Eval(Container.DataItem, "userID") %>' OnCommand="lbDeny_Command"> Deny Request</asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="lbIgnore" CommandName='<%#DataBinder.Eval(Container.DataItem, "userID") %>' OnCommand="lbIgnore_Command"> Ignore Request</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>

        <div runat="server" id="divScheduleDates" class="justify-content-center align-items-center w-75 my-5 mx-auto">
        <br />
        <div class="col-12">
            <div class="card-body p-0">
                <asp:Repeater runat="server" ID="rptSchedule">
                    <ItemTemplate>
                        <div class="card w-100 rounded">
                            <div class="card-body px-5 py-3">
                                <div class="d-flex flex-row justify-content-between">
                                    <!-- Name and time goes here-->
                                    <div>
                                        <asp:Label CssClass="font-weight-bold " runat="server" Text='<%#Eval("userName") %>'></asp:Label>
                                        | 
                                        <asp:LinkButton runat="server" CssClass="text-danger" ID="lbGotoProf" CommandName='<%#DataBinder.Eval(Container.DataItem, "userID") %>' OnCommand="lbGotoProf_Command"> Go to Profile</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="font-italic">
                                    STATUS: Accepted
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
            $("#navlinkDashboard").removeClass("active");
            $("#navlinkDates").addClass("active");

        });



    </script>
</asp:Content>
