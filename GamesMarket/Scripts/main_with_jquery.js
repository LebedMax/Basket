function getCookie(name) {
    let matches = document.cookie.match(new RegExp(
        "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
    ));
    return matches ? decodeURIComponent(matches[1]) : undefined;
}

function setCookie(name, value, options = {}) {

    options = {
        path: '/',
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
        $.ajax({
            url: "cart/CheckSession?sessionId=" + currentSessionId
        }).done(function(response) {
            if (response === 'False') {
                getNewSession();
            }
        });
    }

    if (currentSessionId === undefined || currentSessionId == 'undefined') {
        getNewSession();
    }
}

function getNewSession() {
    $.ajax({
        url: "cart/CreateSession"
    }).done(function(response) {
        setCookie('sessionId', response);
    });
}

document.addEventListener('DOMContentLoaded', updateSessionId, false);