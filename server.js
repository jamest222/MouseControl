/*
*	Calls the Java class to move the mouse and response to user calls
*/

// Requires
var spawn = require("child_process").spawn;
var HTTPServer = require("./HTTPServer.js");
var WebSocketServer = require("WebSocketServer");

// Create the HTTP server (listens on 8080)
console.log("Starting HTTP server...");
var http = new HTTPServer();

// Create the WebSocket Server
console.log("Creating WebSocket server...");
var ws = new WebSocketServer("none", 9000);
var connectionList = [];
ws.on("connection", function(id) {
    connectionList.push(id);
    console.log(id);
});

ws.on("message", function(data, id) {
	var direction = ws.convertToString(ws.unmaskMessage(data).message);
	calcXY(direction);
});

// Calc X AND Y
function calcXY(direction) {
	switch(direction) {
		case "left":
			moveMouse("-100", "0");
			break;
		case "right":
			moveMouse("100", "0");
			break;
		case "down":
			moveMouse("0", "100");
			break;
		case "up":
			moveMouse("0", "-100");
			break;
	}
}

// Move the mouse
function moveMouse(mouseX, mouseY) {
	var child = spawn("java", ["MouseControl", mouseX, mouseY]);
}

//moveMouse("100", "-200");