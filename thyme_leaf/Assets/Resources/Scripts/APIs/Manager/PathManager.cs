using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathManager : Manager<PathManager> {

	public GameObject PathNode;
	public int StageNumber = 1;
	public bool node_visible;
	public GameMode game_mode;
	public static Vector3 client_position;
	public static Vector3 server_position;
	public static Vector3 single_position;

	void Awake()
	{
		//current stage number setting...
		Define.current_stage_number = StageNumber;
		if(game_mode == GameMode.MULTI_PLAY) Define.current_stage_number = 100000;
		//initialize...
		Define.PathDataDispose ();
		Define.CenterListDisPose ();
		UnitNameGetter.GetInstance ().Initialize ();
		UnitMusterController.GetInstance ().Initialize ();
		UnitPoolController.GetInstance ().Initialize ();

		DataToFile.LoadData(Define.current_stage_number, PathNode, node_visible, game_mode);

		//define setting...
		
		//path node offset setting...
		Define.path_node_off.Dispose ();
		
		PathNodeOffsetStruct offset_data = new PathNodeOffsetStruct(9);
		float scale = 0.7f;
		offset_data.setOffsetPos (0, -65, -65, scale);
		offset_data.setOffsetPos (1, -80, 0, scale);
		offset_data.setOffsetPos (2, 65, -65, scale);
		offset_data.setOffsetPos (3, 0, -80, scale);
		offset_data.setOffsetPos (4, 65, 65, scale);
		offset_data.setOffsetPos (5, 80, 0, scale);
		offset_data.setOffsetPos (6, -65, 65, scale);
		offset_data.setOffsetPos (7, 0, 80, scale);
		offset_data.setOffsetPos (8, 0, 0, scale);
		
		Define.path_node_off = offset_data;
		
		//random table setting...
		Define.random_index_table = new RandomTableStruct (30, 0, 3);
		//center data...
		Define.THouse_list = new List<GameObject> ();
	}

	public void ShootMap()
	{
		//object center position setting...
		for(int i=0;i<Define.pathNode.Count;i++)
		{
			scriptPathNode tempFunc = Define.pathNode[i].obj.GetComponent<scriptPathNode>();
			if(tempFunc.automatPoint && !Define.pathNode[i].isUse)
			{
				//one time...
				MapDataStruct map_data = Define.pathNode[i];
				map_data.isUse = true;
				map_data.automat_center = true;
				Define.pathNode[i] = map_data;

				i = 0;
				continue;
			}
			
			if(tempFunc.trovantPoint && !Define.pathNode[i].isUse)
			{
				MapDataStruct map_data = Define.pathNode[i];
				map_data.isUse = true;
				map_data.trovant_center = true;
				Define.pathNode[i] = map_data;

                //THouse w = Spawner.Instance.GetThouse(THouseType.THOUSE_TYPE1);
                //w.transform.localPosition = Define.pathNode[i].obj.transform.localPosition;
                //w.PositionNode = Define.pathNode[i].obj.transform.localPosition;
                //w.ChangeState(THouseState_Idling.Instance);
				
				i = 0;
				continue;
			}
		}

//		wave setting...
		GameObject.Find ("WaveManager").GetComponent<WaveManager> ().WaveSystemEnable ();
	}

	public new const string TAG = "[PathManager]";
}
