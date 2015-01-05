using UnityEngine;
using System.Collections;

public class HeroState_Dying : State<Hero> {
	
	private string animationName = "Comma_Dying_";

	private HeroState_Dying() {
		Successor = null;
	}
	
	public override void Enter (Hero owner)
	{
		if((owner.getLayer() == Layer.Automart)){
			owner.Anim.PlayOneShot(owner.p_name+"Dying_");
		}else if((owner.getLayer() == Layer.Trovant)) {
			owner.Anim.PlayOneShot(owner.p_name+"Dying_");
		}

		owner.controller.setStateName("Dying_");
		owner.state_name = owner.model.StateName;
	}
	
	public override void Execute (Hero owner)
	{
		if( !owner.GetAnim().isPlaying)
			owner.StateMachine.ChangeState(HeroState_None.Instance);
	}
	
	public override void Exit (Hero owner)
	{
		if(owner == null) return;

		//current unit muster release...
		if(owner.helper.getMusterTrigger())
			UnitMusterController.GetInstance().removeUnit(owner.model.MusterID, owner.model.Name);
		//collision module exit...
		owner.Die ();
		//remove character in unit pool...
		UnitPoolController.GetInstance ().RemoveUnit (owner.gameObject);

        Spawner.Instance.Free(owner.gameObject);

        UserAdministrator.Instance.CurrentUser.Gold += 100;

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
