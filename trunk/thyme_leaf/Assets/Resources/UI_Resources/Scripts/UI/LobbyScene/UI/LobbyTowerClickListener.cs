using UnityEngine;
using System.Collections;

public class LobbyTowerClickListener : MonoBehaviour {

	void OnClick() {
		GameObject.Find("Manager").GetComponent<SceneManager>().CurrentScene = SceneManager.TOWER;
	}
}
