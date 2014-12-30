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


}
