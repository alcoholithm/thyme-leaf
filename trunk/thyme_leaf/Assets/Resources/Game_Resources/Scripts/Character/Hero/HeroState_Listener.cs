using UnityEngine;
using System.Collections;

public class HeroState_Listener : MonoBehaviour
{
    private HeroSpawner heroSpawner;

    void Awake()
    {
        heroSpawner = GameObject.Find(EnumConverter.getManagerName(SpawnerType.HERO_SPAWNER)).GetComponent<HeroSpawner>();
    }

    void OnClick()
    {
		Transform temp = GameObject.Find ("AutomartPool").transform;
        Hero hero = heroSpawner.DynamicInstantiate();

		//main setting...
		hero.transform.parent = temp;
		hero.transform.localScale = Vector3.one;
		hero.transform.localPosition = new Vector3 (0, 0, 0);
		hero.setLayer(Layer.Automart());

		//unit detail setting...
		hero.EnableAlive ();
		hero.Visiable();
//		hero.setMoveMode (MoveModeState.FORWARD);
		float range_value = 100;
		hero.setOffset (0, 0);
		hero.setOffset (Random.Range (-range_value, range_value), Random.Range (-range_value, range_value));
		hero.setName (UnitNameGetter.GetInstance ().getNameAutomart ());

		//another   
		//Message msg = tower.ObtainMessage(MessageTypes.MSG_BUILD_TOWER, new TowerBuildCommand(tower));
  		//tower.DispatchMessage(msg);
    }


}
