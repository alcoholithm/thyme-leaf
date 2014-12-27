using UnityEngine;
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
		string fullPath = dataPath + "/Resources/MapFile/" + fileName;

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

			dat = dataNode.automatPoint == true ? "true" : "false";
			textWriter.WriteLine("automatpoint " + dat);
			dat = dataNode.trovantPoint == true ? "true" : "false";
			textWriter.WriteLine("trovantpoint " + dat);
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

	public static void LoadData(int stageNumber, GameObject nodePref, GameObject automat_ct, GameObject trovant_ct)
	{
		if(stageNumber <= 0) return;

		string fileName = "mData" + stageNumber;
		string fullPath = "MapFile/" + fileName;

		TextAsset textAs = (TextAsset)Resources.Load(fullPath, typeof(TextAsset));

		if(textAs == null)
		{
			Debug.Log("no data");
			return;
		}
		Stream strm = new MemoryStream(textAs.bytes);
		StreamReader textReader = new StreamReader(strm);

		Define.pathNode = new List<MapDataStruct> ();
		//parsing start

		string str = textReader.ReadLine();
		int DataNodeNum = int.Parse(str.Split(' ')[1]);

		//first work
		string nameA = textReader.ReadLine().Split(' ')[1];
		GameObject obj = Instantiate(nodePref) as GameObject;
		obj.transform.parent = GameObject.Find("2 - PathNodePool").transform;
		obj.transform.localPosition = new Vector3(0,0,0);
		obj.transform.localScale = new Vector3 (1, 1, 1);
		obj.name = nameA;
		Define.pathNode.Add(new MapDataStruct (obj, false, false, false));
		for(int i=1;i<DataNodeNum;i++)
		{
			//file format...
			for(int k=0;k<12;k++) textReader.ReadLine();
			nameA = textReader.ReadLine().Split(' ')[1];
			
			obj = Instantiate(nodePref) as GameObject;
			obj.transform.parent = GameObject.Find("2 - PathNodePool").transform;
			obj.transform.localPosition = new Vector3(0,0,0);
			obj.transform.localScale = new Vector3 (1, 1, 1);
			obj.name = nameA;
			Define.pathNode.Add(new MapDataStruct (obj, false, false, false));
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
					if(next_node == Define.pathNode[i].obj.name)
					{
						Next = Define.pathNode[i].obj;
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
					if(prev_node == Define.pathNode[i].obj.name)
					{
						Prev = Define.pathNode[i].obj;
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
					if(turnoff_node == Define.pathNode[i].obj.name)
					{
						TurnOffRoot = Define.pathNode[i].obj;
						break;
					}
				}
			}

			bool automat_pt = bool.Parse(textReader.ReadLine().Split(' ')[1]);

			bool trovant_pt = bool.Parse(textReader.ReadLine().Split(' ')[1]);

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
						if(tempList[i] == Define.pathNode[k].obj.name)
						{
							turnList[i] = Define.pathNode[k].obj;
							break;
						}
					}
				}
			}

			//game object infor setting
			GameObject tempsetting = Define.pathNode[c].obj;
			tempsetting.GetComponent<SphereCollider>().enabled = false;

			scriptPathNode tempFunc = tempsetting.GetComponent<scriptPathNode>();
			Define.pathNode[c].obj.transform.localPosition = current_pos;
			Define.pathNode[c].obj.transform.localScale = new Vector3(1,1,1);
			tempFunc.DataInit();

			tempFunc.setID(id);
			tempFunc.Next = Next;
			tempFunc.Prev = Prev;
			tempFunc.turnoffBridge = TurnOffRoot;
			tempFunc.automatPoint = automat_pt;
			tempFunc.trovantPoint = trovant_pt;
			tempFunc.TurnoffRoot = turnoff_pt;
			tempFunc.SetTurnOffListCount(turnoffIndex);
			for(int i=0;i<Define.TurnOffMaxCount();i++)
				tempFunc.turnoffList[i] = turnList[i];

			if(tempFunc.automatPoint) tempFunc.ChangeIMG(SpriteList.AUTOMAT);
			else if(tempFunc.trovantPoint) tempFunc.ChangeIMG(SpriteList.TROVANT);
			else if(tempFunc.TurnoffRoot) tempFunc.ChangeIMG(SpriteList.TURNOFF);

			tempFunc.SetVisialbe(true);

			MapDataStruct mapdata = Define.pathNode[c];
			mapdata.obj = tempsetting;
			Define.pathNode[c] = mapdata;
		}

		//ohu~~
		/*
		//object center position setting...
		for(int i=0;i<Define.pathNode.Count;i++)
		{
			scriptPathNode tempFunc = Define.pathNode[i].obj.GetComponent<scriptPathNode>();
			if(tempFunc.automatPoint && !Define.pathNode[i].isUse)
			{
				MapDataStruct map_data = Define.pathNode[i];
				map_data.isUse = true;
				map_data.automat_center = true;
				Define.pathNode[i] = map_data;

				GameObject center = Instantiate(automat_ct) as GameObject;
				center.transform.parent = GameObject.Find("4 - UI").transform.FindChild("1 - Base layer");
				center.transform.localPosition = Define.pathNode[i].obj.transform.localPosition;
				center.transform.localScale = new Vector3(1, 1, 1);

				i = 0;
				continue;
			}

			if(tempFunc.trovantPoint && !Define.pathNode[i].isUse)
			{
				MapDataStruct map_data = Define.pathNode[i];
				map_data.isUse = true;
				map_data.trovant_center = true;
				Define.pathNode[i] = map_data; 

				GameObject center = Instantiate(trovant_ct) as GameObject;
				center.transform.parent = GameObject.Find("4 - UI").transform.FindChild("1 - Base layer");
				center.transform.localPosition = Define.pathNode[i].obj.transform.localPosition;
				center.transform.localScale = new Vector3(1, 1, 1);

				i = 0;
				continue;
			}
		}
		*/
	}
}

public struct MapDataStruct
{
	public GameObject obj;
	public bool trovant_center;
	public bool automat_center;
	public bool isUse;

	public MapDataStruct(GameObject obj, bool trovant_center, bool automat_center, bool isUse)
	{
		this.obj = obj;
		this.automat_center = automat_center;
		this.trovant_center = trovant_center;
		this.isUse = isUse;
	}

	public MapDataStruct(int null_)
	{
		obj = null;
		automat_center = trovant_center = isUse = false;
	}
}
