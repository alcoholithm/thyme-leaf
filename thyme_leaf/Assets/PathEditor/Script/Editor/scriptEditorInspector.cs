﻿using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(scriptEditor))]
public class scriptEditorInspector : Editor
{
	scriptEditor editor;
	public override void OnInspectorGUI()
	{
		editor = target as scriptEditor;

		editor.pathNodeObj = (GameObject)EditorGUILayout.ObjectField("Node Object", editor.pathNodeObj, typeof(GameObject), true);
		editor.curentState = (EditorState)EditorGUILayout.EnumPopup("Work List",editor.curentState);

		GUILayout.Space(10);
		editor.mode = (GameMode)EditorGUILayout.EnumPopup ("Game Mode", editor.mode);

		GUILayout.Space(10);
		editor.StageNumber = EditorGUILayout.IntField("Stage Number", editor.StageNumber);

		GUILayout.Space(15);
		if(GUILayout.Button("Initialize"))
		{
			editor.NodeNULL = null;
			editor.pathList = new List<GameObject>();
			editor.productID = -1;
			editor.selectedID = -1;
			editor.selectedEventUP = -1;  //mouse up event
			editor.selectedRemove = -1;
		}

//		GUILayout.Space(25);
//		if(GUILayout.Button("Done"))
//		{
//			pathComplete();
//		}

		GUILayout.Space(25);
		if(GUILayout.Button("Scene Load Data"))
		{
			LoadNode();
		}

		GUILayout.Space(25);
		if(GUILayout.Button("ToFile"))
		{
			pathComplete();
			dataToFile();
		}
	}

	public void dataToFile()
	{
		int n = editor.StageNumber;
		if(editor.pathList.Count <= 0) return;
		if(editor.mode == GameMode.MULTI_PLAY) n = editor.StageNumber + 99999;
		DataToFile.SavaData(ref editor.pathList, n);
	}

	public void pathComplete()
	{
		if(editor.pathList.Count <= 0) return;
		for(int i=0;i<editor.pathList.Count;i++)
		{
			scriptPathNode tempFunc = editor.pathList[i].GetComponent<scriptPathNode>();
			if(tempFunc.Next != null)
			{
				tempFunc.DirectionValue(DirectionOption.TO_NEXT);
			}
			if(tempFunc.Prev != null)
			{
				tempFunc.DirectionValue(DirectionOption.TO_PREV);
			}
			if(tempFunc.turnoffBridge != null)
			{
				tempFunc.DirectionValue(DirectionOption.TO_TURNROOT);
			}
			if(tempFunc.TurnoffRoot)
			{
				tempFunc.DirectionValue(DirectionOption.TO_TURNLIST);
			}
		}
	}

	public void LoadNode()
	{
		GameObject PathNodeRoot_obj = GameObject.Find ("PathNodeRoot");
		int child_count = PathNodeRoot_obj.transform.childCount;
		if(child_count <= 0)
		{
			Debug.Log("No Data");
		}
		for(int i=0;i<child_count;i++)
		{
			editor.productID++;
			GameObject func_obj = PathNodeRoot_obj.transform.GetChild(i).gameObject;
			scriptPathNode tempFunc = func_obj.GetComponent<scriptPathNode>();
			Vector3 pos = func_obj.transform.localPosition;
			tempFunc.DataInit();
			tempFunc.setPos(pos.x, pos.y, 0);
			tempFunc.setID(editor.productID);

			editor.pathList.Add(func_obj);
		}
	}

	public void AddNode(float x, float y, float z)  //활성화 되어있을때만 추가한다.
	{
		editor.productID++;

		GameObject obj = Instantiate(editor.pathNodeObj) as GameObject;
		obj.transform.parent = GameObject.Find("PathNodeRoot").transform;
		obj.transform.position = new Vector3(x, y, z);
		obj.transform.localScale = new Vector3 (1, 1, 1);
		obj.name = "node"+editor.productID;

		scriptPathNode tempObj = obj.gameObject.GetComponent<scriptPathNode>();
		tempObj.DataInit();
		tempObj.setPos(x, y, z);
		tempObj.setID(editor.productID);

		editor.pathList.Add(obj);
	}

	public void RemoveNode(ref Event e)
	{
		if(PickModule(ref e, false))
		{
			Debug.Log("node : " + editor.selectedID);
			DestroyImmediate(GameObject.Find("node"+editor.selectedRemove));
			editor.pathList.RemoveAt(editor.selectedID);
			Debug.Log("count : " + editor.pathList.Count);
		}
	}
	
	public void ClearPathList()
	{
		editor.pathList.Clear();
		editor.productID = -1;
	}

