using UnityEngine;
using System.Collections;

public class LobbyBackClickListener : MonoBehaviour {
	void OnClick() {
		GameObject.Find("Manager").GetComponent<SceneManager>().CurrentScene = SceneManager.USERSELECT;
	}
}
