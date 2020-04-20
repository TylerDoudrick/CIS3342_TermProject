<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Notifier.ascx.cs" Inherits="TermProject.UserControls.Notifier" %>
<script>
    var w;

    function startWorker() {
        if (typeof (Worker) !== "undefined") {
            if (typeof (w) == "undefined") {
                w = new Worker("UserControls/js/notifierWorker.js?userID=<%= this.UserID %>&token=<%= this.token %>");
            }
            w.onmessage = function (event) {

                if (event.data.length !== 0) {
                    event.data.forEach(function (obj) {
                        var notificationID = obj.notificationID;
                        toastr.options = {
                            "closeButton": true,
                            "debug": false,
                            "newestOnTop": false,
                            "progressBar": false,
                            "positionClass": "toast-bottom-right",
                            "preventDuplicates": true,
                            "onclick": null,
                            "showDuration": "300",
                            "hideDuration": "1000",
                            "timeOut": "0",
                            "extendedTimeOut": "0",
                            "showEasing": "swing",
                            "hideEasing": "linear",
                            "showMethod": "fadeIn",
                            "hideMethod": "fadeOut",
                            "onclick": function () {
                                var notification = {
                                    "userID": "<%= Session["UserID"].ToString()%>",
                                    "notificationID": notificationID
                                }

                                $.ajax({
                                    url: "https://localhost:44375/api/datingservice/notifications/dismiss",
                                    type: 'delete',
                                    beforeSend: function (request) {
                                        request.setRequestHeader("Authorization", "Bearer <%= Session["token"]%>");
                                    },
                                    contentType: 'application/json',
                                    dataType: "json",
                                    data: JSON.stringify(notification),
                                    error: function (xhr) {
                                        console.log(xhr.responseText);
                                    },
                                    success: function (data) {
                                        console.log(data);
                                    }
                                });

                            }
                        }
                        if (obj.notificationType == 1) {

                            toastr["info"](obj.notificationMessage, "Date Information!")

                        } else if (obj.notificationType == 2) {
                            toastr["info"](obj.notificationMessage, "New Message!")
                        }
                    });

                }
            };
        }
    }

    function stopWorker() {
        w.terminate();
        w = undefined;
    }

    startWorker();



</script>