	public void SearchNode(int compareID, bool connectMode)
	{
		Debug.Log ("id = " + compareID);
		int id = compareID;
		for(int i=0;i<editor.pathList.Count;i++)
		{
			scriptPathNode tempFunc = editor.pathList[i].GetComponent<scriptPathNode>();
			if(id == tempFunc.getID())
			{
				editor.selectedRemove = tempFunc.getID();
				if(!connectMode) editor.selectedID = i;
				else editor.selectedEventUP = i;
				break;
			}
		}

	}

	public void ConnectNode()
	{
		if (editor.selectedID < 0 || editor.selectedEventUP < 0) return;
		
		int from = editor.selectedID;
		int to = editor.selectedEventUP;
		scriptPathNode fromFunc1 = editor.pathList[from].GetComponent<scriptPathNode>();
		scriptPathNode ToFunc2 = editor.pathList[to].GetComponent<scriptPathNode>();

		if (fromFunc1.TurnoffRoot && !ToFunc2.TurnoffRoot)
		{
			GameObject temp = editor.pathList[from];
			scriptPathNode tempFunc = temp.GetComponent<scriptPathNode>();
			tempFunc.AddTurnOffList(editor.pathList[to]);
			editor.pathList[from] = temp;

			temp = editor.pathList[to];
			tempFunc = temp.GetComponent<scriptPathNode>();
			tempFunc.turnoffBridge = editor.pathList[from];
			editor.pathList[to] = temp;
			tempFunc.autoAngle(PosParamOption.TURNOFFROOT);
			tempFunc.DirectionMarkEnable(true);
		}
		else if (fromFunc1.TurnoffRoot && ToFunc2.TurnoffRoot)
		{
			GameObject temp = editor.pathList[to];
			scriptPathNode tempFunc = temp.GetComponent<scriptPathNode>();
			tempFunc.AddTurnOffList(editor.pathList[from]);
			editor.pathList[to] = temp;
			
			temp = editor.pathList[from];
			tempFunc = temp.GetComponent<scriptPathNode>();
			tempFunc.AddTurnOffList(editor.pathList[to]);
			editor.pathList[from] = temp;
		}
		else if(!fromFunc1.TurnoffRoot && !ToFunc2.TurnoffRoot)
		{
			GameObject temp = editor.pathList[from];
			scriptPathNode tempFunc = temp.GetComponent<scriptPathNode>();
			tempFunc.Next = editor.pathList[to];
			editor.pathList[from] = temp;
			tempFunc.autoAngle(PosParamOption.NEXT);
			tempFunc.DirectionMarkEnable(false);
			
			temp = editor.pathList[to];
			tempFunc = temp.GetComponent<scriptPathNode>();
			tempFunc.Prev = editor.pathList[from];
			editor.pathList[to] = temp;
		}
	}

	public void ResetNodeInfor(int idx)
	{
		if (idx < 0) return;
		
		GameObject temp = editor.pathList[idx];
		scriptPathNode tempFunc = temp.GetComponent<scriptPathNode>();
		tempFunc.Next = null;
		tempFunc.Prev = null;
		tempFunc.turnoffBridge = null;
		tempFunc.DisableEndPoint();
		tempFunc.DisableStartPoint();
		tempFunc.DisableTurnoffRoot();
		tempFunc.ClearTurnOffList();
		tempFunc.DirectionMarkDisable(false);
		tempFunc.setAngle(0.0f, false);
		tempFunc.DirectionMarkDisable(true);
		tempFunc.setAngle(0.0f, true);
		tempFunc.ChangeIMG(SpriteList.NORMAL);
		editor.pathList[editor.selectedID] = temp;
	}

	public void NodeModeSetting(bool enable, EnableNodeMode option, int idx)
	{
		if (idx < 0) return;

		scriptPathNode tempFunc = editor.pathList[idx].GetComponent<scriptPathNode>();
		if(enable)
		{
			switch(option)
			{
			case EnableNodeMode.AUTOMAT_NODE:
				tempFunc.EnableStartPoint();
				tempFunc.ChangeIMG(SpriteList.AUTOMAT);
				break;
			case EnableNodeMode.TROVANT_NODE:
				tempFunc.EnableEndPoint();
				tempFunc.ChangeIMG(SpriteList.TROVANT);
				break;
			case EnableNodeMode.TURNOFF_NODE:
				tempFunc.EnableTurnoffRoot();
				tempFunc.ChangeIMG(SpriteList.TURNOFF);
				break;
			}
		}
		else
		{
			switch(option)
			{
			case EnableNodeMode.AUTOMAT_NODE:
				tempFunc.DisableStartPoint();
				break;
			case EnableNodeMode.TROVANT_NODE:
				tempFunc.DisableEndPoint();
				break;
			case EnableNodeMode.TURNOFF_NODE:
				tempFunc.DisableTurnoffRoot();
				break;
			}
			tempFunc.ChangeIMG(SpriteList.NORMAL);
		}
	}

