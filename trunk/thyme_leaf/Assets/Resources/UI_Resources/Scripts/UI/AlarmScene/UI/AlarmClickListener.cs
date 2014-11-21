using UnityEngine;
using System.Collections;

public class AlarmClickListener : MonoBehaviour {

	void OnClick() {
		GameObject.Find("Manager").GetComponent<SceneManager>().CurrentScene = SceneManager.USERSELECT;
	}
}
