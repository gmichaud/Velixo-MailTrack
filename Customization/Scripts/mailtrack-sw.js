'use strict';

self.addEventListener('push', function (event) {
    const eventData = event.data.json();
    const title = eventData.title;
    const options = {
        body: eventData.body,
        icon: 'icon.png',
        badge: 'badge.png',
        data: {
            noteID: eventData.refNoteID
        }
    };

    event.waitUntil(self.registration.showNotification(title, options));
});

self.addEventListener('notificationclick', function (event) {
    const baseUrl = event.currentTarget.origin + event.currentTarget.location.pathname.substring(0, event.currentTarget.location.pathname.lastIndexOf("/Scripts"));

    event.notification.close();
    event.waitUntil(
        clients.openWindow(`${baseUrl}/Main.aspx?ScreenId=CR306015&NoteID=${event.notification.data.noteID}`));
});