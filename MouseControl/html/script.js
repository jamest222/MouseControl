window.onload = function() {

	// Add event listener for the menu
	document.getElementById("menu_btn").addEventListener("click", function(e) {
		e.preventDefault();

		var menu = document.getElementById("menu");
		if (menu.className == "") { //Currently closed
			menu.className = "open";
			openMenu(menu);
		}
		else {
			menu.className = "";
			closeMenu(menu);
		}
	});
}

// Open the menu
function openMenu(menu) {
	$(menu).slideDown();
}

// Close the menu
function closeMenu(menu) {
	$(menu).slideUp();
}