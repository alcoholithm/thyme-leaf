using UnityEngine;
using System.Collections;

public class CommandTravant_Initialize : MonoBehaviour 
{
	void Start() 
	{
		for(int i=0;i<Define.pathNode.Count;i++)
		{
			scriptPathNode tempFunc = Define.pathNode[i].GetComponent<scriptPathNode>();
			if(tempFunc.endPoint)
			{
				transform.localPosition = Define.pathNode[i].transform.localPosition;
			}
		}
	}
}
