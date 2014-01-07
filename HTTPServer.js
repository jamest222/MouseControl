/*
*
*	The Extended HTTP server - capable of delivering HTML/CSS/JS files
*
*/

/* 
*	REQUIRE FILES
*/
var http = require("http");
var fs = require("fs");

function HTTPServer() {

	http.createServer(function(req, res) {
		
		if (req.url == "/") {
			fs.readFile(getFilePath("front"+getOSJoin()+"index.htm"), function(err, data) {
				if (err) {
					handleErrRequest(res);
					return;
				}
				serveFile(res, data, "text/html", 200);
			});
		}
		else if (req.url.indexOf(".htm") != -1 || req.url.indexOf("html") != -1) {
			fs.readFile(getFilePath("front"+getOSJoin()+req.url.substring(1)), function(err, data) {
				if (err) {
					handleErrRequest(res);
					return;
				}
				serveFile(res, data, "text/html", 200);
			});
		}
		else if (req.url.indexOf(".css") != -1) {
			fs.readFile(getFilePath("front"+getOSJoin()+req.url.substring(1)), function(err, data) {
				if (err) {
					handleErrRequest(res);
					return;
				}
				serveFile(res, data, "text/css", 200);
			});
		}
		else if (req.url.indexOf(".js") != -1) {
			fs.readFile(getFilePath("front"+getOSJoin()+req.url.substring(1)), function(err, data) {
				if (err) {
					handleErrRequest(res);
					return;
				}
				serveFile(res, data, "text/javascript", 200);
			});
		}
		else if (req.url.indexOf(".png") != -1) {
			fs.readFile(getFilePath("front"+getOSJoin()+req.url.substring(1)), function(err, data) {
				if (err) {
					handleErrRequest(res);
					return;
				}
				serveFile(res, data, "image/png", 200);
			});
		}
		else if (req.url.indexOf(".jpg") != -1) {
			fs.readFile(getFilePath("front"+getOSJoin()+req.url.substring(1)), function(err, data) {
				if (err) {
					handleErrRequest(res);
					return;
				}
				serveFile(res, data, "image/jpeg", 200);
			});
		}
		else {
			handleErrRequest(res);
		}

	}).listen(8080);

	// Serve a file
	function serveFile(response, file, contType, statusCode) {
		response.writeHead(statusCode, {"Content-Type": contType});
		response.write(file);
		response.end();
	}

	// Get filepath
	function getFilePath(sub) {
		var pathArr = __dirname.split(getOSJoin());
		//pathArr.pop();
		path = pathArr.join(getOSJoin());
		return path + getOSJoin() + sub;
	}

	// Get OS join
	function getOSJoin() {
		if (process.platform == "win32") {
			return "\\";
		}
		else {
			return "/";
		}
	}

	// Handle err
	function handleErrRequest(res) {
		serveFile(res, "<h1>FILE NOT FOUND</h1>", "text/html", 200);
	}
}

module.exports = HTTPServer;