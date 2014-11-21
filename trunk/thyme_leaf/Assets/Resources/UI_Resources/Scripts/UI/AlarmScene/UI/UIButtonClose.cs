using UnityEngine;
using System.Collections;

public class UIButtonClose : MonoBehaviour {

	public UIScaleAnimation anim;

	void Start()
	{
		anim = GameObject.Find("2 - [1] Popup").GetComponent<UIScaleAnimation>();
	}

	void OnClick()
	{
		anim.close();
	}

}
