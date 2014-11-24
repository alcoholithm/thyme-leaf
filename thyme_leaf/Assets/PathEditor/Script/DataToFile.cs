﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class DataToFile : MonoBehaviour
{
	public static void SavaData(ref List<GameObject> data, int stageNum)
	{
		if(data.Count <= 0)
		{
			Debug.Log("No Data");
			return;
		}

		string fileName = "mData" + stageNum + ".txt";
		string dataPath = Application.dataPath;
		string fullPath = dataPath + "/Resources/File/" + fileName;

		FileStream fs = new FileStream(fullPath, FileMode.Create);
		TextWriter textWriter = new StreamWriter(fs);

		textWriter.WriteLine("numofdata " + data.Count);

		for(int c = 0;c<data.Count;c++)
		{
			scriptPathNode dataNode = data[c].GetComponent<scriptPathNode>();

			textWriter.WriteLine("name " + data[c].name);
			textWriter.WriteLine("id " + dataNode.getID());

			textWriter.WriteLine("current_pos");
			string px = dataNode.getPos(PosParamOption.CURRENT).x.ToString();
			string py = dataNode.getPos(PosParamOption.CURRENT).y.ToString();
			string pz = dataNode.getPos(PosParamOption.CURRENT).z.ToString();
			textWriter.WriteLine(px+" "+py+" "+pz);

			string dat = dataNode.Next == null ? "null" : dataNode.Next.name;
			textWriter.WriteLine("next " + dat);
			dat = dataNode.Prev == null ? "null" : dataNode.Prev.name;
			textWriter.WriteLine("prev " + dat);
			dat = dataNode.turnoffBridge == null ? "null" : dataNode.turnoffBridge.name;
			textWriter.WriteLine("turnoffBridge " + dat);

			dat = dataNode.startPoint == true ? "true" : "false";
			textWriter.WriteLine("startpoint " + dat);
			dat = dataNode.endPoint == true ? "true" : "false";
			textWriter.WriteLine("endpoint " + dat);
			dat = dataNode.TurnoffRoot == true ? "true" : "false";
			textWriter.WriteLine("turnoffRoot " + dat);

			dat = "";
			textWriter.WriteLine("turnoffidx " + dataNode.CountTurnOffList());
			for(int i=0;i<Define.TurnOffMaxCount();i++)
			{
				dat += dataNode.turnoffList[i] == null ? "null" : dataNode.turnoffList[i].name;
				dat += " ";
			}
			textWriter.WriteLine("turnoffList");
			textWriter.WriteLine(dat);

			/*
			textWriter.WriteLine("directionNext");
			dat = dataNode.getDir(DirectionOption.TO_NEXT).x.ToString() + " " + 
					dataNode.getDir(DirectionOption.TO_NEXT).y.ToString() + " " + 
						dataNode.getDir(DirectionOption.TO_NEXT).z.ToString();
			textWriter.WriteLine(dat);

			textWriter.WriteLine("directionPrev");
			dat = dataNode.getDir(DirectionOption.TO_PREV).x.ToString() + " " + 
				dataNode.getDir(DirectionOption.TO_PREV).y.ToString() + " " + 
					dataNode.getDir(DirectionOption.TO_PREV).z.ToString();
			textWriter.WriteLine(dat);

			textWriter.WriteLine("directionTurnRoot");
			dat = dataNode.getDir(DirectionOption.TO_TURNROOT).x.ToString() + " " + 
				dataNode.getDir(DirectionOption.TO_TURNROOT).y.ToString() + " " + 
					dataNode.getDir(DirectionOption.TO_TURNROOT).z.ToString();
			textWriter.WriteLine(dat);

			textWriter.WriteLine("directionTurnList");
			Vector3[] tempDir = dataNode.getDirTurnoffList();
			for(int i=0;i<Define.TurnOffMaxCount();i++)
			{
				dat = tempDir[i].x.ToString() + " " + 
					tempDir[i].y.ToString() + " " + 
						tempDir[i].z.ToString();
				textWriter.WriteLine(dat);
			}
			*/
		}
		textWriter.Close();
	}

	public static void LoadData(int stageNumber, GameObject nodePref)
	{
		if(stageNumber <= 0) return;

		string fileName = "mData" + stageNumber;
		string fullPath = "File/" + fileName;

		TextAsset textAs = (TextAsset)Resources.Load(fullPath, typeof(TextAsset));

		if(textAs == null)
		{
			Debug.Log("no data");
			return;
		}
		Stream strm = new MemoryStream(textAs.bytes);
		StreamReader textReader = new StreamReader(strm);

		Define.pathNode = new List<GameObject>();
		//parsing start

		string str = textReader.ReadLine();
		int DataNodeNum = int.Parse(str.Split(' ')[1]);

		//first work
		string nameA = textReader.ReadLine().Split(' ')[1];
		GameObject obj = Instantiate(nodePref) as GameObject;
		obj.transform.parent = GameObject.Find("PathNodePool").transform;
		obj.transform.position = new Vector3(0,0,0);
		obj.name = nameA;
		Define.pathNode.Add(obj);
		for(int i=1;i<DataNodeNum;i++)
		{
			//file format...
			for(int k=0;k<12;k++) textReader.ReadLine();
			nameA = textReader.ReadLine().Split(' ')[1];
			
			obj = Instantiate(nodePref) as GameObject;
			obj.transform.parent = GameObject.Find("PathNodePool").transform;
			obj.transform.position = new Vector3(0,0,0);
			obj.name = nameA;
			Define.pathNode.Add(obj);
		}
		textReader = new StreamReader(strm);
		textReader.BaseStream.Seek(0, SeekOrigin.Begin);  //first point
		textReader.ReadLine();

		//rooping
		for(int c=0;c<Define.pathNode.Count;c++)
		{
			string node_name = textReader.ReadLine().Split(' ')[1];
			//Debug.Log(node_name);
			int id = int.Parse(textReader.ReadLine().Split(' ')[1]);

			textReader.ReadLine(); //current pos
			string[] tempPos = textReader.ReadLine().Split(' ');
			Vector3 current_pos = new Vector3(
				float.Parse(tempPos[0]), 
				float.Parse(tempPos[1]),
				float.Parse(tempPos[2])
				);

			GameObject Next = null;
			string next_node = textReader.ReadLine().Split(' ')[1];
			if(next_node != "null")
			{
				for(int i=0;i<Define.pathNode.Count;i++)
				{
					if(next_node == Define.pathNode[i].name)
					{
						Next = Define.pathNode[i];
						break;
					}
				}
			}

			GameObject Prev = null;
			string prev_node = textReader.ReadLine().Split(' ')[1];
			if(prev_node != "null")
			{
				for(int i=0;i<Define.pathNode.Count;i++)
				{
					if(prev_node == Define.pathNode[i].name)
					{
						Prev = Define.pathNode[i];
						break;
					}
				}
			}

			GameObject TurnOffRoot = null;
			string turnoff_node = textReader.ReadLine().Split(' ')[1];
			if(turnoff_node != "null")
			{
				for(int i=0;i<Define.pathNode.Count;i++)
				{
					if(turnoff_node == Define.pathNode[i].name)
					{
						TurnOffRoot = Define.pathNode[i];
						break;
					}
				}
			}

			bool start_pt = bool.Parse(textReader.ReadLine().Split(' ')[1]);

			bool end_pt = bool.Parse(textReader.ReadLine().Split(' ')[1]);

			bool turnoff_pt = bool.Parse(textReader.ReadLine().Split(' ')[1]);

			int turnoffIndex = int.Parse(textReader.ReadLine().Split(' ')[1]);

			textReader.ReadLine(); //turnoff list
			string[] tempList = textReader.ReadLine().Split(' ');
			GameObject[] turnList = new GameObject[Define.TurnOffMaxCount()];
			for(int i=0;i<Define.TurnOffMaxCount();i++)
			{
				if(tempList[i] == "null")
				{
					turnList[i] = null;
				}
				else
				{
					for(int k=0;k<Define.pathNode.Count;k++)
					{
						if(tempList[i] == Define.pathNode[k].name)
						{
							turnList[i] = Define.pathNode[k];
							break;
						}
					}
				}
			}

			//game object infor setting
			GameObject tempsetting = Define.pathNode[c];
			scriptPathNode tempFunc = tempsetting.GetComponent<scriptPathNode>();
			Define.pathNode[c].transform.localPosition = current_pos;
			Define.pathNode[c].transform.localScale = new Vector3(50,50,50);
			tempFunc.DataInit();

			tempFunc.setID(id);
			tempFunc.Next = Next;
			tempFunc.Prev = Prev;
			tempFunc.turnoffBridge = TurnOffRoot;
			tempFunc.startPoint = start_pt;
			tempFunc.endPoint = end_pt;
			tempFunc.TurnoffRoot = turnoff_pt;
			tempFunc.SetTurnOffListCount(turnoffIndex);
			for(int i=0;i<Define.TurnOffMaxCount();i++)
				tempFunc.turnoffList[i] = turnList[i];
			if(tempFunc.startPoint) tempFunc.ChangeIMG(SpriteList.START);
			else if(tempFunc.endPoint) tempFunc.ChangeIMG(SpriteList.END);
			else if(tempFunc.TurnoffRoot) tempFunc.ChangeIMG(SpriteList.TURNOFF);

			Define.pathNode[c] = tempsetting;
		}


	}
}
