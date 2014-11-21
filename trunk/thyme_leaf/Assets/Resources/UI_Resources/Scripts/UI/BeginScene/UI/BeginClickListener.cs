using UnityEngine;
using System.Collections;

public class BeginClickListener : MonoBehaviour {

	void OnClick() {
		GameObject.Find("Manager").GetComponent<SceneManager>().CurrentScene = SceneManager.ALARM;
	}
}
