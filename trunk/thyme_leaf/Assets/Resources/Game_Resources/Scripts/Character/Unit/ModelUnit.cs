using UnityEngine;
using System.Collections;

public class ModelUnit 
{
	private Transform currentUnit;

	private MoveModeState moveMode;
	private float radian;
	public GameObject nodeCurrent;
	public GameObject nodeStock;
	public scriptPathNode nodeInfor;
	private bool enableMove;
	
	private float speed;
	
	private string nameID;
	
	private string musterID;
	private bool PinPoint;
	private bool musterSet;

	public ModelUnit()
	{

	}

	public ModelUnit(ref Transform trans)
	{
		currentUnit = trans;

		moveMode = MoveModeState.FORWARD;
		radian = 0;
		
		for(int i=0;i<Define.pathNode.Count;i++)
		{
			scriptPathNode tempFunc = Define.pathNode[i].GetComponent<scriptPathNode>();
			if(tempFunc.startPoint)
			{
				nodeStock = Define.pathNode[i];
			}
		}
		nodeCurrent = nodeStock;
		
		nodeInfor = nodeStock.GetComponent<scriptPathNode>();
		currentUnit.localPosition = nodeInfor.getPos(PosParamOption.CURRENT);
		nodeStock = nodeInfor.Next;    //next point
		nodeInfor = nodeStock.GetComponent<scriptPathNode>();
		EnableMove ();
		
		//======================
		PinPoint = false;
		musterSet = false;

		speed = 1;
	}

	public void MoveReverse()
	{
		if(moveMode == MoveModeState.FORWARD)
			SetMoveMode(MoveModeState.BACKWARD);
		else if(moveMode == MoveModeState.BACKWARD)
			SetMoveMode(MoveModeState.FORWARD);
		
		if(moveMode == MoveModeState.FORWARD) nodeStock = nodeInfor.Next;
		else if(moveMode == MoveModeState.BACKWARD) nodeStock = nodeInfor.Prev;
	}
	
	public void SetMoveMode(MoveModeState option)
	{
		moveMode = option;
	}
	
	public MoveModeState GetMoveMode()
	{
		return moveMode;
	}
	
	private Vector3 ToScreenCoord(Vector3 v)
	{
		Vector3 temp = UICamera.mainCamera.ScreenToWorldPoint(v);
		return UICamera.mainCamera.WorldToScreenPoint (temp);
	}
	
	public void EnableMove()
	{
		enableMove = true;
	}
	
	public void DisableMove()
	{
		enableMove = false;
	}
	
	public void setAngle(float ang) //radian value can't input
	{
		radian = Define.AngleToRadian() * ang;
		currentUnit.localRotation = Quaternion.Euler(0,0,radian);
	}
	
	public float getAngle()
	{
		return radian * Define.RadianToAngle();
	}
	
	public void setPos(float x, float y, float z)
	{
		currentUnit.localPosition = new Vector3(x, y, z);
	}
	
	public void setPos(Vector3 v)
	{
		currentUnit.localPosition = v;
	}
	
	public void addPos(float x, float y)
	{
		currentUnit.localPosition += new Vector3(x, y);
	}
	
	public void addPos(Vector3 v)
	{
		currentUnit.localPosition += v;
	}
	
	public Vector3 getPos()
	{
		return currentUnit.localPosition;
	}

	public void setSpeed(float v)
	{
		speed = v;
	}

	public float getSpeed()
	{
		return speed;
	}
	
	public void setID(string v)
	{
		nameID = v;
	}
	
	public string getID()
	{
		return nameID;
	}
	
	public void setmusterID(string v)
	{
		musterID = v;
	}
	
	public string getmusterID()
	{
		return musterID;
	}
	
	public bool isMuster()
	{
		return musterSet;
	}
	
	public void EnableMuster()
	{
		musterSet = true;
	}
	
	public void DisableMuster()
	{
		musterSet = false;
	}
	
	public void EnablePinpoint()
	{
		PinPoint = true;
	}
	
	public void DisablePinpoint()
	{
		PinPoint = false;
	}
	
	public void RemoveUnit() //???
	{
		//disconstructure
	}
}
