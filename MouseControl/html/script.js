window.onload = function() {

	var centerX = $(window).width() / 2;
	var centerY = $(window).height() / 2;

	if (WebSocket) {
		//alert("Supported");
	}

	// Create websocket
	var ws = new WebSocket("ws://192.168.0.2:9000");
	ws.onopen = function() {
		//alert("connected");
		//ws.send("Test from client");
	}

	// use body as scroll
	var body = document.getElementsByTagName("body")[0];
	body.addEventListener("touchmove", function(e) {
		e.preventDefault();

		// Get amount to move 
		var xMove = (e.touches[0].pageX - centerX);
		var yMove = (e.touches[0].pageY - centerY);
		var move = xMove.toString()+","+yMove.toString();
		ws.send(move);

	});


}