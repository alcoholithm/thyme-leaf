﻿using UnityEngine;
using System.Collections;

public class LobbyOptionClickListener : MonoBehaviour {

		void OnClick() {
			GameObject.Find("Manager").GetComponent<SceneManager>().CurrentScene = SceneManager.SETTING;
		}
}
