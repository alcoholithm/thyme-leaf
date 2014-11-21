using UnityEngine;
using System.Collections;

public class UserClickListener : MonoBehaviour {

	void OnClick() {
		GameObject.Find("Manager").GetComponent<SceneManager>().CurrentScene = SceneManager.PLAYERSELECT;
	}
}
