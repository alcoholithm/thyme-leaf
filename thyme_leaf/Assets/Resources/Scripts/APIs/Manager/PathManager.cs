using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathManager : Manager<PathManager> {

	public GameObject PathNode;
	public int StageNumber = 1;

	public new const string TAG = "[PathManager]";

	void Awake()
	{
		DataToFile.LoadData(StageNumber, PathNode);
	}

	public void ShootMap()
	{
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
				
				//				GameObject center = Instantiate(automat_ct) as GameObject;
				//				center.transform.parent = GameObject.Find("AutomatBuildings").transform;
				W_Chat w = Spawner.Instance.GetWChat(WChatType.WCHAT_TYPE1);
				w.transform.localPosition = Define.pathNode[i].obj.transform.localPosition;
				//				center.transform.localScale = new Vector3(1, 1, 1);
				
				i = 0;
				continue;
			}
			
			if(tempFunc.trovantPoint && !Define.pathNode[i].isUse)
			{
				MapDataStruct map_data = Define.pathNode[i];
				map_data.isUse = true;
				map_data.trovant_center = true;
				Define.pathNode[i] = map_data; 
				
				//				GameObject center = Instantiate(trovant_ct) as GameObject;
				//				center.transform.parent = GameObject.Find("TrovantBuildings").transform;
				//				center.transform.localPosition = Define.pathNode[i].obj.transform.localPosition;
				//				center.transform.localScale = new Vector3(1, 1, 1);
				W_Chat w = Spawner.Instance.GetThouse(THouseType.THOUSE_TYPE1);
				w.transform.localPosition = Define.pathNode[i].obj.transform.localPosition;
				
				i = 0;
				continue;
			}
		}
	}

}
