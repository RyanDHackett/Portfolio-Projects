

import javax.swing.JPanel;
import javax.swing.Timer;

import java.util.Random;
import java.util.ArrayList;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.geom.*;
import java.awt.image.*;

import javax.swing.*;

public class animationPanel extends JPanel{
	ArrayList<Shape> shapesArray = new ArrayList<Shape>();
	private final int ANIMATION_DELAY = 10;
	Random rng = new Random();
	private Timer animationTimer = new Timer(ANIMATION_DELAY,new TimerHandler());
	BufferedImage buffImage = new BufferedImage(10,10,BufferedImage.TYPE_INT_RGB);
	Color[] Colours = new Color[]{Color.BLACK,Color.BLUE,Color.CYAN,Color.GREEN,Color.ORANGE,Color.RED,Color.YELLOW,Color.MAGENTA,Color.GRAY,Color.WHITE};
	boolean stretchOut = true;
	int stretchCount = 0;
	int visualChangecounter = 0;

	
	
	public animationPanel(){
		animationTimer.start();
	}
	
	@Override public void paintComponent(Graphics g)
	{
		super.paintComponent(g);//prevents subtle drawing errors
		Graphics2D g2d = (Graphics2D) g;
		
		for(int x=0;x<shapesArray.size();x++)
		{
			shapesArray.get(x).move();
		}
		
		for(int x=0;x<shapesArray.size();x++)
		{
			if(shapesArray.get(x) instanceof Circle)
			{
				if(((Circle) shapesArray.get(x)).getRadius()+((Circle) shapesArray.get(x)).getCenterx() >= this.getWidth())
				{
					((Circle) shapesArray.get(x)).setxSpeed(((Circle) shapesArray.get(x)).getxSpeed()*-1);
				}
				else if(((Circle) shapesArray.get(x)).getRadius()+((Circle) shapesArray.get(x)).getCentery() >= this.getHeight())
				{
					((Circle) shapesArray.get(x)).setySpeed(((Circle) shapesArray.get(x)).getySpeed()*-1);
				}
				
				if(((Circle) shapesArray.get(x)).getCenterx()-((Circle) shapesArray.get(x)).getRadius() <= 0)
				{
					((Circle) shapesArray.get(x)).setxSpeed(((Circle) shapesArray.get(x)).getxSpeed()*-1);
				}
				else if(((Circle) shapesArray.get(x)).getCentery()-((Circle) shapesArray.get(x)).getRadius() <= 0)
				{
					((Circle) shapesArray.get(x)).setySpeed(((Circle) shapesArray.get(x)).getySpeed()*-1);
				}
			}
			else if(shapesArray.get(x) instanceof Triangle)
			{
				if(((Triangle) shapesArray.get(x)).getRadius()+((Triangle) shapesArray.get(x)).getCenterx() >= this.getWidth())
				{
					((Triangle) shapesArray.get(x)).setxSpeed(((Triangle) shapesArray.get(x)).getxSpeed()*-1);
				}
				else if(((Triangle) shapesArray.get(x)).getRadius()+((Triangle) shapesArray.get(x)).getCentery() >= this.getHeight())
				{
					((Triangle) shapesArray.get(x)).setySpeed(((Triangle) shapesArray.get(x)).getySpeed()*-1);
				}
				
				if(((Triangle) shapesArray.get(x)).getCenterx()-((Triangle) shapesArray.get(x)).getRadius() <= 0)
				{
					((Triangle) shapesArray.get(x)).setxSpeed(((Triangle) shapesArray.get(x)).getxSpeed()*-1);
				}
				else if(((Triangle) shapesArray.get(x)).getCentery()-((Triangle) shapesArray.get(x)).getRadius() <= 0)
				{
					((Triangle) shapesArray.get(x)).setySpeed(((Triangle) shapesArray.get(x)).getySpeed()*-1);
				}
			}
			else if(shapesArray.get(x) instanceof SSRectangle)
			{
				if((((SSRectangle) shapesArray.get(x)).getTopLeftx()+((SSRectangle) shapesArray.get(x)).getWidth()) >=this.getWidth())
				{
					((SSRectangle) shapesArray.get(x)).setxSpeed(((SSRectangle) shapesArray.get(x)).getxSpeed()*-1);
				}
				else if((((SSRectangle) shapesArray.get(x)).getTopLefty()+((SSRectangle) shapesArray.get(x)).getHeight()) >=this.getHeight())
				{
					((SSRectangle) shapesArray.get(x)).setySpeed(((SSRectangle) shapesArray.get(x)).getySpeed()*-1);
				}
				
				if(((SSRectangle) shapesArray.get(x)).getTopLeftx() -1<=0)
				{
					((SSRectangle) shapesArray.get(x)).setxSpeed(((SSRectangle) shapesArray.get(x)).getxSpeed()*-1);
				}
				else if(((SSRectangle) shapesArray.get(x)).getTopLefty() -1<=0)
				{
					((SSRectangle) shapesArray.get(x)).setySpeed(((SSRectangle) shapesArray.get(x)).getySpeed()*-1);
				}
			}
			else if(shapesArray.get(x) instanceof Cross)
			{
				if((((Cross) shapesArray.get(x)).getTopLeftx()+((Cross) shapesArray.get(x)).getWidth()+3) >=this.getWidth())
				{
					((Cross) shapesArray.get(x)).setxSpeed(((Cross) shapesArray.get(x)).getxSpeed()*-1);
				}
				else if((((Cross) shapesArray.get(x)).getTopLefty()+((Cross) shapesArray.get(x)).getHeight()+3) >=this.getHeight())
				{
					((Cross) shapesArray.get(x)).setySpeed(((Cross) shapesArray.get(x)).getySpeed()*-1);
				}
				
				if(((Cross) shapesArray.get(x)).getTopLeftx() -1<=0)
				{
					((Cross) shapesArray.get(x)).setxSpeed(((Cross) shapesArray.get(x)).getxSpeed()*-1);
				}
				else if(((Cross) shapesArray.get(x)).getTopLefty() -1<=0)
				{
					((Cross) shapesArray.get(x)).setySpeed(((Cross) shapesArray.get(x)).getySpeed()*-1);
			}
		}
		}
		
		if(visualChangecounter==100)
		{
		for(int x=0;x<shapesArray.size();x++)
		{
		
		if(shapesArray.get(x) instanceof Circle)
		{
			((Circle) shapesArray.get(x)).setCircleColor(Colours[rng.nextInt(10)]);
		}
		else if(shapesArray.get(x) instanceof Triangle)
		{
			((Triangle) shapesArray.get(x)).setTrianglePaint(new GradientPaint(5,30,Colours[rng.nextInt(10)],35,100,Colours[rng.nextInt(10)],true));
		}
		else if(shapesArray.get(x) instanceof SSRectangle)
		{
			((SSRectangle) shapesArray.get(x)).setRectanglePaint(makePlaid());
		}
		else if(shapesArray.get(x) instanceof Cross)
		{
			
			if(stretchOut)
			{
				((Cross) shapesArray.get(x)).setWidth(((Cross) shapesArray.get(x)).getWidth() +3);
				((Cross) shapesArray.get(x)).setHeight(((Cross) shapesArray.get(x)).getHeight() +3);
				stretchCount++;
				if(stretchCount ==30)
				{
					stretchOut = false;
				}
			}
			else
			{
				((Cross) shapesArray.get(x)).setWidth(((Cross) shapesArray.get(x)).getWidth() -3);
				((Cross) shapesArray.get(x)).setHeight(((Cross) shapesArray.get(x)).getHeight() -3);
				stretchCount--;
				if(stretchCount ==0)
				{
					stretchOut = true;
				}
			}
		}
			
		
		}
		visualChangecounter = 0;
		}
		else
		{
			visualChangecounter++;
		}
		

		for(int x=0;x<shapesArray.size();x++)
		{
		
		if(shapesArray.get(x) instanceof Circle)
		{
			g2d.setPaint(((Circle) (shapesArray.get(x))).getCircleColor());
			shapesArray.get(x).draw(g2d,this);
		}
		else if(shapesArray.get(x) instanceof Triangle)
		{
			g2d.setPaint(((Triangle) (shapesArray.get(x))).getTrianglePaint());
			shapesArray.get(x).draw(g2d,this);
		}
		else if(shapesArray.get(x) instanceof SSRectangle)
		{
			g2d.setPaint(((SSRectangle) (shapesArray.get(x))).getRectanglePaint());
			shapesArray.get(x).draw(g2d,this);
		}
		else if(shapesArray.get(x) instanceof Cross)
		{
			//cross doesn't change colours for now
			g2d.setPaint(Color.BLACK);
			shapesArray.get(x).draw(g2d,this);
		}	
		}
		
		
	}
	
