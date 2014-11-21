using UnityEngine;
using System.Collections;

public class UserOptionClickListener : MonoBehaviour {

	void OnClick() {
		GameObject.Find("Manager").GetComponent<SceneManager>().CurrentScene = SceneManager.SETTING;
	}
}
