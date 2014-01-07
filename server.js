/*
*	Calls the Java class to move the mouse and response to user calls
*/

// Requires
var spawn = require("child_process").spawn;

// Move the mouse
function moveMouse(mouseX, mouseY) {
	var child = spawn("java", ["MouseControl", mouseX, mouseY]);
}

moveMouse("100", "200");