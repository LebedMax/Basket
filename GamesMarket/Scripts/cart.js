var basketLoading = false;

window.addEventListener("load", function (event) {
    let sessionId = getCookie('sessionId');

    var xmlHttp = new XMLHttpRequest();
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
            if (xmlHttp.responseText === "True") {
                return;
            } else {
                loadNewSession();
            }
        }
    }
    xmlHttp.open("get", window.location.origin + "/cart/CheckSession?sessionId=" + sessionId);
    xmlHttp.send();
});

function loadNewSession() {
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
            setCookie("sessionId", xmlHttp.responseText);
        }
    }
    xmlHttp.open("get", window.location.origin + "/cart/CreateSession");
    xmlHttp.send();
}

function AddToCart(element) {
    if (basketLoading) {
        return;
    }

    basketLoading = true;

    document.getElementById("basket-list").style.display = "none";
    document.getElementById("basket-btn").innerText = "🛒 Wait ...";

    let itemId = element.getAttribute('data-id');

    let sessionId = getCookie('sessionId');

    var xmlHttp = new XMLHttpRequest();

    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
            document.getElementById("basket-btn").innerText = "🛒";
            basketLoading = false;
        }
    }

    xmlHttp.open("get", `${window.location.origin}/cart/add?gameid=${itemId}&quontity=1&sessionId=${sessionId}`);
    xmlHttp.send();
}

function generateSessionId() {
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
            return xmlHttp.responseText;
        }
    }
    xmlHttp.open("get", window.location.origin + "/cart/CreateSession");
    xmlHttp.send();
}

function showBasket() {
    let sessionCookie = getCookie("sessionId");

    let basketList = document.getElementById("basket-list");
    
    if (basketList.style.display === "none") {
        basketList.style.display = "";
        document.getElementById("basket-list").innerHTML = "Loading ...";

        var xmlHttp = new XMLHttpRequest();
        xmlHttp.onreadystatechange = function () {
            if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
                const parsedBasket = JSON.parse(xmlHttp.responseText);
                let listOfBasket = '<ul>';

                for (var i = 0; i < parsedBasket.length; i++) {
                    listOfBasket += "<li>" + parsedBasket[i].Name + ": " + parsedBasket[i].Quantity + " - " +
                        (parsedBasket[i].Quantity * parsedBasket[i].Price) + "UAH</li>";
                }

                listOfBasket += '</ul> <button onClick="purchaseBasket()">Continue</button>';

                if (parsedBasket.length === 0) {
                    document.getElementById("basket-list").innerHTML = "Basket is empty";
                } else {
                    document.getElementById("basket-list").innerHTML = listOfBasket;
                }
            }
        }
        xmlHttp.open("get", window.location.origin + "/cart/GetCart?session=" + sessionCookie);
        xmlHttp.send();
    } else {
        basketList.style.display = "none";
    }
}

function purchaseBasket() {
    let sessionCookie = getCookie("sessionId");

    window.document.location = window.location.origin + "/purchase/index?session=" + sessionCookie;
}