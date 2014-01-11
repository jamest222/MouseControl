window.onload = function() {

	var startX, startY;

	if (WebSocket) {
		// Create websocket
		var ws = new WebSocket("ws://192.168.0.2:9000");
		ws.onopen = function() {

			// use body as scroll
			var body = document.getElementsByTagName("body")[0];
			body.addEventListener("touchmove", function(e) {
				e.preventDefault();

				// Get amount to move 
				var xMove = (e.touches[0].pageX - startX);
				var yMove = (e.touches[0].pageY - startY);
				var move = xMove.toString()+","+yMove.toString();

				// Set new start position
				startX = e.touches[0].pageX;
				startY = e.touches[0].pageY;

				body.innerHTML += xMove.toString() + "<br>";

				ws.send(move);

			});

			// update start touch
			body.addEventListener("touchstart", function(e) {
				startX = e.touches[0].pageX;
				startY = e.touches[0].pageY;
			});
		}
	}


}