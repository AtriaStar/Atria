function checkCookie(cookieName) {
    return document.cookie.split(';').map(x => x.split('=')[0]).includes(cookieName);
}
