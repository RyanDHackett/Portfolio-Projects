import java.awt.Color;
import java.awt.Graphics;
import java.awt.Graphics2D;
import java.awt.geom.Path2D;

import javax.swing.JPanel;


public class Cross extends Shape{
	private double width;
	private double height;
	private double topLeftx;
	private double topLefty;
	
	public Cross(double width,double height,double topLeftx,double topLefty,double xSpeed,double ySpeed){
		this.width =width;
		this.height =height;
		this.topLeftx =topLeftx;
		this.topLefty =topLefty;
		this.xSpeed = xSpeed;
		this.ySpeed = ySpeed;

	}
	
	
	
	
	
	
	
	public double getWidth() {
		return width;
	}
	public void setWidth(double width) {
		this.width = width;
	}
	public double getHeight() {
		return height;
	}
	public void setHeight(double height) {
		this.height = height;
	}
	public double getTopLeftx() {
		return topLeftx;
	}
	public void setTopLeftx(double topLeftx) {
		this.topLeftx = topLeftx;
	}
	public double getTopLefty() {
		return topLefty;
	}
	public void setTopLefty(double topLefty) {
		this.topLefty = topLefty;
	}
	
	
	public void draw(Graphics2D g,JPanel jp)
	{
		double[] crossXcoords=calcCrossXCoords(this.width,this.topLeftx);
		double[] crossYcoords=calcCrossYCoords(this.height,this.topLefty);
		Path2D.Double myCross = new Path2D.Double();
		myCross.moveTo(crossXcoords[0], crossYcoords[0]);
		for(int i=1;i<crossXcoords.length;i++)
		{
			myCross.lineTo(crossXcoords[i], crossYcoords[i]);
		}
		 myCross.lineTo(crossXcoords[0], crossYcoords[0]);
		 g.fill(myCross);
	}
	
	public double[] calcCrossXCoords(double width, double topLeftx)
	{
		double[] xCoords = new double[12];
		xCoords[0]=topLeftx+(width/3);
		xCoords[1]=topLeftx+(2*(width/3));
		xCoords[2]=topLeftx+(2*(width/3));
		xCoords[3]=topLeftx+width;
		xCoords[4]=topLeftx+width;
		xCoords[5]=topLeftx+(2*(width/3));
		xCoords[6]=topLeftx+(2*(width/3));
		xCoords[7]=topLeftx+(width/3);
		xCoords[8]=topLeftx+(width/3);
		xCoords[9]=topLeftx;
		xCoords[10]=topLeftx;
		xCoords[11]=topLeftx+(width/3);
		
		return xCoords;
	}

	public double[] calcCrossYCoords(double height, double topLefty)
	{
		double[] yCoords = new double[12];
		yCoords[0]=topLefty;
		yCoords[1]=topLefty;
		yCoords[2]=topLefty+(height/3);
		yCoords[3]=topLefty+(height/3);
		yCoords[4]=topLefty+2*(height/3);
		yCoords[5]=topLefty+2*(height/3);
		yCoords[6]=topLefty+height;
		yCoords[7]=topLefty+height;
		yCoords[8]=topLefty+2*(height/3);
		yCoords[9]=topLefty+2*(height/3);
		yCoords[10]=topLefty+(height/3);;
		yCoords[11]=topLefty+(height/3);
		
		return yCoords;

	}
	
	public void move()
	{
		this.topLeftx+=this.xSpeed;
		this.topLefty-=this.ySpeed;
	}
	
}
