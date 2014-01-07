// The java file to control mouse movement

import java.awt.*;

class MouseControl {
	
	public static void main(String args[]) throws AWTException {
		// Create the robot to move the mouse
		Robot robot = new Robot();

		// Get the current mouse position
		PointerInfo mouse = MouseInfo.getPointerInfo();
		Point p = mouse.getLocation();
		int currX = (int)p.getX();
		int currY = (int)p.getY();

		// Get the X and Y
		int mouseX = currX + Integer.parseInt(args[0]);
		int mouseY = currY + Integer.parseInt(args[1]);

		robot.mouseMove(mouseX,mouseY);
	}

}