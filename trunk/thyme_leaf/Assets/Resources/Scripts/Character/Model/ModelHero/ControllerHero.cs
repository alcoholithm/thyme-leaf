using UnityEngine;
using System.Collections;

public class ControllerHero
{
	private MHero _model;
	private Helper _helper;
	
	public ControllerHero(MHero model, Helper helper)
	{
		_model = model;
		_helper = helper;
	}
	
	//helper...
	public void setMoveTrigger(bool v) { _helper.setMoveTrigger(v); }
	
	public void setMusterTrigger(bool v) { _helper.setMusterTrigger (v); }
	
	public bool isGesture() { return _helper.isGesture(); }
	
	public void StartPointSetting(StartPoint option) { _helper.StartPointSetting(option); }
	
	public void MoveReverse() { _helper.MoveReverse(); }
	
	public void SetMoveMode(MoveModeState option) { _helper.SetMoveMode(option); }
	
	public void setPos(float x, float y, float z) { _helper.setPos(x, y, z); }
	public void setPos(Vector3 v) { _helper.setPos(v); }
	public void addPos(float x, float y) { _helper.addPos(x, y); }
	public void addPos(Vector3 v) { _helper.addPos(v); }
	
	//model...
	public void setAngle(float ang) { _model.Angle = ang; }
	
	public void setSpeed(float v) { _model.MovingSpeed = v; }
	
	public void setMaxHp(int v) { _model.MaxHP = v; }
	
	public void addHp(int v) 
	{
		_model.HP += v; 
		if(_model.HP <= 0) _model.HP = 0;
		else if(_model.HP >= _model.MaxHP) _model.HP = _model.MaxHP;
	}
	public void setHp(int v) { 
		_model.HP = v; 
		if(_model.HP <= 0) _model.HP = 0;
		else if(_model.HP >= _model.MaxHP) _model.HP = _model.MaxHP;
	}
	
	public void setName(string v) { _model.Name = v; }
	
	public void setSpeciesName(string v) { _model.SpecieName = v; }
	
	public void setMusterID(string v) { _model.MusterID = v; }
	
	public void setMusterLeaderTrigger(bool v) { _model.MusterLeader = v; }
	
	public void setType(UnitType option) { _model.Type = option; }

	public void setStateName(string state_name) { _model.StateName = state_name; }

	public void setAttackDelay(float v) { _model.AttackDelay = v; }

	public void setAttackDamage(float v) { _model.AttackDamage = v; }

	public void setAttackRange(float v) { _model.AttackRange = v; }

	public void setNodeOffsetStruct(OffsetStruct v) { _model.NodeOffsetStruct = v; }
}
