using UnityEngine;
using System.Collections;

public class ControllerUnit
{
	private ModelUnit _model;
	private HelperUnit _helper;

	public ControllerUnit(ModelUnit model, HelperUnit helper)
	{
		_model = model;
		_helper = helper;
	}

	public void StartPointSetting(StartPoint option)
	{
		_helper.StartPointSetting (option);
	}

	public void MoveReverse() { _helper.MoveReverse (); }
	
	public void SetMoveMode(MoveModeState option) { _helper.SetMoveMode (option); }

	//radian value can't input
	public void setAngle(float ang) { _model.setAngle (ang); }

	public void setLayer(int v) { _helper.setLayer (v); }
	
	public void setPos(float x, float y, float z) { _helper.setPos (x, y, z); }
	public void setPos(Vector3 v) { _helper.setPos (v); }
	public void addPos(float x, float y) { _helper.addPos (x, y); }
	public void addPos(Vector3 v) { _helper.addPos (v); }
	
	public void setSpeed(float v) { _model.setSpeed (v); }

	public void setMaxHp(int v) { _model.setMaxHp (v); }
	public int getMaxHp() { return _model.getMaxHp (); }

	public void addHp(int v) { _model.addHp (v); }
	public void setHp(int v) { _model.setHp (v); }
	public int getHp() { return _model.getHp(); }
	
	public void setID(string v) { _model.setID (v); }
	
	public void setmusterID(string v) { _model.setmusterID (v); }

	public void setMoveTrigger(bool v) { _helper.setMoveTrigger (v); }

	public bool isGesture() { return _helper.isGesture (); }

	public void setMoveOffset(float x, float y) { _model.setMoveOffset (x, y); }

	public void setType(UnitType option) { _model.setType (option); }
}
