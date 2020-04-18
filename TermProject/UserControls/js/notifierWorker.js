
function checkNotifications() {


        var xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                postMessage(JSON.parse(this.responseText));
                setTimeout("checkNotifications()", 10000);
            }
        };
    xhttp.open("GET", "https://localhost:44375/api/datingservice/notifications/" + location.search.split('=')[1], true);
        xhttp.send();
    
}

checkNotifications(); 