var basketLoading = false;

window.addEventListener("load", function () {
    let sessionId = getCookie('sessionId');

    $.ajax({
        url: "cart/CheckSession?sessionId=" + sessionId
    }).done(function(response) {
        if (response === 'True') {
        } else {
            loadNewSession();
        }
    });
});

function loadNewSession() {
    $.ajax({
        url: "cart/CreateSession"
    }).done(function(response) {
        setCookie('sessionId', response);
    });
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

    $.ajax({
        url: `${window.location.origin}/cart/add?gameid=${itemId}&quontity=1&sessionId=${sessionId}`
    }).done(function() {
        document.getElementById("basket-btn").innerText = "🛒";
        basketLoading = false;
    });
}
function showBasket() {
    let sessionCookie = getCookie("sessionId");

    let basketList = document.getElementById("basket-list");

    if (basketList.style.display === "none") {
        basketList.style.display = "";
        document.getElementById("basket-list").innerHTML = "Loading ...";

        $.ajax({
            url: "cart/GetCart?session=" + sessionCookie
        }).done(function(response) {
            const parsedBasket = response;
            let listOfBasket = '<ul>';

            for (let i = 0; i < parsedBasket.length; i++) {
                listOfBasket += "<li>" + parsedBasket[i].Name + ": " + parsedBasket[i].Quantity + " - " +
                    (parsedBasket[i].Quantity * parsedBasket[i].Price) + "UAH</li>";
            }

            listOfBasket += '</ul> <button onClick="purchaseBasket()">Continue</button>';

            if (parsedBasket.length === 0) {
                document.getElementById("basket-list").innerHTML = "Basket is empty";
            } else {
                document.getElementById("basket-list").innerHTML = listOfBasket;
            }
        });
    } else {
        basketList.style.display = "none";
    }
}

function purchaseBasket() {
    let sessionCookie = getCookie("sessionId");

    window.document.location = window.location.origin + "/purchase/index?session=" + sessionCookie;
}