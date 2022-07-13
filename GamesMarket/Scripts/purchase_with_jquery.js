function purchase() {
    let postObject = {
        Name: document.getElementById("client-name").value,
        Address: document.getElementById("client-address").value,
        Surname: document.getElementById("client-surname").value,
        Phone: document.getElementById("client-phone").value,
        Email: document.getElementById("client-email").value,
        SessionId: getCookie("sessionId")
    };
debugger;
    $.ajax({
        method: "POST",
        url: "MakePurchase",
        data: postObject
    }).done(function(response) {
        document.open();
        document.write(response);
        document.close();
    });
}
