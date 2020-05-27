(function () {
    window.mqttwrapper = {

        sayHello: () => "hello",

        createClient: (dotNetReference, topic) => {

            // Create a client instance
            client = new Paho.MQTT.Client("broker.mqttdashboard.com", 8000, "superclient_max_");

            // set callback handlers
            client.onConnectionLost = onConnectionLost;
            client.onMessageArrived = onMessageArrived;

            // connect the client
            client.connect({ onSuccess: onConnect });

            // called when the client connects
            function onConnect() {
                // Once a connection has been made, make a subscription and send a message.
                console.log("onConnect");
                client.subscribe(topic);
            }

            // called when the client loses its connection
            function onConnectionLost(responseObject) {
                if (responseObject.errorCode !== 0) {
                    console.log("onConnectionLost:" + responseObject.errorMessage);
                }
            }

            // called when a message arrives
            function onMessageArrived(message) {
                dotNetReference.invokeMethodAsync("OnMessageArrived", message.payloadString)
                    .then(r => console.log("onMessageArrived:" + message.payloadString));
                ;
            }
        }

    };
})();