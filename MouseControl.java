// The java file to control mouse movement

import java.awt.*;

class MouseControl {
	
	public static void main(String args[]) throws AWTException {
		// Create the robot to move the mouse
		Robot robot = new Robot();

		// Get the X and Y
		int mouseX = Integer.parseInt(args[0]);
		int mouseY = Integer.parseInt(args[1]);

		robot.mouseMove(mouseX,mouseY);
	}

}