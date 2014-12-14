﻿using UnityEngine;
using System.Collections;

public class HeroState_Listener_Trovant : MonoBehaviour
{
    private TrovantSpawner trovantSpawner;

    void Awake(){
        trovantSpawner = GameObject.Find(EnumConverter.getSpawnerNameBy(SpawnerType.TROVANT_SPAWNER)).GetComponent<TrovantSpawner>();
    }

	void OnClick()
	{
		Transform temp = GameObject.Find ("TrovantPool").transform;

        Hero hero = trovantSpawner.DynamicInstantiate();

		//main setting...
		hero.transform.parent = temp;
		hero.transform.localScale = Vector3.one;
		hero.transform.localPosition = new Vector3 (0, 0, 0);
		hero.setLayer(Layer.Trovant());

		//unit detail setting...
		hero.EnableAlive ();
		hero.Visiable();
		float range_value = 100;
		hero.setOffset (0, 0);
//		hero.setOffset (Random.Range (-range_value, range_value), Random.Range (-range_value, range_value));
		hero.setName (UnitNameGetter.GetInstance ().getNameTrovant());

		//another   
		//Message msg = tower.ObtainMessage(MessageTypes.MSG_BUILD_TOWER, new TowerBuildCommand(tower));
		//tower.DispatchMessage(msg);
	}
	
	
}
