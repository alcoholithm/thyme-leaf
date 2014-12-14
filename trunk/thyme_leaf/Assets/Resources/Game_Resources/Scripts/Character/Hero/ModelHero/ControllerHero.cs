using UnityEngine;
using System.Collections;

public class ControllerHero
{
	private ModelHero _model;
	private Helper _helper;

	public ControllerHero(ModelHero model, Helper helper)
	{
		_model = model;
		_helper = helper;
	}

	//helper...
	public void setMoveTrigger(bool v) { _helper.setMoveTrigger (v); }
	
	public bool isGesture() { return _helper.isGesture (); }

	public void StartPointSetting(StartPoint option) { _helper.StartPointSetting (option); }

	public void MoveReverse() { _helper.MoveReverse (); }
	
	public void SetMoveMode(MoveModeState option) { _helper.SetMoveMode (option); }

	public void setLayer(int v) { _helper.setLayer (v); }
	
	public void setPos(float x, float y, float z) { _helper.setPos (x, y, z); }
	public void setPos(Vector3 v) { _helper.setPos (v); }
	public void addPos(float x, float y) { _helper.addPos (x, y); }
	public void addPos(Vector3 v) { _helper.addPos (v); }

	//model...
	public void setAngle(float ang) { _model.setAngle (ang); }

	public void setSpeed(float v) { _model.setSpeed (v); }

	public void setMaxHp(int v) { _model.setMaxHp (v); }

	public void addHp(int v) { _model.addHp (v); }
	public void setHp(int v) { _model.setHp (v); }
	
	public void setID(string v) { _model.setName (v); }
	
	public void setmusterID(string v) { _model.setMusterID (v); }

	public void setMoveOffset(float x, float y) { _model.setMoveOffset (x, y); }

	public void setType(UnitType option) { _model.setType (option); }
}
