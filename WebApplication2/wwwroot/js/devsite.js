 
async function getsw() {
 if ("serviceWorker" in navigator) {
    console.log("registering sw")
     var sw = await navigator.serviceWorker.register('/devsw.js');
     return sw;
    }
}
var mydata = {
    actions: [
        {
            action: "view-content",
            title: "Open",
            url: "https://localhost:7254/Home/Privacy"
        },
        {
            action: "go-home",
            title: "Dismiss"
        }
    ]
}
async function notify(title, data, message) {
    data=data=='mydata'?mydata:null
    console.log("Notify ", title, data, message)
    if (window.Notification && Notification.permission === "granted") {
        var sw = await getsw()
        var options = {
            data: data,
            body: message,
            icon: "/img/logo.png",
            image: "/img/logo.png", 
        };
        if (data && data.actions) {
            options.actions = data.actions
        }
        sw.showNotification(title, options);
    }
    else if (window.Notification && Notification.permission !== "denied") {
        Notification.requestPermission(status => {
            if (status === "granted") {
                notify(title, data, message)
            } else {
                alert("You denied or dismissed permissions to notifications.");
            }
        });
    } else {
        // If the user refuses to get notified
        alert(
            "You denied permissions to notifications. Please go to your browser or phone setting to allow notifications."
        );
    }
}



function connect() {
    if (connection == null) {
        connection = new signalR.HubConnectionBuilder()
            .withUrl("/ChatHub")
            .configureLogging(signalR.LogLevel.Information)
            .build();

        connection.on('newMessage', (sender, messageText, time) => {
            console.log(`${sender}:${messageText}`);
            const newMessage = document.createElement('li');
            var m = JSON.parse(messageText)
            console.log("newMessage", m)
        });

        connection.start()
            .then(() => {
                //hub.server.join("P");
                //connection.invoke('Join', "Pierre");
                console.log('connected!')
                connection = connection;
            })
            .catch(console.error);
    } else {
        console.log('already connectd!')
    }

}

var connection;

async function msg(msg) { 
    //connection.invoke('SendMessage', msg, { text: msg }, { text: msg });
    connect() 
    if (connection) {
        console.log("  connected")
        connection.invoke('SendMessage', msg, JSON.stringify({ text: msg }), msg);
    } else {
        console.log("not connected")
    }
}