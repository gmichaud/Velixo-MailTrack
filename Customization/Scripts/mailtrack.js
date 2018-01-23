'use strict';

const applicationServerPublicKey = 'BAHU3QNr2PvPSufR9hKYgV7daemnktvh6AYqBqta9brwWK6S064nzJQCadUT4LD3fyJMs5FiKh9uW_v_t4aU4eI';

var isSubscribed = false;
var swRegistration = null;

if ('serviceWorker' in navigator && 'PushManager' in window) {
    console.log('Service Worker and Push is supported');
    document.querySelector('.js-push-available').classList.remove('is-invisible');
 
    navigator.serviceWorker.register('../Scripts/MailTrack/mailtrack-sw.js')
        .then(function (swReg) {
            console.log('Service Worker is registered', swReg);

            swRegistration = swReg;
            initializeUI();
        })
        .catch(function (error) {
            console.error('Service Worker Error', error);
        });
} else {
    console.warn('Push messaging is not supported');
    document.querySelector('.js-push-notsupported').classList.remove('is-invisible');
    document.querySelector('.js-enable-push-switch').MaterialSwitch.off();
    document.querySelector('.js-enable-push-switch').disabled = true;
    document.querySelector('.js-enable-push-switch-label').textContent = "Push Notifications: Not Supported";
}

function urlB64ToUint8Array(base64String) {
    const padding = '='.repeat((4 - base64String.length % 4) % 4);
    const base64 = (base64String + padding)
        .replace(/\-/g, '+')
        .replace(/_/g, '/');

    const rawData = window.atob(base64);
    const outputArray = new Uint8Array(rawData.length);

    for (var i = 0; i < rawData.length; ++i) {
        outputArray[i] = rawData.charCodeAt(i);
    }
    return outputArray;
}

function initializeUI() {
    document.querySelector('.js-enable-push-switch input').addEventListener('change', function () {
        if (isSubscribed) {
            unsubscribeUser();
        } else {
            subscribeUser();
        }
    });

    // Set the initial subscription value
    swRegistration.pushManager.getSubscription()
        .then(function (subscription) {
            isSubscribed = !(subscription === null);

            if (isSubscribed) {
                console.log('User IS subscribed.');
                updateSubscriptionOnServer(subscription);  //Resend subscription data in case server lost track of it
            } else {
                console.log('User is NOT subscribed.');
                // Don't update subscription on server since it would disable every push notification for this user - only do it when user turns it off
            }

            updateBtn();
        });
}

function updateBtn() {
    if (Notification.permission === 'denied') {
        console.warn('Push Notifications Blocked');
        document.querySelector('.js-enable-push-switch').MaterialSwitch.off();
        document.querySelector('.js-enable-push-switch').disabled = true;
        document.querySelector('.js-enable-push-switch-label').textContent = "Push Notifications: Blocked";
        return;
    }

    if (isSubscribed) {
        document.querySelector('.js-enable-push-switch').MaterialSwitch.on();
        document.querySelector('.js-enable-push-switch-label').textContent = "Push Notifications: On";
    }
    else {
        document.querySelector('.js-enable-push-switch').MaterialSwitch.off();
        document.querySelector('.js-enable-push-switch-label').textContent = "Push Notifications: Off";
    }
}

function subscribeUser() {
    const applicationServerKey = urlB64ToUint8Array(applicationServerPublicKey);
    swRegistration.pushManager.subscribe({
        userVisibleOnly: true,
        applicationServerKey: applicationServerKey
    })
        .then(function (subscription) {
            console.log('User is subscribed.');
            updateSubscriptionOnServer(subscription);
            isSubscribed = true;
            updateBtn();
        })
        .catch(function (err) {
            console.log('Failed to subscribe the user: ', err);
            updateBtn();
        });
}

function unsubscribeUser() {
    swRegistration.pushManager.getSubscription()
        .then(function (subscription) {
            if (subscription) {
                return subscription.unsubscribe();
            }
        })
        .catch(function (error) {
            console.log('Error unsubscribing', error);
        })
        .then(function () {
            updateSubscriptionOnServer(null);

            console.log('User is unsubscribed.');
            isSubscribed = false;

            updateBtn();
        });
}

function updateSubscriptionOnServer(subscription) {
    // NULL subscription object will be handled as unsubscribe by server
    console.log('Updating subscription on server', subscription);
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.open("POST", "../MailTrack/subscribe");
    xmlhttp.setRequestHeader("Content-Type", "application/json");
    xmlhttp.send(JSON.stringify(subscription));
}