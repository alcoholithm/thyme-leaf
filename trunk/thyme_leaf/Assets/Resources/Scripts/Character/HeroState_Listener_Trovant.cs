using UnityEngine;
using System.Collections;

public class HeroState_Listener_Trovant : MonoBehaviour
{
	void OnClick()
	{
//		Transform temp = GameObject.Find ("TrovantUnits").transform;
		Hero hero = HeroSpawnerScript.Instance.GetHero ();
//
//		//main setting...
//		hero.transform.parent = temp;
//		hero.transform.localScale = Vector3.one;
//		hero.transform.localPosition = new Vector3 (0, 0, 0);
//
//		//unknown code..
////		hero.gameObject.SetActive (false);
////		hero.gameObject.SetActive (true);
//
//		//unit detail setting...
//		hero.setLayer(Layer.Trovant);
//		if(hero.getLayer() == Layer.Automart)
//		{
//			hero.controller.StartPointSetting(StartPoint.AUTOMART_POINT);
//		}
//		else if(hero.getLayer() == Layer.Trovant)
//		{			
//			hero.controller.StartPointSetting(StartPoint.TROVANT_POINT);
//		}
//		hero.CollisionSetting (true);
//		
//		hero.controller.setType (UnitType.TROVANT_CHARACTER);
//		hero.controller.setName (UnitNameGetter.GetInstance ().getNameTrovant ());
//
//		//move trigger & unit pool manager setting <add>...
//		//moving state...
//		hero.StateMachine.ChangeState (HeroState_Moving.Instance);
//		//moveing enable...
//		hero.controller.setMoveTrigger(true);
//		//hp bar setting...
//		hero.HealthUpdate ();
//
//		//test...
//		hero.my_name = hero.model.Name;
//
//		//unit pool insert...
//		UnitObject u_obj = new UnitObject (hero.gameObject, hero.model.Name, hero.model.Type);
//		UnitPoolController.GetInstance ().AddUnit (u_obj);
		//another   
		//Message msg = tower.ObtainMessage(MessageTypes.MSG_BUILD_TOWER, new TowerBuildCommand(tower));
		//tower.DispatchMessage(msg);
	}
	
	
}
