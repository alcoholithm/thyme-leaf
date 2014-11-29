using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class pathFinder : MonoBehaviour 
{
	private MoveModeState moveMode;
	private float radian;
	private GameObject nodeCurrent;
	private GameObject nodeStock;
	private scriptPathNode nodeInfor;
	private bool enableMove;

	public float speed = 1;

	private string nameID;

	private string musterID;
	private bool PinPoint;
	private bool musterSet;

	//====
	private Vector3 mPointStart, mPointEnd;
	private bool selectTurnoffRoot;
	//====

	//identity value
	private float fx, fy;

	void Start () 
	{
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
		transform.localPosition = nodeInfor.getPos(PosParamOption.CURRENT);
		nodeStock = nodeInfor.Next;    //next point
		nodeInfor = nodeStock.GetComponent<scriptPathNode>();
		EnableMove ();

		//======================
		mPointStart = Vector3.zero;
		mPointEnd = Vector3.zero;

		selectTurnoffRoot = false;

		PinPoint = false;
		musterSet = false;
	}

	void Update () 
	{
		FindCheckNode ();
	}

	private void FindCheckNode()
	{
		float dx = nodeInfor.getPos(PosParamOption.CURRENT).x - getPos().x;
		float dy = nodeInfor.getPos(PosParamOption.CURRENT).y - getPos().y;

		if(enableMove)
		{
			float d = dx * dx + dy * dy;
			if(d < 10) //checking range
			{
				if(moveMode == MoveModeState.FORWARD)
				{
					if(nodeInfor.Next != null)
					{
						nodeStock = nodeInfor.Next;
					}
					else
					{
						Debug.Log("next null");
						//turnoff root true
						if(nodeInfor.turnoffBridge == null)
						{
							Debug.Log("turnoff null");
							if(nodeInfor.startPoint || nodeInfor.endPoint) MoveReverse();
							else nodeStock = nodeInfor.turnoffList[0].GetComponent<scriptPathNode>().turnoffBridge;
						}
						else
						{
							nodeStock = nodeInfor.turnoffBridge;
						}
					}
				}
				else if(moveMode == MoveModeState.BACKWARD)
				{
					if(nodeInfor.Prev != null) 
					{
						nodeStock = nodeInfor.Prev;
					}
					else
					{
						Debug.Log("prev null");
						//turnoff root true
						if(nodeInfor.turnoffBridge == null)
						{
							Debug.Log("turnoff null");
							if(nodeInfor.startPoint || nodeInfor.endPoint) MoveReverse();
							else nodeStock = nodeInfor.turnoffList[0].GetComponent<scriptPathNode>().turnoffBridge;
						}
						else
						{
							nodeStock = nodeInfor.turnoffBridge;
						}
					}
				}

				if(nodeInfor.TurnoffRoot && !selectTurnoffRoot)
				{
					selectTurnoffRoot = true;
					DisableMove();
					//select case
					return;
				}
				else if(!nodeInfor.TurnoffRoot)
				{
					selectTurnoffRoot = false;
				}

				nodeInfor = nodeStock.GetComponent<scriptPathNode>();
			}
			//move module
			float sp = speed * Define.FrameControl();
			float rt = Mathf.Atan2(dy, dx);
			fx = Mathf.Cos(rt) * sp;
			fy = Mathf.Sin(rt) * sp;
			//addPos(xv, yv);
		}
		else
		{
			//change state
			//don't move
			if(selectTurnoffRoot)
			{
				if(Input.GetMouseButtonDown(0))
				{
					mPointStart = ToScreenCoord(Input.mousePosition);
				}
				else if(Input.GetMouseButtonUp(0))
				{
					mPointEnd = ToScreenCoord(Input.mousePosition);
					if(SelectPathNode(mPointStart, mPointEnd))
					{
						Debug.Log(nodeStock);
						EnableMove();
					}
				}
				float value = (0.8f * Define.FrameControl());
				Debug.Log(value);
				fx *= value;
				fy *= value;
			}
		}
		addPos(fx, fy);
	}

	public bool SelectPathNode(Vector3 startPt, Vector3 endPt)
	{
		//turnoffRoot = nodestoke  <all like>
		//select path node ... when unit arrive at turnoff point
		//searching node
		if(nodeStock == null) return false;

		scriptPathNode tempFunc = nodeStock.GetComponent<scriptPathNode>();
		Vector3 centerPoint = nodeStock.transform.localPosition;

		float dx = endPt.x - startPt.x;
		float dy = endPt.y - startPt.y;
		float ag = Mathf.Atan2 (dy, dx) * Define.RadianToAngle ();

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

		if (min_idx < 0 || min_r > 30) return false;

		//=========select node part
		GameObject tempObj = tempFunc.turnoffList [min_idx];
		tempFunc = tempObj.GetComponent<scriptPathNode> ();
		if (tempFunc.Next != null) 
		{
			//if selected node is not null
			SetMoveMode(MoveModeState.FORWARD);
			nodeStock = tempObj;
		}
		else
		{
			Debug.Log("turnoff Root next null");
			if(tempFunc.Prev != null)
			{
				SetMoveMode(MoveModeState.BACKWARD);
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
		transform.localRotation = Quaternion.Euler(0,0,radian);
	}

	public float getAngle()
	{
		return radian * Define.RadianToAngle();
	}

	public void setPos(float x, float y, float z)
	{
		transform.localPosition = new Vector3(x, y, z);
	}

	public void setPos(Vector3 v)
	{
		transform.localPosition = v;
	}

	public void addPos(float x, float y)
	{
		transform.localPosition += new Vector3(x, y);
	}

	public void addPos(Vector3 v)
	{
		transform.localPosition += v;
	}

	public Vector3 getPos()
	{
		return transform.localPosition;
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
