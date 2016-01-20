import java.awt.Color;
import java.awt.Graphics;
import java.awt.Graphics2D;
import java.awt.geom.Ellipse2D;

import javax.swing.JPanel;


public class Circle extends Shape{
private double radius;
private double centerx;
private double centery;
private Color circleColor;
public Circle(double radius,double centerx,double centery,double xSpeed,double ySpeed,Color circleColor){
	this.radius = radius;
	this.centerx = centerx;
	this.centery = centery;
	this.xSpeed = xSpeed;
	this.ySpeed = ySpeed;
	this.circleColor =circleColor;
}
public double getCenterx() {
	return centerx;
}
public void setCenterx(double centerx) {
	this.centerx = centerx;
}
public double getCentery() {
	return centery;
}
public void setCentery(double centery) {
	this.centery = centery;
}
public double getRadius() {
	return radius;
}
public void setRadius(double radius) {
	this.radius = radius;
}

public void draw(Graphics2D g,JPanel jp)
{
	g.fill(createCircle(this.centerx,this.centery,this.radius));
}

public void move()
{
	this.centerx+=this.xSpeed;
	this.centery-=this.ySpeed;
}


public Ellipse2D.Double createCircle(double centerx,double centery,double radius)
{
	Ellipse2D.Double myCircle = new Ellipse2D.Double(centerx-radius, centery-radius, radius*2, radius*2);
	return myCircle;
}
public Color getCircleColor() {
	return circleColor;
}
public void setCircleColor(Color circleColor) {
	this.circleColor = circleColor;
}

}
