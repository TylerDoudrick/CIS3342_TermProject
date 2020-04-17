<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Notifier.ascx.cs" Inherits="TermProject.UserControls.Notifier" %>
<script>
    var w;

    function startWorker() {
        if (typeof (Worker) !== "undefined") {
            if (typeof (w) == "undefined") {
                w = new Worker("UserControls/js/notifierWorker.js");
            }
            w.onmessage = function (event) {
                if (event.data.length !== 0) {

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
                    "onclick": toastClick
                }
                toastr["info"]("You have new messages!", event.data)

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
