self.addEventListener('notificationclick', function (event) {
    if (!event.action) { 
        console.log('Notification Click.');
        return;
    }
    console.log("event", event)
    console.log("event.action", event.action)

    function getevent() {
        if (event.notification) {
            if (event.notification.data) {
                if (event.notification.data.actions) {
                    console.log("event.notification.data.actions", event.notification.data.actions.filter(x => x.action == event.action))
                    return event.notification.data.actions.find(x => x.action == event.action)
                }
            }
        } 
    } 
    var action = getevent();
    if (action) {
        console.log("actionfocus", action)
        if (action.url) {
            focusWindow(event, action.url) 
        }
        return;
    }

    switch (event.action) {

        case 'coffee-action':
            console.log('User ❤️️\'s coffee.');
            break;
        case 'doughnut-action':
            console.log('User ❤️️\'s doughnuts.');
            break;
        case 'gramophone-action':
            console.log('User ❤️️\'s music.');
            break;
        case 'atom-action':
            console.log('User ❤️️\'s science.');
            break;
        default:
            console.log(`Unknown action clicked: '${event.action}'`);
            break;
    }
}); 

const notificationCloseAnalytics = () => {
    return Promise.resolve();
};
self.addEventListener('notificationclose', function (event) {
    const dismissedNotification = event.notification; 
    console.log("dismissedNotification", event.notification.title)
    const promiseChain = notificationCloseAnalytics();
    event.waitUntil(promiseChain);
    console.log("promiseChain", promiseChain)
});
function isClientFocused() {
    return clients.matchAll({
        type: 'window',
        includeUncontrolled: true
    })
        .then((windowClients) => {
            let clientIsFocused = false;
            for (let i = 0; i < windowClients.length; i++) {
                const windowClient = windowClients[i];
                if (windowClient.focused) {
                    clientIsFocused = true;
                    break;
                }
            } 
            return clientIsFocused;
        });
}
function focusWindow(event, examplePageURL) { 
    const urlToOpen = new URL(examplePageURL, self.location.origin).href; 
    const promiseChain = clients.matchAll({
        type: 'window',
        includeUncontrolled: true
    }) 
        .then((windowClients) => {
            let matchingClient = null; 
            for (let i = 0; i < windowClients.length; i++) {
                const windowClient = windowClients[i];
                if (windowClient.url === urlToOpen) {
                    matchingClient = windowClient;
                    break;
                }
            } 
            if (matchingClient) {
                return matchingClient.focus();
            } else {
                return clients.openWindow(urlToOpen);
            }
        }); 
    event.waitUntil(promiseChain); 
}
 

