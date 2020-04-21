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

        .messageRow:hover {
            background-color: #f8f8ff;
            cursor: pointer;
        }

        .unread {
            border: 1px solid red;
            border-radius: 7px;
            box-shadow: 0 0 5px red;
        }
    </style>
    <link href="styles/jquery-ui.min.css" rel="stylesheet" />
    <script src="js/jquery-ui.min.js"></script>
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
                    <%--                    <input type="text" id="jsAutocomplete" class="form-control" />--%>

                    <div class="form-group">
                        <label for="<%= ddlRecipient.ClientID %>">Recipient</label>
                        <asp:DropDownList ID="ddlRecipient" CssClass="form-control selectpicker" runat="server" data-live-search="true">
                        </asp:DropDownList>
                    </div>
                    <div class="form-group hidden" id="divProfile">
                        <div class="row">

                            <div class="card mb-3 mx-auto" style="max-width: 465px;">
                                <div class="row">
                                    <div class="col-6">
                                        <img id="imgRecipient" class="card-img" src="" />
                                    </div>
                                    <div class="col-6 d-flex justify-content-center align-items-center">
                                        <div class="card-body p-0 d-flex align-items-center flex-column justify-content-center">
                                            <h5 class="card-title" id="lblRecipientName"></h5>
                                            <p class="card-text" id="lblRecipientLocation">
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>

                    </div>
                    <div class="form-group">
                        <label for="txtMessage">Message</label>
                        <textarea type="text" class="form-control" id="txtMessage" placeholder="Message..."></textarea>
                    </div>


                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-secondary" id="btnSendMessage">Send Message</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal -->
    <div class="modal fade" id="modalReply" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalReplyLabel">Send Reply</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">

                    <div class="form-group">
                        <label>Recipient</label>
                        <h5 id="lblReplyTo"></h5>
                    </div>

                    <div class="form-group">
                        <label for="txtMessage">Message</label>
                        <textarea class="form-control" id="txtReply" placeholder="Reply..."></textarea>
                    </div>


                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <button type="button" class="btn theme-red" id="btnSendReply">Send Repy</button>
                </div>
            </div>
        </div>
    </div>




    <div class="row justify-content-start align-items-center mt-5 mb-3 w-75 mx-auto">
        <div class="col">
            <div class="btn-group btn-group-toggle" data-toggle="buttons" id="divInboxListControls">
                <label class="btn theme-red" id="lblShowInbox">
                    <input type="radio" name="options" id="btnShowInbox" autocomplete="off" checked>
                    Inbox
                </label>
                <label class="btn theme-white" id="lblShowOutbox">
                    <input type="radio" name="options" id="btnShowOutbox" autocomplete="off">
                    Outbox
                </label>
            </div>
            <div class="hidden" id="divMessageViewControls">
                <button type="button" class="btn btn-secondary" id="btnReturnToInbox">Return to Inbox</button>
            </div>
            <div class="hidden" id="divOutgoingViewControls">
                <button type="button" class="btn btn-secondary" id="btnReturnToOutbox">Return to Outbox</button>
            </div>
        </div>
        <div class="col text-right">
            <button type="button" class="btn theme-red" data-toggle="modal" data-target="#modalSendMessage">Send Message</button>
        </div>

    </div>
    <div class="row justify-content-center align-items-center w-75 mx-auto">
        <div class="col">
            <div class="card" id="divInboxList">
                <h5 class="card-header">Inbox</h5>
                <div class="card-body p-0" id="divInbox">
                </div>
            </div>
            <div class="card hidden" id="divOutboxList">
                <h5 class="card-header">Outbox</h5>
                <div class="card-body p-0" id="divOutbox">
                </div>
            </div>
            <div class="card hidden" id="divViewMessage">
                <h5 class="card-header">View Message</h5>
                <div class="card-body">
                    <div class="row">
                        <div class="col-3">
                            <div class="card profilePhoto" style="width: 18rem;">
                                <img class="card-img-top" id="imgSender" src="" />
                                <div class="card- pt-3">
                                    <h2 class="card-title text-center p-0">
                                        <span id="lblSender"></span>

                                    </h2>

                                </div>
                            </div>


                        </div>
                        <div class="col pt-2">
                            <div class="row text-right justify-content-end px-2">
                                <h5><span id="lblSentDate" class="font-weight-bold"></span></h5>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <div class="row">
                                        <h5>Message:</h5>
                                    </div>
                                    <div class="row h-100 w-75" style="min-height: 10em">
                                        <div id="lblMessageBody" class="well form-control h-100" readonly></div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="row px-3">
                    </div>


                    <hr />
                    <div class="text-right">
                        <button type="button" class="btn btn-secondary" data-toggle="modal" id="btnReply" data-target="#modalReply">Reply</button>
                        <button type="button" class="btn theme-red">Reply with Date Request</button>
                    </div>
                </div>

            </div>
            <div class="card hidden" id="divViewOutgoing">
                <h5 class="card-header">View Sent Message</h5>
                <div class="card-body">
                    <div class="row">
                        <div class="col-3">
                            <div class="card profilePhoto" style="width: 18rem;">
                                <img class="card-img-top" id="imgReceiver" src="" />
                                <div class="card-body pt-3">
                                    <h2 class="card-title text-center p-0">
                                        <span id="lblReceiver"></span>

                                    </h2>

                                </div>
                            </div>


                        </div>
                        <div class="col pt-2">
                            <div class="row text-right justify-content-end px-2">
                                <h5 class="font-weight-bold">Sent On: <span id="lblReceivedDate" class="font-weight-bold"></span></h5>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <div class="row">
                                        <h5>Message:</h5>
                                    </div>
                                    <div class="row h-100 w-75" style="min-height: 10em">
                                        <div id="lblReceivedMessage" class="well form-control h-100" readonly></div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="row px-3">
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="EndBodyPlaceHolder" runat="server">
    <script>
        $(document).ready(function () {
            $("#navlinkDashboard").removeClass("active");
            $("#navlinkMessages").addClass("active");

            var user = {
                "userID": "<%= Session["userID"].ToString()%>"

            }

            var selected = {};

            function grabInbox() {
                $("#divInbox").empty();
                $.ajax({
                    url: "https://localhost:44375/api/datingservice/interactions/GetUserInbox",
                    type: 'post',
                    beforeSend: function (request) {
                        request.setRequestHeader("Authorization", "Bearer <%= Session["token"]%>");
                    },
                    contentType: 'application/json',
                    dataType: "json",
                    data: JSON.stringify(user),
                    error: function (xhr) {
                        console.log(xhr.responseText);
                    },
                    success: function (data) {
                        data.forEach(function (obj) {
                            var container = document.createElement("div");
                            var message = ``;
                            if (obj.readreceipt == "") {
                                container.className = "unread"
                            } else {
                            }
                            message += ` 
<div class="card w-100 rounded-0 messageRow">
                                <div class="row p-2">
                                    <div class="col-1">
                                        <img id="imgRecipient" class="card-img rounded-lg" src="`+ obj.senderimage + `" />
                                    </div>
                                    <div class="col-11 d-flex justify-content-center align-items-center w-100">
                                        <div class="d-flex align-items-around flex-column w-100">
                            <div class="d-flex flex-row justify-content-between">
                                <h6 class="font-weight-bold">`+ obj.sendername + `</h6>
                                <h6 class="text-muted">`+ obj.timestamp + `</h6>
                            </div>
                            <div class="text-truncate text-muted">`+ obj.message + `</div>
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>`

                            container.innerHTML = message;
                            container.onclick = function () {
                                $("#divInboxList").hide();
                                $("#divInboxListControls").hide();
                                $("#divMessageViewControls").removeClass("hidden");
                                $("#divViewMessage").removeClass("hidden");
                                $("#lblSender").text(obj.sendername);
                                $("#lblSentDate").text(obj.timestamp);
                                $("#lblMessageBody").text(obj.message);
                                $("#imgSender").attr("src", obj.senderimage);
                                var messageinfo = {
                                    "id": obj.messageid
                                }
                                if (obj.readreceipt == "") {
                                    $.ajax({
                                        url: "https://localhost:44375/api/datingservice/interactions/UpdateReadReceipt",
                                        type: 'put',
                                        beforeSend: function (request) {
                                            request.setRequestHeader("Authorization", "Bearer <%= Session["token"]%>");
                                        },
                                        contentType: 'application/json',
                                        dataType: "json",
                                        data: JSON.stringify(messageinfo),
                                        error: function (xhr) {
                                            console.log(xhr.responseText);
                                        },
                                        success: function (data) {
                                            if (data.result == "success") {
                                                container.className = "";
                                            }
                                        }
                                    });
                                }
                                //Reply Modal Stuff

                                $("#lblReplyTo").text(obj.sendername);

                                selected = obj;
                            };
                            $("#divInbox").append(container);
                        });

                    }
                });

                $.ajax({
                    url: "https://localhost:44375/api/datingservice/notifications/dismiss/messages/<%= Session["UserID"]%>",
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
            }
            function grabOutbox() {
                $("#divOutbox").empty();
                $.ajax({
                    url: "https://localhost:44375/api/datingservice/interactions/GetUserOutbox",
                    type: 'post',
                    beforeSend: function (request) {
                        request.setRequestHeader("Authorization", "Bearer <%= Session["token"]%>");
                    },
                    contentType: 'application/json',
                    dataType: "json",
                    data: JSON.stringify(user),
                    error: function (xhr) {
                        console.log(xhr.responseText);
                    },
                    success: function (data) {
                        data.forEach(function (obj) {
                            var container = document.createElement("div");
                            var message = ` 
                            <div class="card w-100 rounded-0 messageRow">
                                <div class="row p-2">
                                    <div class="col-1">
                                        <img id="imgRecipient" class="card-img rounded-lg" src="`+ obj.receiverimage + `" />
                                    </div>
                                    <div class="col-11 d-flex justify-content-center align-items-center w-100">
                                        <div class="d-flex align-items-around flex-column w-100">
                            <div class="d-flex flex-row justify-content-between">
                                <h6 class="font-weight-bold">`+ obj.receivername + `</h6>
                                <h6 class="text-muted">Sent On: `+ obj.timestamp + `</h6>
                            </div>
                            <div class="text-truncate text-muted">`+ obj.message + `</div>`
                            if (obj.readreceipt == "") {
                                message += `<div class="text-truncate text-muted text-right">` + obj.readreceipt + `</div>
`
                            } else {
                                message += `<div class="text-truncate text-muted text-right">Opened On: ` + obj.readreceipt + `</div>
`
                            }

                            message += `</p>
                                        </div>
                                    </div>
                                </div>
                            </div>`

                            container.innerHTML = message;
                            container.onclick = function () {
                                $("#divOutboxList").hide();
                                $("#divInboxListControls").hide();
                                $("#divOutgoingViewControls").removeClass("hidden");
                                $("#divViewOutgoing").removeClass("hidden");
                                $("#lblReceiver").text(obj.receivername);
                                $("#lblReceivedDate").text(obj.timestamp);
                                $("#lblReceivedMessage").text(obj.message);
                                $("#imgReceiver").attr("src", obj.receiverimage);


                            };
                            $("#divOutbox").append(container);
                        });

                    }
                });
            }
            grabInbox();
            $("#btnShowOutbox").click(function () {
                $("#lblShowOutbox").addClass('theme-red');
                $("#lblShowOutbox").removeClass('theme-white');
                $("#lblShowInbox").addClass('theme-white');
                $("#lblShowInbox").removeClass('theme-red');
                $("#divOutboxList").removeClass('hidden');
                $("#divInboxList").addClass('hidden');
                grabOutbox();
            });

            $("#btnShowInbox").click(function () {
                $("#lblShowOutbox").removeClass('theme-red');
                $("#lblShowOutbox").addClass('theme-white');
                $("#lblShowInbox").removeClass('theme-white');
                $("#lblShowInbox").addClass('theme-red');
                $("#divOutboxList").addClass('hidden');
                $("#divInboxList").removeClass('hidden');
                grabInbox();
            });

            $("#btnReturnToInbox").click(function () {
                $("#divInboxList").show();
                $("#divInboxListControls").show();
                $("#divMessageViewControls").addClass("hidden");
                $("#divViewMessage").addClass("hidden");
                $("#lblSender").text("");
                $("#lblSentDate").text("");
                $("#lblMessageBody").text("");
                $("#imgSender").attr("src", "");
            });

            $("#btnReturnToOutbox").click(function () {
                $("#divOutboxList").show();
                $("#divInboxListControls").show();
                $("#divOutgoingViewControls").addClass("hidden");
                $("#divViewOutgoing").addClass("hidden");
                $("#lblReceiver").text("");
                $("#lblReceivedDate").text("");
                $("#lblReceivedMessage").text("");
                $("#imgReceiver").attr("src", "");
            });


            $("#<%= ddlRecipient.ClientID%>").change(function () {
                var data = {
                    "id": $("#<%= ddlRecipient.ClientID%>").selectpicker('val')
                }
                $.ajax({
                    url: "https://localhost:44375/api/datingservice/interactions/ProfileSnippet",
                    type: 'post',
                    beforeSend: function (request) {
                        request.setRequestHeader("Authorization", "Bearer <%= Session["token"]%>");
                    },
                    contentType: 'application/json',
                    dataType: "json",
                    data: JSON.stringify(data),
                    error: function (xhr) {
                        console.log(xhr.responseText);
                    },
                    success: function (data) {
                        $("#imgRecipient").attr("src", data.image);
                        $("#lblRecipientName").text(data.name);
                        $("#lblRecipientLocation").text(data.location);
                        $("#divProfile").removeClass("hidden");
                        $("#btnSendMessage").removeClass('btn-secondary');
                        $("#btnSendMessage").addClass('theme-red');
                    }
                });
            });
            $("#btnSendMessage").click(function () {
                if ($("#<%= ddlRecipient.ClientID%>").selectpicker('val') == "" || $("#txtMessage").val() == "") {

                } else {
                    var data = {
                        "recipientid": $("#<%= ddlRecipient.ClientID%>").selectpicker('val'),
                        "senderid": "<%= Session["UserID"].ToString()%>",
                        "message": $("#txtMessage").val()
                    }
                    $.ajax({
                        url: "https://localhost:44375/api/datingservice/interactions/SendMessage",
                        type: 'put',
                        beforeSend: function (request) {
                            request.setRequestHeader("Authorization", "Bearer <%= Session["token"]%>");
                        },
                        contentType: 'application/json',
                        dataType: "json",
                        data: JSON.stringify(data),
                        error: function (xhr) {
                            console.log(xhr.responseText);
                        },
                        success: function (data) {
                            if (data.result == "success") {
                                $('#modalSendMessage').modal('hide');
                                showToast("success", "Success!", "Message sent to " + $("#lblRecipientName").text().split(' ')[0] + "!");
                                $("#imgRecipient").attr("src", "");
                                $("#lblRecipientName").text("");
                                $("#lblRecipientLocation").text("");
                                $("#divProfile").addClass("hidden");
                                $("#btnSendMessage").addClass('btn-secondary');
                                $("#btnSendMessage").removeClass('theme-red');
                                $("#<%= ddlRecipient.ClientID%>").selectpicker('val', '');
                                $("#txtMessage").val('');
                            }
                        }
                    });
                }
            });

            $("#btnSendReply").click(function () {
                if ($("#txtReply").val() == "") {

                } else {
                    var data = {
                        "recipientid": selected.senderid,
                        "senderid": "<%= Session["UserID"].ToString()%>",
                        "message": $("#txtReply").val()
                    }
                    $.ajax({
                        url: "https://localhost:44375/api/datingservice/interactions/SendMessage",
                        type: 'put',
                        beforeSend: function (request) {
                            request.setRequestHeader("Authorization", "Bearer <%= Session["token"]%>");
                        },
                        contentType: 'application/json',
                        dataType: "json",
                        data: JSON.stringify(data),
                        error: function (xhr) {
                            console.log(xhr.responseText);
                        },
                        success: function (data) {
                            if (data.result == "success") {
                                $('#modalReply').modal('hide');
                                showToast("success", "Success!", "Message sent to " + $("#lblReplyTo").text().split(' ')[0] + "!");
                                $("#lblReplyTo").text("");
                                $("#txtReply").val('');
                            }
                        }
                    });
                }
            });

          <%--  $('#modalSendMessage').on('shown.bs.modal', function () {

                $("#jsAutocomplete").autocomplete({
                    source: function (request, response) {
                        // Fetch data
                        $.ajax({
                            url: "https://localhost:44375/api/datingservice/interactions/getMessageRecipients",
                            type: 'post',
                            dataType: "json",
                            data: {
                                userid: <%= Session["UserID"].ToString()%>
                            },
                            success: function (data) {
                                var options = [];
                                data.forEach(function (obj) { options.push(obj.name); });
                                console.log($.map(data, function (item) {
                                    return {
                                        value: item.name,
                                        image: item.image,
                                        location: item.location,
                                        selectedId: item.userID
                                    };
                                }));
                                response(options);
                            }
                        });
                    }, select: function (event, ui) {
                        // Set selection
                        $('#jsAutocomplete').val(ui.item.name); // display the selected text
                        // $('#selectuser_id').val(ui.item.value); // save selected id to input
                        return false;
                    }
                });
                //}).autocomplete("instance")._renderItem = function (ul, item) {
                //    return $("<li><div><img src=><span>" + item.value + "</span></div></li>").appendTo(ul);
                //};
            })--%>


        });
    </script>
</asp:Content>
