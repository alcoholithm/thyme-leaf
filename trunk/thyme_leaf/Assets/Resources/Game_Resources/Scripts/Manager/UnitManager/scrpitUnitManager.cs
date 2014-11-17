using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class scrpitUnitManager : MonoBehaviour {

	public GameObject PathNode;
	public int StageNumber = 1;
	public Sprite startSprite, endSprite, turnoffSprite, normalSprite;

	void Awake()
	{
		DataToFile.LoadData(StageNumber, PathNode);
	}
}
