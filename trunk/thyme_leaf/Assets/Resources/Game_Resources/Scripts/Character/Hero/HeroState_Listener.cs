using UnityEngine;
using System.Collections;

public class HeroState_Listener : MonoBehaviour
{
    void OnClick()
    {
		Transform temp = GameObject.Find ("AutomartPool").transform;
        Hero hero = HeroSpawner.Instance.Allocate();

		//main setting...
		hero.transform.parent = temp;
		hero.transform.localScale = Vector3.one;
		hero.transform.localPosition = new Vector3 (0, 0, 0);

		//unit detail setting...
		hero.setLayer(Layer.Automart());
		hero.EnableAlive ();
		hero.Visiable();
		float range_value = 100;
	//	hero.setOffset (0, 0);
		hero.setOffset (Random.Range (-range_value, range_value), Random.Range (-range_value, range_value));
		hero.setName (UnitNameGetter.GetInstance ().getNameAutomart ());

		//another   
		//Message msg = tower.ObtainMessage(MessageTypes.MSG_BUILD_TOWER, new TowerBuildCommand(tower));
  		//tower.DispatchMessage(msg);
    }


}
