function pay() {
    let sessionId = getCookie("sessionId");

    let paymentObject = {
        Card: {
            CardNumber: document.getElementById("card-number").value,
            Date: document.getElementById("card-validity-period").value,
            Cvv: document.getElementById("card-cvv").value
        },
        SessionId: sessionId
    };

    var xmlHttp = new XMLHttpRequest();

    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {

            document.open();
            document.write(xmlHttp.responseText);
            document.close();
        }
    }

    xmlHttp.open("post", window.location.origin + "/Purchase/Pay");

    xmlHttp.setRequestHeader("Content-type", "application/json");

    xmlHttp.send(JSON.stringify(paymentObject));
}
