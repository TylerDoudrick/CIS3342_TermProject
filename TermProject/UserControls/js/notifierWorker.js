//
//js Worker for checking notifications
//
//Gets the userid and the token from the usercontrol
//
//Checks for notifications every 10 seconds
//
function checkNotifications() {
    var split = location.search.split('&');
    var userid = split[0].split('=')[1];
    var token = split[1].split('=')[1];
        var xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                postMessage(JSON.parse(this.responseText));
                setTimeout("checkNotifications()", 10000);
            }
        };
    xhttp.open("GET", "https://localhost:44375/api/datingservice/notifications/" + userid, true);
    xhttp.setRequestHeader('Authorization', 'Bearer ' + token);
        xhttp.send();
    
}

checkNotifications(); 