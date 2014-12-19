using UnityEngine;
using System.Collections;

public class HeroState_Dying : State<Hero> {
	
	private string animationName = "Comma_Dying_";
    private HeroSpawner heroSpawner;

	private HeroState_Dying() {
		Successor = null;
        heroSpawner = GameObject.Find(EnumConverter.getSpawnerNameBy(SpawnerType.HERO_SPAWNER)).GetComponent<HeroSpawner>();
	}
	
	public override void Enter (Hero owner)
	{
		if((owner.getLayer() == Layer.Automart)){
			owner.Anim.PlayOneShot("Comma_Dying_");
		}else if((owner.getLayer() == Layer.Trovant)) {
			owner.Anim.PlayOneShot("Python_Dying");
		}
	}
	
	public override void Execute (Hero owner)
	{
		if( !owner.GetAnim().isPlaying)
		{
			owner.StateMachine.ChangeState(HeroState_None.Instance);
		}
	}
	
	public override void Exit (Hero owner)
	{
		if(owner == null) return;

		//no massage...
	//	Message msg = owner.target.ObtainMessage(MessageTypes.MSG_MOVE_HERO, new HeroMovingCommand(owner.target));
	//	owner.target.DispatchMessage(msg);

        heroSpawner.Free(owner.gameObject);

		//remove character in unit pool...
		UnitPoolController.GetInstance ().RemoveUnit (owner.gameObject, owner.model.Type);

		//throw new System.NotImplementedException ();
	}

	public override bool HandleMessage (Message msg)
	{
		throw new System.NotImplementedException ();
	}
	
	private static HeroState_Dying instance = new HeroState_Dying();
	public static HeroState_Dying Instance {
		get { return HeroState_Dying.instance;}
		set { HeroState_Dying.instance = value;}
	}
	
	
}
