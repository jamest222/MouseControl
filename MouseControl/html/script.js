window.onload = function() {

	// Create websocket
	var ws = new WebSocket("ws://localhost:9000");
	ws.onopen = function() {
		ws.send("Test from client");
	}

	document.getElementById("menu_btn").addEventListener("click", function(e) {
		e.preventDefault();
		ws.send("From btn");
	});

}