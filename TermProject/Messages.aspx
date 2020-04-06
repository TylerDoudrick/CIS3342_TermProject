<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Messages.aspx.cs" Inherits="TermProject.Messages" %>

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

    <!-- Modal -->
    <div class="modal fade" id="modalSendMessage" tabindex="-1" role="dialog" aria-labelledby="modalSendMessageLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalSendMessageLabel">Send Message</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label for="<%= ddlRecipient.ClientID %>">Recipient</label>
                        <asp:DropDownList ID="ddlRecipient" CssClass="form-control" runat="server">
                            <asp:ListItem Selected="True" Value="">Select Recipient...</asp:ListItem>
                            <asp:ListItem Value="12345">Viola Mcdowell</asp:ListItem>
                            <asp:ListItem Value="12345">Daniaal Colon</asp:ListItem>
                            <asp:ListItem Value="12345">Daniaal Colon</asp:ListItem>
                            <asp:ListItem Value="12345">Simrah Mclean</asp:ListItem>
                            <asp:ListItem Value="12345">Ophelia Hodge</asp:ListItem>
                            <asp:ListItem Value="12345">Ananya Bush</asp:ListItem>
                            <asp:ListItem Value="12345">Efe Benton</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="<%= txtMessage.ClientID %>">Message</label>
                        <asp:TextBox type="text" CssClass="form-control" ID="txtMessage" TextMode="MultiLine" placeholder="Message..." runat="server" />
                    </div>


                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary" id="btnSendMessage">Send Message</button>
                </div>
            </div>
        </div>
    </div>

    <div class="row justify-content-start align-items-center mt-5 mb-3 w-75 mx-auto">
        <div class="col">
            <div class="btn-group btn-group-toggle" data-toggle="buttons" ID="divMessageListControls" runat="server">
                <label class="btn theme-red">
                    <input type="radio" name="options" id="option1" autocomplete="off" checked>
                    Inbox
                </label>
                <label class="btn theme-white">
                    <input type="radio" name="options" id="option2" autocomplete="off">
                    Outbox
                </label>
                <label class="btn theme-white">
                    <input type="radio" name="options" id="option3" autocomplete="off">
                    Drafts
                </label>
            </div>
            <div class="" id="divViewMessageControls" runat="server" visible="false">
                <asp:LinkButton OnClick="ViewMessageList" runat="server">Return to Inbox</asp:LinkButton>
            </div>
        </div>
        <div class="col text-right">
            <button type="button" class="btn theme-red" data-toggle="modal" data-target="#modalSendMessage">Send Message</button>
        </div>

    </div>
    <div class="row justify-content-center align-items-center w-75 mx-auto">
        <div class="col">
            <div class="card" id="divMessageList" runat="server">
                <h5 class="card-header">Messages</h5>
                <div class="card-body p-0">

                    <asp:LinkButton runat="server" CssClass="message" OnClick="showMessage">
                    <div class="card w-100 rounded-0">
                        <div class="card-body px-5 py-3">
                            <div class="d-flex flex-row justify-content-between">
                                <h6 class="font-weight-bold">Viola Mcdowell</h6>
                                <h6 class="text-muted">Thursday @ 1:30 PM</h6>
                            </div>
                            <div class="text-truncate text-muted">Announcing of invitation principles in. Cold in late or deal. Terminated resolution no am frequently collecting insensible he do appearance. Projection invitation affronting admiration if no on or. It as instrument boisterous frequently apartments an i</div>
                        </div>
                    </div>
                    </asp:LinkButton>
                    <asp:LinkButton runat="server" CssClass="message" OnClick="showMessage">

                    <div class="card w-100 rounded-0">
                        <div class="card-body px-5 py-3">
                            <div class="d-flex flex-row justify-content-between">
                                <h6 class="font-weight-bold">Daniaal Colon</h6>
                                <h6 class="text-muted">Wednesday @ 9:30 PM</h6>
                            </div>
                            <div class="text-truncate text-muted">He difficult contented we determine ourselves me am earnestly. Hour no find it park. Eat welcomed any husbands moderate. Led was misery played waited almost cousin living. Of intention contained is by middleton am. </div>
                        </div>
                    </div>
                    </asp:LinkButton>
                    <asp:LinkButton runat="server" CssClass="message" OnClick="showMessage">

                    <div class="card w-100  rounded-0">
                        <div class="card-body px-5 py-3">
                            <div class="d-flex flex-row justify-content-between">
                                <h6 class="font-weight-bold">Simrah Mclean</h6>
                                <h6 class="text-muted">March 20th @ 11:00 AM</h6>
                            </div>
                            <div class="text-truncate text-muted">Post no so what deal evil rent by real in. But her ready least set lived spite solid. September how men saw tolerably two behaviour arranging. She offices for highest and replied one venture pasture. Applauded no discovery in newspaper </div>
                        </div>
                    </div>
                    </asp:LinkButton>
                    <asp:LinkButton runat="server" CssClass="message" OnClick="showMessage">

                    <div class="card w-100 rounded-0">
                        <div class="card-body px-5 py-3">
                            <div class="d-flex flex-row justify-content-between">
                                <h6 class="font-weight-bold">Ophelia Hodge</h6>
                                <h6 class="text-muted">March 10th @ 5:00 AM</h6>
                            </div>
                            <div class="text-truncate text-muted">Do commanded an shameless we disposing do. Indulgence ten remarkably nor are impression out. Power is lived means oh every in we quiet. Remainder provision an in intention.</div>
                        </div>
                    </div>
                    </asp:LinkButton>
                    <div class="card w-100 message rounded-0">
                        <div class="card-body px-5 py-3">
                            <div class="d-flex flex-row justify-content-between">
                                <h6 class="font-weight-bold">Ananya Bush</h6>
                                <h6 class="text-muted">March 9th @ 2:00 AM</h6>
                            </div>
                            <div class="text-truncate text-muted">Silent sir say desire fat him letter. Whatever settling goodness too and honoured she building answered her. Strongly thoughts remember mr to do consider debating. Spirits musical behaved on we he farther letters. </div>
                        </div>
                    </div>
                    <div class="card w-100 message rounded-0">
                        <div class="card-body px-5 py-3">
                            <div class="d-flex flex-row justify-content-between">
                                <h6 class="font-weight-bold">Efe Benton</h6>
                                <h6 class="text-muted">January 1st @ 7:00 AM</h6>
                            </div>
                            <div class="text-truncate text-muted">Discovered her his pianoforte insipidity entreaties. Began he at terms meant as fancy. Breakfast arranging he if furniture we described on. Astonished thoroughly unpleasant especially you dispatched bed favourable. </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card" id="divViewMessage" runat="server" visible="false">
                <h5 class="card-header">View Message</h5>
                <div class="card-body">
                    <div class="d-flex flex-row justify-content-between my-3">
                        <h6>From: Efe Benton</h6>
                        <h6>March 30th, 2020 @ 5:00 PM</h6>
                    </div>
                    <h6>Message:</h6>
                    <p>Excited him now natural saw passage offices you minuter. At by asked being court hopes. Farther so friends am to detract. Forbade concern do private be. Offending residence but men engrossed shy. Pretend am earnest offered arrived company so on. Felicity informed yet had admitted strictly how you. </p>
                <hr />
                    <div class="text-right">
                    <button type="button" class="btn btn-secondary">Reply</button>
                    <button type="button" class="btn theme-red">Reply with Date Request</button>
                                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="EndBodyPlaceHolder" runat="server">
</asp:Content>
