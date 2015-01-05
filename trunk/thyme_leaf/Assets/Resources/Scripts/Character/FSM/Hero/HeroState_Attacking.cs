using UnityEngine;
using System.Collections;

public class HeroState_Attacking : State<Hero>
{

    private string animationsName = "Comma_Attacking_Normal_";

    private HeroState_Attacking()
    {
        Successor = HeroState_Hitting.Instance;
    }

    public override void Enter(Hero owner)
	{
//		if((owner.getLayer() == Layer.Automart)){
//			owner.Anim.Play("Comma_Attacking_Normal_");
//		}else if((owner.getLayer() == Layer.Trovant)) {
//			owner.Anim.Play("Python_Attacking_Normal_");
//		}

		owner.controller.setStateName ("Attacking_");
		owner.state_name = owner.model.StateName;
        owner.helper.attack_delay_counter = 0;  //attack delay setting...

		if(owner.helper.getMusterTrigger())
			UnitMusterController.GetInstance().CommandAttack(owner.model.MusterID, owner.model.ID);
    }

    public override void Execute(Hero owner)
    {
		//area checking...
		bool isCharacter = false;
		for(int i=0;i<UnitPoolController.GetInstance().CountUnit();i++)
		{
			UnitObject other = UnitPoolController.GetInstance().ElementUnit(i);

			if(owner.model.ID == other.nameID) continue;
			
			bool check = false;
			switch(owner.getLayer())
			{
			case Layer.Automart:  //automart
				if(other.obj.layer == (int)Layer.Automart) check = true;
				break;
			case Layer.Trovant:  //trovant
				if(other.obj.layer == (int)Layer.Trovant) check = true;
				break;
			default:
				check = true;
				break;
			}
			
			if(!check)
			{
				float range = 100;//owner.helper.collision_range + 120;
				if(Vector3.SqrMagnitude(other.obj.transform.localPosition - owner.helper.getPos()) < range * range)
				{
					isCharacter = true;
					Hero other_infor = other.obj.GetComponent<Hero>();
					if(owner.helper.attack_target.model.ID != other_infor.model.ID)
						owner.helper.attack_target = other.obj.GetComponent<Hero>();
					break;
				}
			}
		}

        ChangeStateIntoMoving(owner, isCharacter);
        SendAttackMessage(owner);
    }

    

    private void ChangeStateIntoMoving(Hero owner, bool isCharacter)
    {
        if (owner.helper.attack_target == null || (!isCharacter && owner.helper.attack_target.model.HP <= 0))
        {
            owner.target = null;
            if (Network.peerType != NetworkPeerType.Disconnected && owner.networkView.isMine)
            {
                owner.networkView.RPC("NetworkChangeState", RPCMode.All, owner.networkView.viewID);
            }
            else
            {
                owner.StateMachine.ChangeState(HeroState_Moving.Instance);
            }
            Debug.Log("Enemy is died or disappeared");
        }
    }

    private void SendAttackMessage(Hero owner)
    {
        if (owner.helper.attack_target != null)
        {
            //attack...
            owner.helper.attack_delay_counter += Time.deltaTime;
            if (owner.helper.attack_delay_counter >= owner.model.AttackDelay)
            {
                Message msg = owner.helper.attack_target.ObtainMessage(MessageTypes.MSG_DAMAGE, (int)owner.model.AttackDamage);

                owner.helper.attack_target.DispatchMessage(msg);
                owner.helper.attack_delay_counter = 0;
                AudioManager.Instance.PlayClipWithState(owner.gameObject, StateType.ATTACKING);                
            }
        }
    }

    public override void Exit(Hero owner)
    {
        //Debug.Log("Attack  Exit ************************");
        //throw new System.NotImplementedException ();
		owner.helper.attack_target = null;
    }

    public override bool HandleMessage(Message msg)
    {
        switch (msg.what)
        {
            case MessageTypes.MSG_MOVE_HERO:
            case MessageTypes.MSG_MISSING:
                msg.command.Execute();
                return true;
        }
        return false;
    }

    private static HeroState_Attacking instance = new HeroState_Attacking();
    public static HeroState_Attacking Instance
    {
        get { return HeroState_Attacking.instance; }
        set { HeroState_Attacking.instance = value; }
    }


}
