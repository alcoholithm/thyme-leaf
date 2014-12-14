using UnityEngine;
using System.Collections;

public abstract class AutomartUnit : Unit 
{
	protected float speed;

	//extra...
	protected float radian;
	protected string musterID;
	protected Vector2 node_offset;

	
	public abstract void setSpeed (float v);
	public abstract float getSpeed ();

	public abstract void setAngle (float ang);
	public abstract float getAngle ();

	public abstract void setMusterID (string v);
	public abstract string getMusterID ();
	
	public abstract void setMoveOffset (float x, float y);
	public abstract Vector2 getMoveOffset ();
}
