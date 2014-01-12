window.onload = function() {

	var startX, startY, leftDbl = 0, rightDbl = 0;

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


				ws.send(move);

			});

			// update start touch
			body.addEventListener("touchstart", function(e) {
				startX = e.touches[0].pageX;
				startY = e.touches[0].pageY;
			});

			// Add support for mouse buttons
			var left = document.getElementById("left");
			left.addEventListener("click", function(e) {
				e.preventDefault();
			});
			// Use touchend for better responsiveness than click
			left.addEventListener("touchend", function(e) {
				leftDbl++;
				setTimeout(function() {
					if (leftDbl == 0) {
						return;
					}
					else if (leftDbl == 1) {
						leftDbl = 0;
						ws.send("left");
					}
					else {
						leftDbl = 0;
						ws.send("left_dbl");
					}
				}, 500);
			});

			var right = document.getElementById("right");
			right.addEventListener("click", function(e) {
				e.preventDefault();
			});
			// Once again touchend for right too
			right.addEventListener("touchend", function() {
				ws.send("right");
			});
		}
	}
}

// Provide styling to the touch area
function provideStyling(x, y) {
	var glow = document.createElement("div");
	glow.style.left = x + "px";
	glow.style.top = y + "px";
	glow.id = "highlightTouch";
	document.getElementsByTagName("body")[0].appendChild(glow);
}

function removeStyling() {
	var glow = document.getElementById("highlightTouch");
	glow.parentNode.removeChild(glow);
}