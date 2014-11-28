using UnityEngine;
using System.Collections;

public class nodeTracing : MonoBehaviour {

	// Use this for initialization
	private Transform childObj;

	private bool activeChk  = false;

	void Update () 
	{
		if(activeChk) Module ();
	}

	public void Initialize()
	{
		childObj = transform.parent.FindChild ("PinPoint");
		for(int i=0;i<Define.pathNode.Count;i++)
		{
			scriptPathNode tempFunc = Define.pathNode[i].GetComponent<scriptPathNode>();
			if(tempFunc.startPoint) transform.localPosition = Define.pathNode[i].transform.localPosition;
		}
	}

	private void Module()
	{
		Interpolation ();
	}

	private void Interpolation()
	{
		float rate = 0.9f * Time.deltaTime;
		float dx = (FocusPosition ().x - CurrentPosition ().x) * rate;
		float dy = (FocusPosition ().y - CurrentPosition ().y) * rate;
		Add (dx, dy, 0);
	}

	private void Add(float ax, float ay, float az)
	{
		transform.localPosition += new Vector3 (ax, ay, az) * 2;
	}

	private Vector3 FocusPosition()
	{
		return childObj.localPosition;
	}

	private Vector3 CurrentPosition()
	{
		return transform.localPosition;
	}
}
