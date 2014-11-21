using UnityEngine;
using System.Collections;

public class LobbyAutomartClickListener : MonoBehaviour {

	void OnClick() {
		GameObject.Find("Manager").GetComponent<SceneManager>().CurrentScene = SceneManager.AUTOMART;
	}
}
