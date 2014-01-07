window.onload = function() {
	// Websocket parts
	var serverConnection = new WebSocket("ws://192.168.0.2:9000");
	
	serverConnection.onopen = function() {
		console.log("Connected");
	}

	// Buttons (Temporary)
	var buttons = document.getElementsByTagName("button");
	for (var i = 0; i < buttons.length; i++) {
		buttons[i].addEventListener("click", function() {
			var direction = this.id;
			serverConnection.send(direction);
		});
	}
}