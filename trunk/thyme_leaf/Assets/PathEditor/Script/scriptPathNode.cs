using UnityEngine;
using System.Collections;

public class scriptPathNode : MonoBehaviour
{
	private Vector3 pos;
	public GameObject Next, Prev;
	public GameObject[] turnoffList;
	public GameObject turnoffBridge;

	private Vector3 DirPosToNext, DirPosToPrev, DirPosToTurnoff;
	private Vector3[] DirPosToTurnoffList;

	private int turnoffListIndex;
	private int id;
	public bool TurnoffRoot;
	public bool automatPoint, trovantPoint;
	private float dir_angle, dirTurnoff_angle;

	private UISprite uiSprite;

	public void DataInit()
	{
		pos = Vector3.zero;
		turnoffList = new GameObject[Define.TurnOffMaxCount()];
		turnoffListIndex = 0;
		id = -1;
		automatPoint = trovantPoint = false;
		TurnoffRoot = false;
		dir_angle = dirTurnoff_angle = 0;

		DirPosToNext = Vector3.zero;
		DirPosToPrev = Vector3.zero;
		DirPosToTurnoff = Vector3.zero;
		DirPosToTurnoffList = new Vector3[Define.TurnOffMaxCount()];
		for(int i=0;i<Define.TurnOffMaxCount();i++) 
			DirPosToTurnoffList[i] = Vector3.zero;

		Next = Prev = turnoffBridge = null;

		uiSprite = gameObject.GetComponent<UISprite> ();
	}
	
	public void AddTurnOffList(GameObject obj)
	{
		if (turnoffListIndex >= Define.TurnOffMaxCount()) return;

		bool chk = false;
		for(int i=0;i<turnoffListIndex;i++)
		{
			if(turnoffList[i] == obj)
			{
				chk = true;
				break;
			}
		}
		if(!chk) turnoffList[turnoffListIndex++] = obj;
	}

	public void SetTurnOffListCount(int idx)
	{
		turnoffListIndex = idx;
	}

	public int CountTurnOffList()
	{
		return turnoffListIndex;
	}
	
	public void ClearTurnOffList()
	{
		turnoffListIndex = 0;
	}
	
	public void setPos(float x, float y, float z)
	{
		pos.Set(x, y, z);
		transform.localPosition = pos;
	}
	
	public Vector3 getPos(PosParamOption option)
	{
		Vector3 temp = new Vector3();
		switch (option)
		{
		case PosParamOption.CURRENT:
			temp = transform.localPosition;
			break;
		case PosParamOption.NEXT:
			temp = Next.transform.localPosition;
			break;
		case PosParamOption.PREVIOUS:
			temp = Prev.transform.localPosition;
			break;
		}
		return temp;
	}

	public Vector3 getPosTurnoffList(int idx)
	{
		Vector3 temp = new Vector3();
		if(idx < Define.TurnOffMaxCount()) 
			temp = turnoffList[idx].transform.localPosition;
		return temp;
	}
	
	public void setID(int id)
	{
		this.id = id;
	}

	public int getID()
	{
		return id;
	}
	
	public void EnableStartPoint()
	{
		automatPoint = true;
		DisableEndPoint();
	}
	
	public void DisableStartPoint()
	{
		automatPoint = false;
	}
	
	public void EnableEndPoint()
	{
		trovantPoint = true;
		DisableStartPoint();
	}
	
	public void DisableEndPoint()
	{
		trovantPoint = false;
	}
	
	public void EnableTurnoffRoot()
	{
		DisableEndPoint();
		DisableStartPoint();
		TurnoffRoot = true;
	}
	
	public void DisableTurnoffRoot()
	{
		TurnoffRoot = false;
	}

	public void SwapSideNode()
	{
		GameObject temp = Next;
		Next = Prev;
		Prev = temp;
	}

	public void setAngle(float v, bool turnoffRoot)
	{
		Transform tempTrans;
		if(turnoffRoot){
			dirTurnoff_angle = v;
			tempTrans = transform.FindChild("directionMark_TurnoffRoot");
		}
		else
		{
			dir_angle = v;
			tempTrans = transform.FindChild("directionMark");
		}
		tempTrans.localRotation = Quaternion.Euler(0,0,v);
	}

	public float getAngle(bool turnoffRoot)
	{
		if(turnoffRoot) return dirTurnoff_angle;
		else return dir_angle;
	}

