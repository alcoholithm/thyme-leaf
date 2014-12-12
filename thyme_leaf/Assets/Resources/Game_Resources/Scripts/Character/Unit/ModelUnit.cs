using UnityEngine;
using System.Collections;

public class ModelUnit
{
	private float radian;
	private float speed;
	private string nameID;
	private string musterID;
	private UnitType type;

	private int maxhp;
	private int hp;

	private Vector2 node_offset;

	private HelperUnit _helper;

	public ModelUnit(HelperUnit helper)
	{
		radian = 0;
		_helper = helper;
	}

	public void setAngle(float ang) //radian value can't input
	{
		radian = Define.AngleToRadian() * ang;
		_helper.currentUnit.transform.localRotation = Quaternion.Euler(0,0,ang);
	}
	public float getAngle() { return radian * Define.RadianToAngle(); }

	public void setMaxHp(int v) { maxhp = v; }
	public int getMaxHp() { return maxhp; }

	public void setHp(int v) { hp = v; }
	public void addHp(int v) 
	{
		hp += v;
		if(maxhp <= hp) hp = maxhp;
	}
	public int getHp() { return hp; }

	public void setSpeed(float v) { speed = v; }
	public float getSpeed() { return speed; }
	
	public void setID(string v) { nameID = v; }
	public string getID() {	return nameID; }
	
	public void setmusterID(string v) { musterID = v; }
	public string getmusterID() { return musterID; }

	public void setMoveOffset(float x, float y) { node_offset.Set (x, y); }
	public Vector2 getMoveOffset() { return node_offset; }

	public void setType(UnitType option) { type = option; }
	public UnitType getType() {	return type; }
}
