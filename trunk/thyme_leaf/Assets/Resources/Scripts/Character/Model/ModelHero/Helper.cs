using UnityEngine;
using System.Collections;

public class Helper
{
	public GameObject currentUnit;
	public Hero current_hero;
	
	//moving value
	private MoveModeState moveMode;
	public GameObject nodeStock;
	public scriptPathNode nodeInfor;
//	private string current_node_name;
	
	private bool enableMove;
	private bool enalbeMuster;
	
	//attacking value
	public float attack_delay_counter;
	public Hero attack_target;
	//==================================
	//extra
	public bool selectTurnoffRoot;
	
	public string old_name;
	public Vector3 gesture_startpoint;
	public Vector3 gesture_endpoint;
	public Vector3 oldpos;
	
	public float angle_calculation_rate;
	//==================================
	
	//collision range
	public float collision_range;
	//==================================
	
	public Helper(GameObject obj)
	{
		currentUnit = obj;
		current_hero = obj.GetComponent<Hero> ();
		
		oldpos = getPos ();
		
		moveMode = MoveModeState.FORWARD;
		
		enableMove = false;
		
		//======================
		//extra value
		selectTurnoffRoot = false;
		
		gesture_startpoint = gesture_endpoint = Vector3.zero;
		
		attack_target = null;
		//=======================
		
		attack_delay_counter = 0;
		angle_calculation_rate = 0;
	}
	
	public float CurrentAngle()
	{
		Vector3 d = getPos () - oldpos;
		oldpos = getPos ();
		return Mathf.Atan2 (d.y, d.x) * Define.RadianToAngle ();
	}
	
	public int Current_Right_orLeft()
	{
		//-1 = left, 1 = right
		float d = getPos ().x - oldpos.x;
		if(d < 3 && d > -3) return 0;
		else return d <= 0 ? -1 : 1;
	}
	
	public void StartPointSetting(StartPoint option)
	{
		for(int i=0;i<Define.pathNode.Count;i++)
		{
			scriptPathNode tempFunc = Define.pathNode[i].obj.GetComponent<scriptPathNode>();
			if(option == StartPoint.AUTOMAT_POINT)
			{ 
				if(tempFunc.automatPoint) nodeStock = Define.pathNode[i].obj;
			}
			else if(option == StartPoint.TROVANT_POINT)
			{
				if(tempFunc.trovantPoint) nodeStock = Define.pathNode[i].obj;
			}
		}
		nodeInfor = nodeStock.GetComponent<scriptPathNode>();
		
		setPos (nodeInfor.getPos (PosParamOption.CURRENT));
		
		if(nodeInfor.Prev != null) SetMoveMode(MoveModeState.BACKWARD);
		else if(nodeInfor.Next != null) SetMoveMode(MoveModeState.FORWARD);
	}
	
	public bool SelectPathNode(Vector3 startPt, Vector3 endPt, Layer option)
	{
		return SelectPathNode(startPt, endPt, option, FindingNodeDefaultOption.NORMAL);
	}
	
	public bool SelectPathNode(Vector3 startPt, Vector3 endPt, Layer option, FindingNodeDefaultOption default_auto)
	{
		Vector3 start_pt = startPt;
		Vector3 end_pt = endPt;
		
		if(nodeStock == null) return false;
		
		scriptPathNode tempFunc = nodeStock.GetComponent<scriptPathNode>();
		if(!tempFunc.TurnoffRoot)
		{
			return false;
		}
		Vector3 centerPoint = nodeStock.transform.localPosition;

		if(option == Layer.Trovant || default_auto == FindingNodeDefaultOption.RANDOM_NODE)
		{
			start_pt = getPos();
			Random.seed = 1; //test code...
			int rand_idx = Random.Range(0, tempFunc.CountTurnOffList());
            Debug.Log("RANDOM INDEX :: "+rand_idx);

			//Re edit...
//			Vector3 my_dir = 0;
//			while(true)
//			{
//				int idx = Define.random_index_table.getRandomIndex();
//				if(idx > tempFunc.CountTurnOffList()) continue;
//				if(Vector3.Dot(start_pt, tempFunc.getPosTurnoffList(idx)) < 0) continue;
//				else
//				{
//					end_pt = tempFunc.getPosTurnoffList(idx);
//					break;
//				}
//			}

			end_pt = tempFunc.getPosTurnoffList(rand_idx);

			current_hero.helper.gesture_startpoint = start_pt;
			current_hero.helper.gesture_endpoint = end_pt;
		}
		
//		if(option != Layer.Trovant && default_auto == FindingNodeDefaultOption.NORMAL)
//		{
//			Collider2D coll = RaycastHittingObject (start_pt);
//			Debug.Log(coll == null ? "null" : coll.name);
//			//test code
//			bool like_check = false;
//			if(coll == null) return false;
//			else
//			{
//				string i_am = currentUnit.GetComponent<Hero>().model.Name;
//				if(i_am == coll.gameObject.GetComponent<Hero>().model.Name)
//					like_check = true;
//			}
//			if(!like_check) return false;
//		}

		float dx = end_pt.x - start_pt.x;
		float dy = end_pt.y - start_pt.y;
		float ag = Mathf.Atan2 (dy, dx) * Define.RadianToAngle ();
//		Debug.Log ("angle : " + ag);
		
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
		
		if (min_idx < 0 || min_r > 70) return false;
		
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
			//	Debug.Log("turnoff Root next null");
			if(tempFunc.Prev != null)
			{
				moveMode = MoveModeState.BACKWARD;
				nodeStock = tempObj;
			}
			else
			{
				//		Debug.Log("turnoff Root null error");
				return false;
			}
		}
		
		//selected node get component
		nodeInfor = nodeStock.GetComponent<scriptPathNode>();
		selectTurnoffRoot = false;

		return true;
	}

	public void SelectNodeFinding(Vector3 start_pt, Vector3 end_pt)
	{
		if(!isGesture()) return;

		scriptPathNode tempFunc = nodeStock.GetComponent<scriptPathNode>();
		if(!tempFunc.TurnoffRoot) return;
		
		Vector3 centerPoint = nodeStock.transform.localPosition;

		float dx = end_pt.x - start_pt.x;
		float dy = end_pt.y - start_pt.y;
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
		
		if (min_idx < 0 || min_r > 40) return;
		
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
			//	Debug.Log("turnoff Root next null");
			if(tempFunc.Prev != null)
			{
				moveMode = MoveModeState.BACKWARD;
				nodeStock = tempObj;
			}
			else
			{
				//		Debug.Log("turnoff Root null error");
				return;
			}
		}
		
		//selected node get component
		nodeInfor = nodeStock.GetComponent<scriptPathNode>();
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
	
	public void setMusterTrigger(bool v) { enalbeMuster = v; }
	public bool getMusterTrigger() { return enalbeMuster; }
	
	public bool isGesture() { return selectTurnoffRoot; }
	
	public string getCurrentNodeName() { return nodeInfor == null ? "null" : nodeInfor.name; }
	
}
