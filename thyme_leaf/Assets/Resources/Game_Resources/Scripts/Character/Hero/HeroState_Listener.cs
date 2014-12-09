using UnityEngine;
using System.Collections;

public class HeroState_Listener : MonoBehaviour
{
	public float v = -1.1f;
	public float h= 0.5f;

    void OnClick()
    {
		Debug.Log ("Attack Click*********************");

		Transform temp = GameObject.Find ("Heroes").transform;
        Hero hero = HeroSpawner.Instance.Allocate();
		hero.transform.parent = temp;
		hero.transform.localScale = Vector2.one;
		hero.transform.position = new Vector2(v,h);

		hero.gameObject.GetComponent<Hero>().StateMachine.ChangeState(HeroState_Moving.Instance);


		//another


		//Message msg = tower.ObtainMessage(MessageTypes.MSG_BUILD_TOWER, new TowerBuildCommand(tower));
  		//tower.DispatchMessage(msg);
    }


}
