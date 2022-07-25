function creditCardValidation() {
    var creditCardNumber = document.getElementById("card-number");
    var creditCardValidityPerion = document.getElementById("card-validity-period");
    var creditCardCvv = document.getElementById("card-cvv");

    if (!creditCardNumber.value)
    {
        creditCardNumber.style.border = "2px solid red";
        return false;
    }
    if (!creditCardValidityPerion.value) {
        creditCardValidityPerion.style.border = "2px solid red";
        return false;
    }
    if (!creditCardCvv.value) {
        creditCardCvv.style.border = "2px solid red";
        return false;
    }
    return pay();
}

function purchaseFormValidation() {
    var clientName = document.getElementById("client-name");
    var clientSurname = document.getElementById("client-surname");
    var clientAdress = document.getElementById("client-address");
    var clientPhone = document.getElementById("client-phone");
    var clientEmail = document.getElementById("client-email");

    if (!clientName.value) {
        clientName.style.border = "2px solid red";
        return false;
    }
    if (!clientSurname.value) {
        clientSurname.style.border = "2px solid red";
        return false;
    }
    if (!clientAdress.value) {
        clientAdress.style.border = "2px solid red";
        return false;
    }
    if (!clientPhone.value) {
        clientPhone.style.border = "2px solid red";
        return false;
    }
    if (!clientEmail.value) {
        clientEmail.style.border = "2px solid red";
        return false;
    }

}
