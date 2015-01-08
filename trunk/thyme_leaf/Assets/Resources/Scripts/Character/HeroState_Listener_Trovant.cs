using UnityEngine;
using System.Collections;

public class HeroState_Listener_Trovant : MonoBehaviour
{
	private bool wave_trigger;
	void Awake()
	{
		wave_trigger = GameObject.Find ("WaveManager").GetComponent<WaveManager> ().WaveSystemTrigger;
	}

	void OnClick()
	{
	//	if(wave_trigger) return;

		Spawner.Instance.GetTrovant (TrovantType.COMMA);
	}
}
