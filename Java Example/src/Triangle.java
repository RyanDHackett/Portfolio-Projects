import java.awt.Color;
import java.awt.GradientPaint;
import java.awt.Graphics;
import java.awt.Graphics2D;
import java.awt.geom.Path2D;

import javax.swing.JPanel;


public class Triangle extends Shape{
private double height;
private double width;
private double centerx;
private double centery;
private double radius;
private GradientPaint trianglePaint;

public Triangle(double height,double width,double centerx, double centery,double radius,double xSpeed,double ySpeed,GradientPaint trianglePaint){
	this.height = height;
	this.width = width;
	this.centerx = centerx;
	this.centery=centery;
	this.radius = radius;
	this.xSpeed = xSpeed;
	this.ySpeed = ySpeed;
	this.setTrianglePaint(trianglePaint);
}








public double getHeight() {
	return height;
}
public void setHeight(double height) {
	this.height = height;
}
public double getWidth() {
	return width;
}
public void setWidth(double width) {
	this.width = width;
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

public void draw(Graphics2D g,JPanel jp)
{
	 Path2D.Double myTriangle = new Path2D.Double();
	 double[] myTriangleX = genTriangleXCoords(this.width,this.centerx);
	 double[] myTriangleY = genTriangleYCoords(this.height,this.centery,this.width);
	 
	 myTriangle.moveTo(myTriangleX[0], myTriangleY[0]);
	 
	 for(int i=1;i<myTriangleX.length;i++)
	 {
		 myTriangle.lineTo(myTriangleX[i], myTriangleY[i]);
	 }
	 myTriangle.lineTo(myTriangleX[0], myTriangleY[0]);
	 
	 g.fill(myTriangle);
}

public void move()
{
	this.centerx+=xSpeed;
	this.centery-=ySpeed;
}



public double[] genTriangleXCoords(double w,double x)//width and x coordinate of center point
{
	double[] xCoords = new double[3];
	xCoords[0] = x;
	xCoords[1] = x+(0.5*w);
	xCoords[2] = x-(0.5*w);

	return xCoords;
}

public double[] genTriangleYCoords(double h,double y,double width)//height and y coordinate of center point, width for finding the distance to the top point from the center point
{
	double distanceTobottom =(Math.sin(60)*width)-(2/3*(Math.sin(60)*width))+0.67*h;
	double distanceTotop = h-distanceTobottom;
	double[] yCoords = new double[3];
	yCoords[0] = y-distanceTotop;
	yCoords[1] = (y-distanceTotop)+h;
	yCoords[2] = (y-distanceTotop)+h;
	return yCoords;
}








public double getRadius() {
	return radius;
}








public void setRadius(double radius) {
	this.radius = radius;
}








public GradientPaint getTrianglePaint() {
	return trianglePaint;
}








public void setTrianglePaint(GradientPaint trianglePaint) {
	this.trianglePaint = trianglePaint;
}







}