	public void autoAngle(PosParamOption option)
	{
		if(option == PosParamOption.CURRENT) return;
		float dx, dy;
		scriptPathNode tempFunc = null;
		if(option == PosParamOption.NEXT) tempFunc = Next.GetComponent<scriptPathNode>();
		if(option == PosParamOption.PREVIOUS) tempFunc = Prev.GetComponent<scriptPathNode>();
		else if(option == PosParamOption.TURNOFFROOT) tempFunc = turnoffBridge.GetComponent<scriptPathNode>();

		if(tempFunc == null) return;

		dx = tempFunc.getPos(PosParamOption.CURRENT).x - getPos(PosParamOption.CURRENT).x;
		dy = tempFunc.getPos(PosParamOption.CURRENT).y - getPos(PosParamOption.CURRENT).y;

		Transform tempTrans;
		float ang;
		if(option == PosParamOption.TURNOFFROOT)
		{
			dirTurnoff_angle = Mathf.Atan2(dy, dx) * Define.RadianToAngle();
			tempTrans = transform.FindChild("directionMark_TurnoffRoot");
			ang = dirTurnoff_angle;
		}
		else
		{
			dir_angle = Mathf.Atan2(dy, dx) * Define.RadianToAngle();
			tempTrans = transform.FindChild("directionMark");
			ang = dir_angle;
		}
		tempTrans.localRotation = Quaternion.Euler(0,0,ang);

		float L = 0.5f * Vector2.Distance(new Vector2(tempFunc.getPos(PosParamOption.CURRENT).x, tempFunc.getPos(PosParamOption.CURRENT).y),
		                 new Vector2(getPos(PosParamOption.CURRENT).x, getPos(PosParamOption.CURRENT).y));
		Vector2 tempVector = new Vector2(dx, dy);
		tempVector.Normalize();

		tempTrans.localPosition = new Vector3 (tempVector.x * L, tempVector.y * L, 0);
	}

	public void DirectionValue(DirectionOption option)
	{
		switch(option)
		{
		case DirectionOption.TO_NEXT:
			DirPosToNext = new Vector3(getPos(PosParamOption.NEXT).x - getPos(PosParamOption.CURRENT).x,
			                           getPos(PosParamOption.NEXT).y - getPos(PosParamOption.CURRENT).y);
			DirPosToNext.Normalize();
			break;
		case DirectionOption.TO_PREV:
			DirPosToPrev = new Vector3(getPos(PosParamOption.PREVIOUS).x - getPos(PosParamOption.CURRENT).x,
			                           getPos(PosParamOption.PREVIOUS).y - getPos(PosParamOption.CURRENT).y);
			DirPosToPrev.Normalize();
			break;
		case DirectionOption.TO_TURNROOT:
			DirPosToTurnoff = new Vector3(getPos(PosParamOption.TURNOFFROOT).x - getPos(PosParamOption.CURRENT).x,
			                              getPos(PosParamOption.TURNOFFROOT).y - getPos(PosParamOption.CURRENT).y);
			DirPosToTurnoff.Normalize();
			break;
		case DirectionOption.TO_TURNLIST:
			for(int i=0;i<turnoffListIndex;i++)
			{
				DirPosToTurnoffList[i] = 
					new Vector3(
						getPosTurnoffList(i).x - getPos(PosParamOption.CURRENT).x,
						getPosTurnoffList(i).y - getPos(PosParamOption.CURRENT).y
						);
				DirPosToTurnoffList[i].Normalize();
			}
			break;
		}
	}

	public Vector3 getDir(DirectionOption option)
	{
		switch(option)
		{
		case DirectionOption.TO_NEXT:
			return DirPosToNext;
		case DirectionOption.TO_PREV:
			return DirPosToPrev;
		case DirectionOption.TO_TURNROOT:
			return DirPosToTurnoff;
		}
		return new Vector3();
	}

	public Vector3[] getDirTurnoffList()
	{
		return DirPosToTurnoffList;
	}

	public void DirectionMarkEnable(bool turnoffRoot)
	{
		Transform tempTrans;
		if(turnoffRoot) tempTrans = transform.FindChild("directionMark_TurnoffRoot");
		else tempTrans = transform.FindChild("directionMark");
		tempTrans.gameObject.SetActive(true);
	}

	public void DirectionMarkDisable(bool turnoffRoot)
	{
		Transform tempTrans;
		if(turnoffRoot) tempTrans = transform.FindChild("directionMark_TurnoffRoot");
		else tempTrans = transform.FindChild("directionMark");
		tempTrans.gameObject.SetActive(false);
	}

	public void ChangeIMG(SpriteList option)
	{
		string str = "none";
		switch(option)
		{
		case SpriteList.TROVANT:
			str = "End_Point";
			break;
		case SpriteList.NORMAL:
			str = "Normal_Point";
			break;
		case SpriteList.AUTOMAT:
			str = "Start_Point";
			break;
		case SpriteList.TURNOFF:
			str = "TurnOff_Point";
			break;
		}
		gameObject.SetActive (false);
		uiSprite.spriteName = str;
		gameObject.SetActive (true);
	}

	public void SetVisialbe(bool trigger)
	{
		uiSprite.enabled = trigger;
	}
}
