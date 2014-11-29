using UnityEngine;
using System.Collections;

public class ControllerUnit
{
	private ModelUnit _model;

	public ControllerUnit(ModelUnit model)
	{
		_model = model;
	}

	public void MoveReverse()
	{
		_model.MoveReverse ();
	}
	
	public void SetMoveMode(MoveModeState option)
	{
		_model.SetMoveMode (option);
	}
	
	public void EnableMove()
	{
		_model.EnableMove ();
	}
	
	public void DisableMove()
	{
		_model.DisableMove ();
	}
	
	public void setAngle(float ang) //radian value can't input
	{
		_model.setAngle (ang);
	}
	
	public void setPos(float x, float y, float z)
	{
		_model.setPos (x, y, z);
	}
	
	public void setPos(Vector3 v)
	{
		_model.setPos (v);
	}
	
	public void addPos(float x, float y)
	{
		_model.addPos (x, y);
	}
	
	public void addPos(Vector3 v)
	{
		_model.addPos (v);
	}
	
	public void setSpeed(float v)
	{
		_model.setSpeed (v);
	}
	
	public void setID(string v)
	{
		_model.setID (v);
	}
	
	public void setmusterID(string v)
	{
		_model.setmusterID (v);
	}
	
	public bool isMuster()
	{
		return _model.isMuster ();
	}
	
	public void EnableMuster()
	{
		_model.EnableMuster ();
	}
	
	public void DisableMuster()
	{
		_model.DisableMuster ();
	}
	
	public void EnablePinpoint()
	{
		_model.EnablePinpoint ();
	}
	
	public void DisablePinpoint()
	{
		_model.DisablePinpoint ();
	}

}
