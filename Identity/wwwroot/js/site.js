// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener('DOMContentLoaded', function () {
    var cookieNotification = document.getElementById('cookie-notification');
    var acceptCookiesButton = document.getElementById('accept-cookies');
    var declineCookiesButton = document.getElementById('decline-cookies');

    // Check if the user has already accepted cookies
    var cookiesAccepted = localStorage.getItem('cookiesAccepted');
    if (!cookiesAccepted) {
        cookieNotification.style.display = 'block';
    }

    // Handle accept cookies button click
    acceptCookiesButton.addEventListener('click', function () {
        // Set cookie to remember user's choice
        localStorage.setItem('cookiesAccepted', true);
        // Hide the notification
        cookieNotification.style.display = 'none';
    });

    declineCookiesButton.addEventListener('click', function () {
        // Set cookie to remember user's choice
        localStorage.setItem('cookiesDeclined', true);
        // Hide the notification
        cookieNotification.style.display = 'none';
    });
});