using UnityEngine;
using System.Collections;

public class CommandTravant_Initialize : MonoBehaviour 
{
	void Start() 
	{
		for(int i=0;i<Define.pathNode.Count;i++)
		{
			scriptPathNode tempFunc = Define.pathNode[i].obj.GetComponent<scriptPathNode>();
			if(tempFunc.trovantPoint)
			{
				transform.localPosition = Define.pathNode[i].obj.transform.localPosition;
			}
		}
	}
}