	public void OnSceneGUI()
	{
		if(Application.isPlaying) return;

		scriptEditor editor = target as scriptEditor;

		Handles.BeginGUI();
		if(GUI.Button(new Rect(10,10,100,30), "Clear Path List"))
		{
			if(GameObject.Find("PathNodeRoot").transform.childCount > 0)
			{
				ClearPathList();
				List<GameObject> gobjList = new List<GameObject>();
				foreach(Transform child in GameObject.Find("PathNodeRoot").transform)
				{
					gobjList.Add(child.gameObject);
				}
				for(int i=0;i<gobjList.Count;i++)
				{
					DestroyImmediate(gobjList[i]);					
				}
				gobjList.Clear();
				gobjList = null;
			}
		}
		else if(GUI.Button(new Rect(10,50,100,30), "Reset Node"))
		{
			if(editor.pathList.Count > 0) ResetNodeInfor(editor.selectedID);
		}
		else if(GUI.Button(new Rect(10,90,100,30), "Automat Point"))
		{
			if(editor.pathList.Count > 0) NodeModeSetting(true, EnableNodeMode.AUTOMAT_NODE, editor.selectedID);
		}
		else if(GUI.Button(new Rect(10,130,100,30), "Trovant Point"))
		{
			if(editor.pathList.Count > 0) NodeModeSetting(true, EnableNodeMode.TROVANT_NODE, editor.selectedID);
		}
		else if(GUI.Button(new Rect(10,170,100,30), "TurnOff Point"))
		{
			if(editor.pathList.Count > 0) NodeModeSetting(true, EnableNodeMode.TURNOFF_NODE, editor.selectedID);
		}
		else if(GUI.Button(new Rect(10,210,100,30), "Normal Point"))
		{
			if(editor.pathList.Count > 0)
			{
				NodeModeSetting(false, EnableNodeMode.AUTOMAT_NODE, editor.selectedID);
				NodeModeSetting(false, EnableNodeMode.TROVANT_NODE, editor.selectedID);
				NodeModeSetting(false, EnableNodeMode.TURNOFF_NODE, editor.selectedID);          
			}
		}
		Handles.EndGUI();

		int controlID = GUIUtility.GetControlID(FocusType.Passive);
		HandleUtility.AddDefaultControl(controlID);

		Event e = Event.current;

		if(e.type == EventType.MouseDown)
		{
			switch(editor.curentState)
			{
				case EditorState.ADD:
				AddNode(0.0f, 0.0f, 0.0f);
					break;
				case EditorState.CONNECT:
				if(editor.pathList.Count > 0) PickModule(ref e, false);
					break;
				case EditorState.REMOVE:
				if(editor.pathList.Count > 0) RemoveNode(ref e);
					break;
				case EditorState.SELECT:
				if(editor.pathList.Count > 0) PickModule(ref e, false);
					break;
			}
		}

		if(e.type == EventType.MouseUp)
		{
			if(editor.curentState == EditorState.CONNECT)
			{
				PickModule(ref e, true);
				ConnectNode();
				Debug.Log(editor.selectedID + ", " + editor.selectedEventUP);
			}
		}
	}

	private bool PickModule(ref Event e, bool connectMode)
	{
		Vector2 msPoint = Event.current.mousePosition;
		Ray ray = HandleUtility.GUIPointToWorldRay(msPoint);
		RaycastHit hit;
		bool result = Physics.Raycast(ray, out hit , 1000.0f);
		 
		Debug.Log(result);
		if(result)
		{
			GameObject pathnodeObj = hit.transform.gameObject;
			Debug.Log(pathnodeObj.name);
			scriptPathNode nodeInfo = pathnodeObj.GetComponent<scriptPathNode>();
			if(nodeInfo == null)
			{
				if(!connectMode) editor.selectedID = -1;
				else editor.selectedEventUP = -1;
				editor.selectedRemove = -1;
				return false;
			}

			if(e.button == 0)
			{
				SearchNode(nodeInfo.getID(), connectMode);
				return true;
			}
		}

		if(!connectMode) editor.selectedID = -1;
		else editor.selectedEventUP = -1;
		editor.selectedRemove = -1;
		return false;
	}
}
