<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MailTrackPushSetup.aspx.cs" Inherits="Frames_MailTrackPushSetup" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

    <title>Velixo MailTrack Notifications</title>

    <link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons">
    <link rel="stylesheet" href="https://code.getmdl.io/1.2.1/material.indigo-pink.min.css">
    <script defer src="https://code.getmdl.io/1.2.1/material.min.js"></script>
    <style type="text/css">
        html {
            height: 100%;
        }

        html, body {
            width: 100%;
            padding: 0;
            margin: 0;
        }

        body {
            min-height: auto;
            box-sizing: border-box;
        }

        header {
            padding: 115px 0 32px 0;
            background-color: #2F6493;
            color: white;
        }

            main, header > h1 {
                padding: 0 16px;
                max-width: 760px;
                box-sizing: border-box;
                margin: 0 auto;
            }

        main {
            margin: 24px auto;
            box-sizing: border-box;
        }

        pre {
            white-space: pre-wrap;
            background-color: #EEEEEE;
            padding: 16px;
        }

            pre code {
                word-break: break-word;
            }

        .is-invisible {
            opacity: 0;
        }

        .subscription-details {
            transition: opacity 1s;
        }

        @media (max-width: 600px) {
            header > h1 {
                font-size: 36px;
            }
        }
    </style>
</head>

<body>

    <header>
        <h1>Velixo MailTrack Notifications</h1>
    </header>

    <main>
        <section class="js-push-notsupported is-invisible">
            <p>
                Your browser does not support Push Notifications.
            </p>
        </section>
        <section class="js-push-available is-invisible">
            <p>
                When you enable Push Notifications, Acumatica will notify you about events related to the emails you send from Acumatica such as:
                <ul>
                    <li>When an email is delivered</li>
                    <li>When an email bounces</li>
                    <li>When an email is opened by the recipient</li>
                    <li>When a link inside an email is clicked</li>
                </ul>
                A detailed log of all the events of an email is kept in the Tracking tab of the Email Activity page.
            </p>
            <p>
                <b>Note:</b>
                Notifications are activated per device. To receive notifications on multiple devices, sign in on each device, enable notifications,
                and make sure you're online. However, turning off notifications on one device will turn them off on all devices.
            </p>
        </section>
        <p>
            <label class="js-enable-push-switch mdl-checkbox mdl-switch mdl-js-switch mdl-js-ripple-effect" for="enable-push-switch">
                <input type="checkbox" id="enable-push-switch" class="mdl-switch__input">
                <span class="js-enable-push-switch-label mdl-switch__label">Push Notifications</span>
            </label>
        </p>
    </main>

    <script src="https://code.getmdl.io/1.2.1/material.min.js"></script>
    
    <script type="text/javascript">
        'use strict';

        const applicationServerPublicKey = 'BAHU3QNr2PvPSufR9hKYgV7daemnktvh6AYqBqta9brwWK6S064nzJQCadUT4LD3fyJMs5FiKh9uW_v_t4aU4eI';

        var isSubscribed = false;
        var swRegistration = null;

        if ('serviceWorker' in navigator && 'PushManager' in window) {
            console.log('Service Worker and Push is supported');
            document.querySelector('.js-push-available').classList.remove('is-invisible');

            navigator.serviceWorker.register('<%= PX.Common.PXUrl.RemoveSessionSplit(Page.ResolveUrl("~/Scripts/MailTrack/mailtrack-sw.js")) %>')
                .then(function (swReg) {
                    console.log('Service Worker is registered', swReg);

                    swRegistration = swReg;
                    initializeUI();
                })
                .catch(function (error) {
                    console.error('Service Worker Error', error);
                    alert(error);
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
            xmlhttp.open("POST", '<%= PX.Common.PXUrl.RemoveSessionSplit(Page.ResolveUrl("~/MailTrack/subscribe")) %>');
            xmlhttp.setRequestHeader("Content-Type", 'application/json');
            xmlhttp.send(JSON.stringify(subscription));
        }
    </script>
</body>
</html>
