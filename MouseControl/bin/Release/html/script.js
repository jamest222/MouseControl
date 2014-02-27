window.onload = function() {

	var startX, startY, leftDbl = 0, rightDbl = 0, keyboard = false;

	if (WebSocket) {

		// Create websocket
		var url = document.URL;
		var pieces = url.split(":");
		pieces[0] = "ws";
		pieces.pop();
		url = pieces.join(":") + ":9000";
		var ws = new WebSocket(url);

		ws.onopen = function() {

			/*
			*  Mouse
			*/

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

				// Make sure keyboard isn't in use
				if (!keyboard) {
					ws.send(move);
				}

			});

			// update start touch
			body.addEventListener("touchstart", function(e) {
				startX = e.touches[0].pageX;
				startY = e.touches[0].pageY;

				/*
				* Detect a tap
				*/
				// Cache the X/Y (to detect whether move or tap)
				var cacheX = startX;
				var cacheY = startY;
				// Detect tap
				setTimeout(function() {
					if ((cacheY == startY) && (cacheX == startX)) {
						ws.send("left");
					}
				}, 200);

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

			/*
			* Keyboard
			*/
			// Listen for device orientation change to display keyboard
			window.addEventListener("orientationchange", function() {
				if (window.orientation == 0) {
					keyboard = false;
					hideKeyboard();
				}
				else {
					keyboard = true;
					showKeyboard();
				}
			});

			// Listen for keyboard keys
			var key = document.getElementById("staging_area");
			key.addEventListener("keydown", function(e) {
				var sendMsg = "key,"+e.keyCode;
				ws.send(sendMsg);
			});

			// Listen for keyboard display;
			key.addEventListener("focus", function() {
				this.innerHTML = "";
				this.style.color = "#fff";
			});
			key.addEventListener("blur", function() {
				this.innerHTML = "Tap to open the keyboard";
				this.style.color = "#e8e8e8";
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

// Show the keyboard
function showKeyboard() {
	var key = document.getElementById("keyboard");
	key.style.display = "block";
}

// Hide the keyboard
function hideKeyboard() {
	var key = document.getElementById("keyboard");
	key.style.display = "none";
	key.getElementsByTagName("textarea")[0].blur();
}