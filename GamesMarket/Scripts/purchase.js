function purchase() {
    let postObject = {
        Name: document.getElementById("client-name").value,
        Address: document.getElementById("client-address").value,
        Surname: document.getElementById("client-surname").value,
        Phone: document.getElementById("client-phone").value,
        Email: document.getElementById("client-email").value,
        SessionId: getCookie("sessionId")
    };

    
    var xmlHttp = new XMLHttpRequest();

    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
            
            document.open();
            document.write(xmlHttp.responseText);
            document.close();
        }
    }

    xmlHttp.open("post", window.location.origin + "/Purchase/MakePurchase");

    xmlHttp.setRequestHeader("Content-type", "application/json");

    xmlHttp.send(JSON.stringify(postObject));
}
