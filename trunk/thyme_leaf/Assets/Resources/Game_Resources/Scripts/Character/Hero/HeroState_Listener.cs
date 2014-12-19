using UnityEngine;
using System.Collections;

public class HeroState_Listener : MonoBehaviour
{
    void OnClick()
    {
		Transform temp = GameObject.Find ("AutomatUnits").transform;
        Hero hero = HeroSpawner.Instance.Allocate();
		Debug.Log ("character init");

		//main setting...
		hero.transform.parent = temp;
		hero.transform.localScale = Vector3.one;
		hero.transform.localPosition = new Vector3 (0, 0, 0);

		//unit detail setting...
		hero.setLayer(Layer.Automart);
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

		hero.controller.setType (UnitType.AUTOMART_CHARACTER);
		hero.controller.setName (UnitNameGetter.GetInstance ().getNameAutomart ());
		
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
