﻿using UnityEngine;
using System.Collections;

public class Helper
{
	public GameObject currentUnit;

	//moving value
	private MoveModeState moveMode;
	public GameObject nodeStock;
	public scriptPathNode nodeInfor;
	private bool enableMove;
	public Hero attack_target;

	//attacking value
	public float attack_delay_counter;

	//==================================
	//extra
	public bool selectTurnoffRoot;
	public Vector3 force;

	public Vector3 gesture_startpoint;
	public Vector2 gesture_endpoint;

	//private Layer current_layer;

	//collision range
	public float collision_range;
	//==================================

	public Helper(GameObject obj)
	{
		currentUnit = obj;
		
		moveMode = MoveModeState.FORWARD;

		enableMove = false;

		//======================
		//extra value
		force = Vector3.zero;
		selectTurnoffRoot = false;

		gesture_startpoint = gesture_endpoint = Vector3.zero;

		attack_target = null;
		//=======================

		attack_delay_counter = 0;
	}

	public void StartPointSetting(StartPoint option)
	{
		for(int i=0;i<Define.pathNode.Count;i++)
		{
			scriptPathNode tempFunc = Define.pathNode[i].GetComponent<scriptPathNode>();
			if(option == StartPoint.AUTOMART_POINT)
			{ 
				if(tempFunc.startPoint) nodeStock = Define.pathNode[i];
			}
			else if(option == StartPoint.TROVANT_POINT)
			{
				if(tempFunc.endPoint) nodeStock = Define.pathNode[i];
			}
		}
		nodeInfor = nodeStock.GetComponent<scriptPathNode>();

		setPos (nodeInfor.getPos (PosParamOption.CURRENT));

		if(nodeInfor.Prev != null) SetMoveMode(MoveModeState.BACKWARD);
		else if(nodeInfor.Next != null) SetMoveMode(MoveModeState.FORWARD);
	}

	public bool SelectPathNode(Vector3 startPt, Vector3 endPt)
	{
		//turnoffRoot = nodestoke  <all like>
		//select path node ... when unit arrive at turnoff point
		//searching node
		if(nodeStock == null) return false;

		Collider2D coll = RaycastHittingObject (startPt);
		//test code
		bool like_check = false;
		if(coll == null) return false;
		else
		{
			string i_am = currentUnit.GetComponent<Hero>().model.Name;
			if(i_am == coll.gameObject.GetComponent<Hero>().model.Name)
				like_check = true;
		}
		if(!like_check) return false;

		//layer checking...

		float dx = endPt.x - startPt.x;
		float dy = endPt.y - startPt.y;

		scriptPathNode tempFunc = nodeStock.GetComponent<scriptPathNode>();
		Vector3 centerPoint = nodeStock.transform.localPosition;

		float ag = Mathf.Atan2 (dy, dx) * Define.RadianToAngle ();
		Debug.Log ("angle : " + ag);

		int min_idx = -1;
		float min_r = float.MaxValue;
		for (int i=0; i<tempFunc.CountTurnOffList(); i++)
		{
			Vector3 turn_list = tempFunc.getPosTurnoffList(i);
			dx = turn_list.x - centerPoint.x;
			dy = turn_list.y - centerPoint.y;
			float r = ag - (Mathf.Atan2(dy, dx) * Define.RadianToAngle());
			r = r > 0 ? r : -r;
			if(r < min_r)
			{
				min_r = r;
				min_idx = i;
			}
		}
		
		if (min_idx < 0 || min_r > 40) return false;
		
		//=========select node part
		GameObject tempObj = tempFunc.turnoffList [min_idx];
		tempFunc = tempObj.GetComponent<scriptPathNode> ();
		if (tempFunc.Next != null) 
		{
			//if selected node is not null
			moveMode = MoveModeState.FORWARD;
			nodeStock = tempObj;
		}
		else
		{
			Debug.Log("turnoff Root next null");
			if(tempFunc.Prev != null)
			{
				moveMode = MoveModeState.BACKWARD;
				nodeStock = tempObj;
			}
			else
			{
				Debug.Log("turnoff Root null error");
				return false;
			}
		}
		
		//selected node get component
		nodeInfor = nodeStock.GetComponent<scriptPathNode>();
		
		return true;
	}

	public Collider2D RaycastHittingObject(Vector3 mouse_position)
	{
		Vector2 wp = UICamera.mainCamera.ScreenToWorldPoint(mouse_position);
		Ray2D ray = new Ray2D (wp, Vector2.zero);
		RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

		return hit.collider;
	}

	public void MoveReverse()
	{
		if(moveMode == MoveModeState.FORWARD)
			SetMoveMode(MoveModeState.BACKWARD);
		else if(moveMode == MoveModeState.BACKWARD)
			SetMoveMode(MoveModeState.FORWARD);
	}

//	public void setLayer(Layer v) { current_layer = v; }
//	public Layer getLayer() { return current_layer; }

	public void setPos(float x, float y, float z) { currentUnit.transform.localPosition = new Vector3(x, y, z); }
	public void setPos(Vector3 v) { currentUnit.transform.localPosition = v; }
	public void addPos(float x, float y) { currentUnit.transform.localPosition += new Vector3(x, y); }
	public void addPos(Vector3 v) { currentUnit.transform.localPosition += v; }
	public Vector3 getPos() { return currentUnit.transform.localPosition; }
	
	public void SetMoveMode(MoveModeState option)
	{
		moveMode = option;

		if(moveMode == MoveModeState.FORWARD) nodeStock = nodeInfor.Next;
		else if(moveMode == MoveModeState.BACKWARD) nodeStock = nodeInfor.Prev;

		nodeInfor = nodeStock.GetComponent<scriptPathNode>();
	}
	public MoveModeState GetMoveMode() { return moveMode; }
	
	public void setMoveTrigger(bool v) { enableMove = v; }
	public bool getMoveTrigger() { return enableMove; }

	public bool isGesture() { return selectTurnoffRoot; }

}
