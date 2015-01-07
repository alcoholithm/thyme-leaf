using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class scriptEditor : MonoBehaviour 
{
	public GameObject pathNodeObj;
	public EditorState curentState = EditorState.ADD;
	public int StageNumber = 1;

	public List<GameObject> pathList = new List<GameObject>();
	public int productID;
	public int selectedID;
	public int selectedEventUP;
	public int selectedRemove;
	public scriptPathNode NodeNULL;
	public GameMode mode;
	//================================================
}
