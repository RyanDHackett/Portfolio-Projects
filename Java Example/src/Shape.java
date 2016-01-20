import javax.swing.*;

import java.awt.Color;
import java.awt.Graphics;
import java.awt.Graphics2D;
public abstract class Shape{
protected double xSpeed;
protected double ySpeed;
public double getxSpeed() {
	return xSpeed;
}
public void setxSpeed(double xSpeed) {
	this.xSpeed = xSpeed;
}
public double getySpeed() {
	return ySpeed;
}
public void setySpeed(double ySpeed) {
	this.ySpeed = ySpeed;
}

abstract void draw(Graphics2D g,JPanel jp);

abstract void move();




	
	
	
	
	
	
	
	
	
	
	
	
	
}
