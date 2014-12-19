using UnityEngine;
using System.Collections;

public class ModelHero : AutomartUnit
{
	//damage...
	private int criticalDamage;
	private int criticalProbability;
	
	//attack...
	private int attackDelay;
	private int attackDamage;
	private int attackRange;

	private Helper _helper;

	public ModelHero(Helper helper)
	{
		radian = 0;
		_helper = helper;
	}

	public void setAttackDelay(int v) { attackDelay = v; }
	public int getAttackDelay() { return attackDelay; }
	
	public void setAttackDamage(int v) { this.attackDamage = v; }
	public int getAttackDamage() { return this.attackDamage; }
	
	public void setAttackRange(int v) { this.attackRange = v; }
	public int getAttackRange() { return this.attackRange; }

	public override void setMaxHp(int v) { maxHp = v; }
	public override int getMaxHp() { return maxHp; }
	
	public override void setHp(int v) { curHp = v; }
	public override void addHp(int v) 
	{
		curHp += v;
		if(maxHp <= curHp) curHp = maxHp;
	}
	public override int getHp() { return curHp; }
	
	public override void setName(string v) { name = v; }
	public override string getName() {	return name; }
	
	public override void setDefencePoint(int v) { defence_point = v; }
	public override int getDefencePoint() { return defence_point; }
	
	public override void setType(UnitType option) { type = option; }
	public override UnitType getType() {	return type; }

	public override void setSpeed(float v) { speed = v; }
	public override float getSpeed() { return speed; }
	
	public override void setAngle(float ang) { radian = Define.AngleToRadian() * ang; }
	public override float getAngle() { return radian * Define.RadianToAngle(); }
	
	public override void setMusterID(string v) { musterID = v; }
	public override string getMusterID() { return musterID; }
	
	public override void setMoveOffset(float x, float y) { node_offset.Set (x, y); }
	public override Vector2 getMoveOffset() { return node_offset; }
}
