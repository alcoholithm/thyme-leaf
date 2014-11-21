using UnityEngine;
using System.Collections;

public class UserPlayClickListener : MonoBehaviour {

	void OnClick() {
		GameObject.Find("Manager").GetComponent<SceneManager>().CurrentScene = SceneManager.LOBBYS;
	}
}
