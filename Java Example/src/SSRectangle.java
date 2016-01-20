import java.awt.Color;
import java.awt.Graphics;
import java.awt.Graphics2D;
import java.awt.TexturePaint;
import java.awt.geom.Rectangle2D;

import javax.swing.JPanel;



public class SSRectangle extends Shape{//SS for Screen Saver, to get around confusion with java.awt.Rectangle
	private double height;
	private double width;
	private double topLeftx;
	private double topLefty;
	private TexturePaint rectanglePaint;
	
	public SSRectangle(double height,double width,double topLeftx,double topLefty,double xSpeed,double ySpeed,TexturePaint rectanglePaint){
		this.height = height;
		this.width = width;
		this.topLeftx = topLeftx;
		this.topLefty =topLefty;
		this.xSpeed = xSpeed;
		this.ySpeed = ySpeed;
		this.setRectanglePaint(rectanglePaint);
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
		g.fill(new Rectangle2D.Double(topLeftx,topLefty,width,height));
	}
	
	public void move()
	{
		topLeftx+=xSpeed;
		topLefty+=ySpeed;
	}

	public TexturePaint getRectanglePaint() {
		return rectanglePaint;
	}

	public void setRectanglePaint(TexturePaint rectanglePaint) {
		this.rectanglePaint = rectanglePaint;
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
}
