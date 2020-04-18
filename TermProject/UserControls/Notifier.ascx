<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Notifier.ascx.cs" Inherits="TermProject.UserControls.Notifier" %>
<script>
    var w;

    function startWorker() {
        if (typeof (Worker) !== "undefined") {
            if (typeof (w) == "undefined") {
                w = new Worker("UserControls/js/notifierWorker.js?userID=<%= this.UserID %>");
            }
            w.onmessage = function (event) {

                if (event.data.length !== 0) {
                    event.data.forEach(function (obj) {
                        var id = obj.notificationID;
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
                                alert("This will dismiss notification with id: " + id)
                            }
                        }
                        if (obj.notificationType == 1) {

                            toastr["info"](obj.notificationMessage, "New Date Request!")

                        } else if (obj.notificationType == 2) {
                            toastr["info"](obj.notificationMessage, "New Message!")
                        }
                    });


                    function toastClick(notificationID) {
                        alert("Clicked Toast!");
                    }
                }
            };
        } else {
            document.getElementById("result").innerHTML = "Sorry, your browser does not support Web Workers...";
        }
    }

    function stopWorker() {
        w.terminate();
        w = undefined;
    }

    startWorker();



</script>
