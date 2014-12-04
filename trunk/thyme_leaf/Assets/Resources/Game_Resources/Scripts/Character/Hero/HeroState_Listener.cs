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
		hero.transform.localPosition = new Vector3 (0, 0, 0);
		hero.EnableAlive ();
		hero.Visiable();
		hero.setName (UnitNameGetter.GetInstance ().getNameAutomart ());
		//another

		//Message msg = tower.ObtainMessage(MessageTypes.MSG_BUILD_TOWER, new TowerBuildCommand(tower));
  		//tower.DispatchMessage(msg);
    }


}
