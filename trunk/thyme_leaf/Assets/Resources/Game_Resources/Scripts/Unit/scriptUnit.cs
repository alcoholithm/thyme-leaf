using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class scriptUnit : MonoBehaviour 
{
	private MoveModeState moveMode;
	private float radian;
	private List<GameObject> plist;
	private GameObject nodeStock;
	private scriptPathNode nodeInfor;
	private bool enableMove;

	public float speed = 1;

	void Start () 
	{
		plist = Define.pathNode;
		moveMode = MoveModeState.FORWARD;
		radian = 0;

		for(int i=0;i<plist.Count;i++)
		{
			scriptPathNode tempFunc = plist[i].GetComponent<scriptPathNode>();
			if(tempFunc.startPoint)
			{
				nodeStock = plist[i];
			}
		}
		nodeInfor = nodeStock.GetComponent<scriptPathNode>();
		transform.localPosition = nodeInfor.getPos(PosParamOption.CURRENT);

		nodeStock = nodeInfor.Next;    //next point
		nodeInfor = nodeStock.GetComponent<scriptPathNode>();

		enableMove = false;
	}

	void Update () 
	{
		FindCheckNode();

		if(Input.GetMouseButtonDown(0))
		{
			Debug.Log("click");
			EnableMove();
		}
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
						if(nodeInfor.turnoffBridge != null)
						{
							nodeStock = nodeInfor.turnoffBridge;
						}
						else
						{
							Debug.Log("turnoff null");
							MoveReverse();
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
						if(nodeInfor.turnoffBridge != null)
						{
							nodeStock = nodeInfor.turnoffBridge;
						}
						else
						{
							Debug.Log("turnoff null");
							MoveReverse();
						}
					}
				}

				if(nodeInfor.TurnoffRoot)
				{
					DisableMove();
					//select case
					Debug.Log("exit");
					return;
				}

				nodeInfor = nodeStock.GetComponent<scriptPathNode>();
			}
			float rt = Mathf.Atan2(dy, dx);
			float xv = Mathf.Cos(rt) * (speed * Time.deltaTime);
			float yv = Mathf.Sin(rt) * (speed * Time.deltaTime);
			addPos(xv, yv);
		}
		else
		{
			//don't move
			//attack~!

		}
	}

	public void MoveReverse()
	{
		if(moveMode == MoveModeState.FORWARD)
			moveMode = MoveModeState.BACKWARD;
		else if(moveMode == MoveModeState.BACKWARD)
			moveMode = MoveModeState.FORWARD;

		if(moveMode == MoveModeState.FORWARD) nodeStock = nodeInfor.Next;
		else if(moveMode == MoveModeState.BACKWARD) nodeStock = nodeInfor.Prev;
	}

	public void SelectPathNode(GameObject gobj)
	{
		//select path node ... when unit arrive at turnoff point
		scriptPathNode tempFunc = gobj.GetComponent<scriptPathNode>();
		if(tempFunc.TurnoffRoot) return;
		
		if(tempFunc.Next == null) moveMode = MoveModeState.BACKWARD;
		else if(tempFunc.Prev == null) moveMode = MoveModeState.FORWARD;
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
		//rigidbody2D.MovePosition(rigidbody2D.position + new Vector2(x,y));
		transform.localPosition += new Vector3(x, y);
	}

	public void addPos(Vector2 v)
	{
		rigidbody2D.position += v;
	}

	public Vector3 getPos()
	{
		return transform.localPosition;
	}
}
