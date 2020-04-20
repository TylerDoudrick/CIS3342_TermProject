<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Dates.aspx.cs" Inherits="TermProject.Dates" MaintainScrollPositionOnPostback="true" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">

    <style>
        .message {
            color: black;
        }

            .message:hover {
                background: rgb(250,250,250);
                cursor: pointer;
                text-decoration: none;
                color: black;
            }

            .message div:hover {
                background: rgb(250,250,250);
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
            <asp:LinkButton runat="server" CssClass="btn theme-red" ID="lbShowPendingISent" OnClick="lbShowPendingISent_Click">
                    <input type="radio" name="options" id="option1" autocomplete="off" checked>
                    Awaiting Response
            </asp:LinkButton>

            <asp:LinkButton runat="server" CssClass="btn theme-white" ID="lbShowPendingRecieved" OnClick="lbShowPendingRecieved_Click">
                    <input type="radio" name="options" id="option4" autocomplete="off" checked>
                    Incoming Requests
            </asp:LinkButton>

            <asp:LinkButton runat="server" CssClass="btn theme-white" ID="lbShowDatestbScheduled" OnClick="lbShowDatestbScheduled_Click">
                    <input type="radio" name="options" id="option2" autocomplete="off">
                    Accepted Dates
            </asp:LinkButton>


            <asp:LinkButton runat="server" CssClass="btn theme-white" ID="lbShowDates" OnClick="lbShowDates_Click">
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
                                        |
                                        
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

    <div id="divReqsToApprove" runat="server" visible="false">
        <div class="row">

            <div runat="server" id="divPending" class="justify-content-center align-items-center w-75 my-5 mx-auto">
                <br />
                <div class="col-12">
                    <div class="card-body p-0">
                        <h5 class="text-info">Pending Requests </h5>

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
        </div>

        <div class="row">
            <div runat="server" id="divIgnoredDates" class="justify-content-center align-items-center w-75 my-5 mx-auto ">
                <br />
                <div class="col-12 ">
                    <div class="card-body p-0">
                        <h5 class="text-info">Ignored Requests </h5>
                        <asp:Repeater runat="server" ID="rptIgnoredReqs">
                            <ItemTemplate>
                                <div class="card w-100 rounded">
                                    <div class="card-body px-5 py-3">
                                        <div class="d-flex flex-row justify-content-between">
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
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>

        </div>

    </div>


    <div class="modal " id="modalScheduleDate" tabindex="-1" role="dialog" aria-labelledby="modalScheduleDateLabel" aria-hidden="true">
        <div class=" modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header text-center">
                    <div class="modal-title ">
                        <h5 class="card-title">Set Up Date</h5>
                    </div>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>

                </div>

                <div class="modal-body">
                    <div class="form-group row justify-content-around">
                        <div class="col-4">
                            <asp:Label runat="server" CssClass="col-form-label my-1 d-flex align-items-end font-weight-bold" for="<%= lblNameDate.ClientID %>"> Date With: </asp:Label>
                        </div>
                        <div class="col-7">
                            <asp:Label runat="server" ID="lblNameDate" CssClass="text-info col-form-label my-1 d-flex align-items-end font-weight-bold" ></asp:Label>
                        </div>
                    </div>
                    <div class="form-group row justify-content-around">
                        <div class="col-4">
                            <asp:Label runat="server" CssClass="col-form-label my-1 d-flex align-items-end" for="<%= txtWhen.ClientID %>"> When</asp:Label>
                        </div>
                        <div class="col-7">
                            <asp:TextBox runat="server" ID="txtWhen" TextMode="DateTimeLocal" CssClass="form-control" placeholder="04/21/2020 3:00pm"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row form-group justify-content-around">
                        <div class="col-4">
                            <asp:Label runat="server" CssClass="col-form-label my-1 d-flex align-items-end" for="<%= txtLocation.ClientID %>"> Location </asp:Label>
                        </div>
                        <div class="col-7">
                            <asp:TextBox runat="server" ID="txtLocation" CssClass="form-control" placeholder="Location..."></asp:TextBox>
                        </div>
                    </div>
                    <div class="row form-group justify-content-around">
                        <div class="col-4">
                            <asp:Label runat="server" CssClass="col-form-label my-1 d-flex align-items-end" for="<%= txtDesc.ClientID %>"> Description</asp:Label>
                        </div>
                        <div class="col-7">
                            <asp:TextBox runat="server" ID="txtDesc" TextMode="MultiLine" CssClass="form-control" placeholder="Description..."></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <!--          <button type="button" runat="server" ID="cancel" Text="cancel"> Cancel</button>
                        <asp:Button runat="server" class="btn btn-secondary mr-2" Text="Cancel" OnClick="Unnamed_Click" /> -->
                    <asp:Button runat="server" ID="btnSave" type="button" class="btn btn-primary" Text="Save" OnClick="btnSave_Click" />

                </div>

            </div>
        </div>
    </div>

    <div runat="server" id="divScheduleDates" class="justify-content-center align-items-center w-75 my-5 mx-auto hidden">
        <br />
        <div class="col-12" runat="server">

            <div class="card-body p-0" runat="server">
                <asp:ListView runat="server" ID="lvSchedule">
                    <ItemTemplate>
                        <div class="card w-100 rounded" runat="server">
                            <div class="card-body px-5 py-3" runat="server">
                                <div class="d-flex flex-row justify-content-between" runat="server">
                                    <!-- Name and time goes here-->
                                    <div runat="server">
                                        <asp:Label CssClass="font-weight-bold " runat="server" Text='<%#Eval("userName") %>'></asp:Label>
                                        | 
                                        <asp:LinkButton runat="server" CssClass="text-danger" ID="lbGotoProf" CommandName='<%#DataBinder.Eval(Container.DataItem, "userID") %>' OnCommand="lbGotoProf_Command"> Go to Profile</asp:LinkButton>
                                    </div>
                                    <div class="font-italic" runat="server">
                                        STATUS: Accepted
                                    </div>
                                </div>

                                <div runat="server">
                                    <asp:LinkButton runat="server" ID="btnShowDate" CssClass="btn btn-primary" Text="Schedule Date" CommandName='<%#DataBinder.Eval(Container.DataItem, "userID") %>' OnCommand="btnShowDate_Command"></asp:LinkButton>
                                </div>
                                <button type="button" style="display: none;" id="btnShowPopup" class="btn btn-primary btn-lg">
                                </button>

                            </div>
                        </div>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </div>
        </div>

    </div>


    <div runat="server" id="divplanneddates" class="justify-content-center align-items-center w-75 my-5 mx-auto hidden">
        <br />
        <div class="col-12" runat="server">
            <div class="card-body p-0" runat="server">
                <asp:ListView runat="server" ID="lvPlannedDates" OnItemEditing="lvPlannedDates_ItemEditing" OnItemCanceling="lvPlannedDates_ItemCanceling" OnItemUpdating="lvPlannedDates_ItemUpdating">
                    <ItemTemplate>
                        <div class="card w-100 rounded" runat="server">
                            <div class="card-body px-5 py-3" runat="server">
                                <div class="d-flex flex-row justify-content-between" runat="server">
                                    <!-- Name and time goes here-->
                                    <div runat="server">
                                        <asp:Label CssClass="font-weight-bold " runat="server" Text='<%#Eval("userName") %>'></asp:Label>
                                        | 
                                        <asp:LinkButton runat="server" CssClass="text-danger" ID="lbGotoProf" CommandName='<%#DataBinder.Eval(Container.DataItem, "userID") %>' OnCommand="lbGotoProf_Command"> Go to Profile</asp:LinkButton>
                                    </div>
                                    <div class="font-italic" runat="server">
                                        STATUS: Planned Date
                                    </div>
                                </div>
                                <div runat="server" class="row my-2">
                                    <div class="col-1">
                                        <asp:Label runat="server" CssClass="col-form-label d-flex align-items-end" for="<%= txtWhenEdit.ClientID %>"> When</asp:Label>
                                    </div>
                                    <div class="col-3">
                                        <asp:Label runat="server" ReadOnly="true" Text='<%#Eval("dt") %>' CssClass="form-control"> </asp:Label>
                                    </div>
                                </div>
                                <div runat="server" class="row my-2">
                                    <div class="col-1">
                                        <asp:Label runat="server" CssClass="col-form-label d-flex align-items-end" for="<%= txtLocationEdit.ClientID %>"> Location </asp:Label>
                                    </div>
                                    <div class="col-4">
                                        <asp:Label runat="server" ReadOnly="true" Text='<%#Eval("location") %>' CssClass="form-control"> </asp:Label>

                                    </div>
                                </div>
                                <div runat="server" class="row my-2">
                                    <div class="col-1">
                                        <asp:Label runat="server" CssClass="col-form-label  d-flex align-items-end" for="<%= txtDescEdit.ClientID %>"> Description</asp:Label>
                                    </div>
                                    <div class="col-6">
                                        <asp:Label runat="server" ReadOnly="true" TextMode="MultiLine" Text='<%#Eval("description") %>' CssClass="form-control"> </asp:Label>

                                    </div>
                                    <div class="col-2">
                                        <asp:Label runat="server" ID="lblIDHidden" Text='<%#Eval("userID") %>' CssClass="d-none"></asp:Label>
                                        <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <div class="card w-100 rounded" runat="server">
                            <div class="card-body px-5 py-3" runat="server">
                                <div class="d-flex flex-row justify-content-between" runat="server">
                                    <!-- Name and time goes here-->
                                    <div runat="server">
                                        <asp:Label CssClass="font-weight-bold " ID="lblName" runat="server" Text='<%#Bind("userName") %>'></asp:Label>
                                        | 
                                        <asp:LinkButton runat="server" CssClass="text-danger" ID="lbGotoProf" CommandName='<%#DataBinder.Eval(Container.DataItem, "userID") %>' OnCommand="lbGotoProf_Command"> Go to Profile</asp:LinkButton>
                                    </div>
                                    <div class="font-italic" runat="server">
                                        STATUS: Planned Date
                                    </div>
                                </div>
                                <div runat="server" class="row my-2">
                                    <div class="col-1">
                                        <asp:Label runat="server" CssClass="col-form-label d-flex align-items-end" for="<%= txtWhenEdit.ClientID %>"> When</asp:Label>
                                    </div>
                                    <div class="col-3">
                                        <asp:TextBox runat="server" ReadOnly="false" ID="txtWhenEdit" Text='<%#Bind("dt") %>' CssClass="form-control"> </asp:TextBox>
                                    </div>
                                </div>
                                <div runat="server" class="row my-2">
                                    <div class="col-1">
                                        <asp:Label runat="server" CssClass="col-form-label d-flex align-items-end" for="<%= txtLocationEdit.ClientID %>"> Location </asp:Label>
                                    </div>
                                    <div class="col-4">
                                        <asp:TextBox runat="server" ReadOnly="false" ID="txtLocationEdit" Text='<%#Bind("location") %>' CssClass="form-control"> </asp:TextBox>
                                    </div>
                                </div>
                                <div runat="server" class="row my-2">
                                    <div class="col-1">
                                        <asp:Label runat="server" CssClass="col-form-label  d-flex align-items-end" for="<%= txtDescEdit.ClientID %>"> Description</asp:Label>
                                    </div>
                                    <div class="col-6">
                                        <asp:TextBox runat="server" ReadOnly="false" TextMode="MultiLine" ID="txtDescEdit" Text='<%#Eval("description") %>' CssClass="form-control"> </asp:TextBox>
                                    </div>
                                    <div class="col-2">
                                        <asp:LinkButton runat="server" ID="lbSaveChanges" CommandName='<%#DataBinder.Eval(Container.DataItem, "userID") %>'></asp:LinkButton>
                                    </div>
                                </div>
                                <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update" Text="Update" CssClass="btn btn-success" />
                                <asp:LinkButton ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" CssClass="btn btn-secondary" />
                            </div>
                        </div>
                    </EditItemTemplate>
                </asp:ListView>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="EndBodyPlaceHolder" runat="server">
    <script>
        $(document).ready(function () {
            $("#navlinkDashboard").removeClass("active");
            $("#navlinkDates").addClass("active");

            $.ajax({
                    url: "https://localhost:44375/api/datingservice/notifications/dismiss/dates/<%= Session["UserID"]%>",
                    type: 'delete',
                    beforeSend: function (request) {
                        request.setRequestHeader("Authorization", "Bearer <%= Session["token"]%>");
                    },
                    contentType: 'application/json',
                    error: function (xhr) {
                        console.log(xhr.responseText);
                    },
                    success: function (data) {
                        console.log(data);
                        if (data.result == "success") {
                            $('.close-toastr').closest('.toast').remove();
                        }
                    }
                });


            function ShowPopup() {
                //$("#btnShowPopup").click();
                // $("modalScheduleDate").modal("show");
            }


        });
    </script>
</asp:Content>