	private class TimerHandler implements ActionListener
	{
		@Override public void actionPerformed(ActionEvent actionEvent)
		{
			repaint();
		}
	}
	
	public TexturePaint makePlaid()
	{
		Graphics2D gg = buffImage.createGraphics();
		gg.setColor(Colours[rng.nextInt(10)]);
		gg.fillRect(0, 0, 10, 10);
		gg.setColor(Colours[rng.nextInt(10)]);
		gg.drawRect(1, 1, 6, 6);
		gg.setColor(Colours[rng.nextInt(10)]);
		gg.fillRect(1, 1, 6, 6);
		gg.setColor(Colours[rng.nextInt(10)]);
		gg.fillRect(4, 4, 3, 3);
		TexturePaint Plaid = new TexturePaint(buffImage,new Rectangle(10,10));
		return Plaid;
	}
	
	public void nextShape(Point p)
	{
		int shapeIndex = rng.nextInt(4);
		double ySpeed = rng.nextInt(11)/10.0;
		double xSpeed = Math.sqrt(1-(ySpeed*ySpeed));
		if(rng.nextInt(2)==0)
		{
			if(rng.nextInt(2)==0)
			{
				ySpeed=ySpeed*-1;
			}
			else if(rng.nextInt(2)==0)
			{
				xSpeed = xSpeed*-1;
			}
		}
		if(shapeIndex == 0)
		{
		Circle aCircle = new Circle(rng.nextInt(41)+20,p.getX(),p.getY(),xSpeed,ySpeed,Colours[rng.nextInt(10)]);
		if(aCircle.getRadius()+aCircle.getCenterx() >= this.getWidth())
		{
			aCircle.setCenterx(aCircle.getCenterx()-(aCircle.getRadius()-(this.getWidth()-aCircle.getCenterx())));
		}
		else if(aCircle.getRadius()+aCircle.getCentery() >= this.getHeight())
		{
			aCircle.setCentery(aCircle.getCentery()-(aCircle.getRadius()-(this.getHeight()-aCircle.getCentery())));
		}
		
		if(aCircle.getCenterx()-aCircle.getRadius() <= 0)
		{
			aCircle.setCenterx(aCircle.getCenterx()+(0-(aCircle.getCenterx()-aCircle.getRadius())));
		}
		else if(aCircle.getCentery()-aCircle.getRadius() <= 0)
		{
			aCircle.setCentery(aCircle.getCentery()+(0-(aCircle.getCentery()-aCircle.getRadius())));
		}
		shapesArray.add(aCircle);
		}
		else if(shapeIndex == 1)
		{
			int triangleDimensions = rng.nextInt(81)+20;
			Triangle aTriangle = new Triangle(triangleDimensions,
					triangleDimensions,
					p.getX(),
					p.getY(),
					triangleDimensions-(Math.sin(60)*triangleDimensions)-(2/3*(Math.sin(60)*triangleDimensions))-0.75*triangleDimensions,
					xSpeed,
					ySpeed,
					new GradientPaint(5,30,Colours[rng.nextInt(10)],35,100,Colours[rng.nextInt(10)],true));
			if(aTriangle.getRadius()+aTriangle.getCenterx() >= this.getWidth())
			{
				aTriangle.setCenterx(aTriangle.getCenterx()-(aTriangle.getRadius()-(this.getWidth()-aTriangle.getCenterx())));
			}
			else if(aTriangle.getRadius()+aTriangle.getCentery() >= this.getHeight())
			{
				aTriangle.setCentery(aTriangle.getCentery()-(aTriangle.getRadius()-(this.getHeight()-aTriangle.getCentery())));
			}
			
			if(aTriangle.getCenterx()-aTriangle.getRadius() <= 0)
			{
				aTriangle.setCenterx(aTriangle.getCenterx()+(0-(aTriangle.getCenterx()-aTriangle.getRadius())));
			}
			else if(aTriangle.getCentery()-aTriangle.getRadius() <= 0)
			{
				aTriangle.setCentery(aTriangle.getCentery()+(0-(aTriangle.getCentery()-aTriangle.getRadius())));
			}
			shapesArray.add(aTriangle);
		}
		else if(shapeIndex==2)
		{
			SSRectangle aRectangle = new SSRectangle(rng.nextInt(81)+20,rng.nextInt(81)+20,p.getX(),p.getY(),xSpeed,ySpeed,makePlaid());
			if((aRectangle.getTopLeftx()+aRectangle.getWidth()) >=this.getWidth())
			{
				aRectangle.setWidth(this.getWidth()-aRectangle.getTopLeftx());
			}
			else if((aRectangle.getTopLefty()+aRectangle.getHeight()) >=this.getHeight())
			{
				aRectangle.setHeight(this.getHeight()-aRectangle.getTopLefty());
			}
			
			if(aRectangle.getTopLeftx()-1 <=0)
			{
				aRectangle.setTopLeftx(0);
			}
			else if(aRectangle.getTopLefty()-1 <=0)
			{
				aRectangle.setTopLefty(0);
			}
			
			shapesArray.add(aRectangle);
		}
		else if(shapeIndex==3)
		{
			Cross aCross = new Cross(rng.nextInt(81)+20,rng.nextInt(81)+20,p.getX(),p.getY(),xSpeed,ySpeed);
			if((aCross.getTopLeftx()+aCross.getWidth()) >=this.getWidth())
			{
				aCross.setWidth(this.getWidth()-aCross.getTopLeftx());
			}
			else if((aCross.getTopLefty()+aCross.getHeight()) >=this.getHeight())
			{
				aCross.setHeight(this.getHeight()-aCross.getTopLefty());
			}
			
			if(aCross.getTopLeftx() -1<=0)
			{
				aCross.setTopLeftx(0);
			}
			else if(aCross.getTopLefty() -1<=0)
			{
				aCross.setTopLefty(0);
			}
			shapesArray.add(aCross);
		}
	}
	
	
	
	
	
	
}
