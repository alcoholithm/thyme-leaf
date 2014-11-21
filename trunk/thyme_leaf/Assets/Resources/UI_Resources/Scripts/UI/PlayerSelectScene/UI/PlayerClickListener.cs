using UnityEngine;
using System.Collections;

public class PlayerClickListener : MonoBehaviour {

	void OnClick() {
		GameObject.Find("Manager").GetComponent<SceneManager>().CurrentScene = SceneManager.ALARM;
	}
}
