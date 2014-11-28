using UnityEngine;
using System.Collections;

public class HeroState_Listener : MonoBehaviour
{
    void OnClick()
    {
		Transform temp = GameObject.Find ("Heroes").transform;
        Hero hero = HeroSpawner.Instance.Allocate();
		hero.transform.parent = temp;
		hero.transform.localScale = Vector3.one;
		hero.transform.position = GameObject.Find ("Heroes").transform.localPosition;

		//another


		//Message msg = tower.ObtainMessage(MessageTypes.MSG_BUILD_TOWER, new TowerBuildCommand(tower));
  		//tower.DispatchMessage(msg);
    }
}
