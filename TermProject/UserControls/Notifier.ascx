<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Notifier.ascx.cs" Inherits="TermProject.UserControls.Notifier" %>
<script>
    //
    //Usercontrol to hander notifications
    //
    //
    var w;

    function startWorker() {
        //Check if browser supports workers
        if (typeof (Worker) !== "undefined") {
            //Start the worker if it hasn't been started
            if (typeof (w) == "undefined") {
                w = new Worker("UserControls/js/notifierWorker.js?userID=<%= this.UserID %>&token=<%= this.token %>");
            }
            w.onmessage = function (event) {


                //When the worker tells us about a message, shoat a toast using the toastr plugin
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

                                //
                                //When a notification is clicked, contact the API to dismiss that notification
                                //

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
                        //Show different toast title depending on notification type
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
