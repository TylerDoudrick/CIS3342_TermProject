
function checkNotifications() {


        var xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                postMessage(this.responseText);
                setTimeout("checkNotifications()", 10000);
            }
        };
    xhttp.open("GET", "https://localhost:44375/api/datingservice/notifications/123", true);
        xhttp.send();
    
}

checkNotifications(); 