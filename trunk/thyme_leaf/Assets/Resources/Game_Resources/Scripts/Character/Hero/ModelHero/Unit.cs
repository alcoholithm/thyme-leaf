using UnityEngine;
using System.Collections;

public abstract class Unit 
{
	protected string name;
	protected int maxHp;
	protected int curHp;
	protected int defence_point;
	
	protected UnitType type;

	public abstract void setMaxHp (int v);
	public abstract int getMaxHp ();

	public abstract void setHp (int v);
	public abstract void addHp (int v);
	public abstract int getHp ();
	
	public abstract void setName (string v);
	public abstract string getName ();
	
	public abstract void setDefencePoint (int v);
	public abstract int getDefencePoint ();
	
	public abstract void setType (UnitType option);
	public abstract UnitType getType ();
}
