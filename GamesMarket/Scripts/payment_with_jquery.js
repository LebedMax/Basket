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

    $.ajax({
        method: "POST",
        url: "Pay",
        data: paymentObject
    }).done(function(response) {
        document.open();
        document.write(response);
        document.close();
    });
}
