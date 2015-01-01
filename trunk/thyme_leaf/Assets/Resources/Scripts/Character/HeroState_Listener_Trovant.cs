using UnityEngine;
using System.Collections;

public class HeroState_Listener_Trovant : MonoBehaviour
{
	//after....co~routine...
	void OnClick()
	{
		Spawner.Instance.GetTrovant (TrovantType.COMMA);
	}
}
