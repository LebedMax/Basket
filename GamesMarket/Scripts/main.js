function getCookie(name) {
    let matches = document.cookie.match(new RegExp(
        "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
    ));
    return matches ? decodeURIComponent(matches[1]) : undefined;
}

function setCookie(name, value, options = {}) {

    options = {
        path: '/',
        // при необходимости добавьте другие значения по умолчанию
        ...options
    };

    if (options.expires instanceof Date) {
        options.expires = options.expires.toUTCString();
    }

    let updatedCookie = encodeURIComponent(name) + "=" + encodeURIComponent(value);

    for (let optionKey in options) {
        updatedCookie += "; " + optionKey;
        let optionValue = options[optionKey];
        if (optionValue !== true) {
            updatedCookie += "=" + optionValue;
        }
    }

    document.cookie = updatedCookie;
}

function updateSessionId() {
    let currentSessionId = getCookie('sessionId');
    if (currentSessionId !== undefined) {
        var xmlHttp = new XMLHttpRequest();
        xmlHttp.onreadystatechange = function () {
            if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
                if (xmlHttp.responseText === 'False') {
                    getNewSession();
                }
            }
        }
        xmlHttp.open("get", window.location.origin + "/cart/CheckSession?sessionId=" + currentSessionId);
        xmlHttp.send();
    }

    if (currentSessionId === undefined || currentSessionId == 'undefined') {
        getNewSession();
    }
}

function getNewSession() {
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
            setCookie('sessionId', xmlHttp.responseText);
        }
    }
    xmlHttp.open("get", window.location.origin + "/cart/CreateSession");
    xmlHttp.send();
}

document.addEventListener('DOMContentLoaded', updateSessionId, false);