using UnityEngine;
using System.Collections;

public class HeroState_Listener : MonoBehaviour
{
    [RPC]
    void NetworkOnClick()
    {

    }

    void OnClick()
    {
        if (Network.peerType != NetworkPeerType.Disconnected)
        {
            Debug.Log("Connected....");
        }
		Transform temp = GameObject.Find ("AutomatUnits").transform;
        Hero hero = Spawner.Instance.Allocate();
		Debug.Log ("character init");

		//main setting...
		hero.transform.parent = temp;
		hero.transform.localScale = Vector3.one;
		hero.transform.localPosition = new Vector3 (0, 0, 0);

		//unknown code..
		hero.gameObject.SetActive (false);
		hero.gameObject.SetActive (true);

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
		hero.CollisionSetting (true);

		hero.controller.setType (UnitType.AUTOMART_CHARACTER);
		hero.controller.setName (UnitNameGetter.GetInstance ().getNameAutomart ());
		
		//move trigger & unit pool manager setting <add>...
		//moving state...
		hero.StateMachine.ChangeState (HeroState_Moving.Instance);
		//moveing enable...
		hero.controller.setMoveTrigger(true);
		//hp bar setting...
		hero.HealthUpdate ();

		//test...
		hero.my_name = hero.model.Name;

		//unit pool insert...
		UnitObject u_obj = new UnitObject (hero.gameObject, hero.model.Name, hero.model.Type);
		UnitPoolController.GetInstance ().AddUnit (u_obj);
		//another   
		//Message msg = tower.ObtainMessage(MessageTypes.MSG_BUILD_TOWER, new TowerBuildCommand(tower));
  		//tower.DispatchMessage(msg);
    }


}
