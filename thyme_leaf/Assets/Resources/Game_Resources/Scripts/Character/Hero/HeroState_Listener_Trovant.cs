using UnityEngine;
using System.Collections;

public class HeroState_Listener_Trovant : MonoBehaviour
{
    private TrovantSpawner trovantSpawner;

    void Awake(){
        trovantSpawner = GameObject.Find(EnumConverter.getSpawnerNameBy(SpawnerType.TROVANT_SPAWNER)).GetComponent<TrovantSpawner>();
    }

	void OnClick()
	{
		Transform temp = GameObject.Find ("TrovantUnits").transform;
        Hero hero = trovantSpawner.DynamicInstantiate();
	
		//main setting...
		hero.transform.parent = temp;
		hero.transform.localScale = Vector3.one;
		hero.transform.localPosition = new Vector3 (0, 0, 0);

		//unit detail setting...
		hero.setLayer(Layer.Trovant);
		if(hero.getLayer() == Layer.Automart)
		{
			hero.controller.StartPointSetting(StartPoint.AUTOMART_POINT);
		}
		else if(hero.getLayer() == Layer.Trovant)
		{			
			hero.controller.StartPointSetting(StartPoint.TROVANT_POINT);
		}
		hero.CollisionVisiable ();
//		hero.EnableAlive ();
		
		hero.controller.setType (UnitType.TROVANT_CHARACTER);
		hero.controller.setName (UnitNameGetter.GetInstance ().getNameTrovant ());

		//move trigger & unit pool manager setting <add>...
		//moving state...
		hero.StateMachine.ChangeState (HeroState_Moving.Instance);
		//moveing enable...
		hero.controller.setMoveTrigger(true);
		//unit pool insert...
		UnitPoolController.GetInstance ().AddUnit (hero.gameObject, hero.model.getType());

		//another   
		//Message msg = tower.ObtainMessage(MessageTypes.MSG_BUILD_TOWER, new TowerBuildCommand(tower));
		//tower.DispatchMessage(msg);
	}
	
	
}
