using UnityEngine;
using System.Collections;

public class HeroState_Listener_Trovant : MonoBehaviour
{
	void OnClick()
	{
		Spawner.Instance.GetTrovant (TrovantType.COMMA);
	}
	
	
}
