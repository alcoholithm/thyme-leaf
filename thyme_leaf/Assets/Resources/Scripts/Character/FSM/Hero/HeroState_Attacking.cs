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

		owner.controller.setStateName (Naming.ATTACKING);
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
					if(owner.helper.attack_target.nameID != GetId(ref other))
					{
						owner.helper.attack_target = other;
					}
					break;
				}
			}
		}

        ChangeStateIntoMoving(owner, isCharacter);
        SendAttackMessage(owner);
    }

    private void ChangeStateIntoMoving(Hero owner, bool isCharacter)
    {
		int hp = GetHp (ref owner.helper.attack_target);
		bool check = MissingChecking (ref owner.helper.attack_target);
		if (!check && hp <= 0 && !isCharacter)
        {
			owner.target.DataInit();
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
        if (owner.helper.attack_target.obj != null)
        {
            //attack...
            owner.helper.attack_delay_counter += Time.deltaTime;
            if (owner.helper.attack_delay_counter >= owner.model.AttackDelay)
            {
				SendMessageAttack(ref owner, MessageTypes.MSG_NORMAL_DAMAGE, (int)owner.model.AttackDamage);

                owner.helper.attack_delay_counter = 0;
                AudioManager.Instance.PlayClipWithState(owner.gameObject, StateType.ATTACKING);                
            }
        }
    }

    public override void Exit(Hero owner)
    {
        //Debug.Log("Attack  Exit ************************");
        //throw new System.NotImplementedException ();
		owner.helper.attack_target.DataInit ();
    }

	private bool MissingChecking(ref UnitObject obj)
	{
		switch(obj.type)
		{
		case UnitType.AUTOMAT_CHARACTER:
		case UnitType.TROVANT_CHARACTER:
			return obj.infor_hero == null ? true : false;
		case UnitType.AUTOMAT_WCHAT:
			return obj.infor_automat_center == null ? true : false;
		case UnitType.TROVANT_THOUSE:
			return obj.infor_trovant_center == null ? true :  false;
		}
		return false;
	}
	
	private int GetHp(ref UnitObject obj)
	{
		switch(obj.type)
		{
		case UnitType.AUTOMAT_CHARACTER:
		case UnitType.TROVANT_CHARACTER:
			return obj.infor_hero.model.HP;
		case UnitType.AUTOMAT_WCHAT:
			return obj.infor_automat_center.Model.HP;
		case UnitType.TROVANT_THOUSE:
			return obj.infor_trovant_center.Model.HP;
		}
		return -1;
	}

	private int GetId(ref UnitObject obj)
	{
		switch(obj.type)
		{
		case UnitType.AUTOMAT_CHARACTER:
		case UnitType.TROVANT_CHARACTER:
			return obj.infor_hero.model.ID;
		case UnitType.AUTOMAT_WCHAT:
			return obj.infor_automat_center.Model.ID;
		case UnitType.TROVANT_THOUSE:
			return obj.infor_trovant_center.Model.ID;
		}
		return -1;
	}

	private void SendMessageAttack(ref Hero obj, MessageTypes option, int v)
	{
		GameEntity game_entity = null;
		switch(obj.helper.attack_target.type)
		{
		case UnitType.AUTOMAT_CHARACTER:
		case UnitType.TROVANT_CHARACTER:
			game_entity = obj.helper.attack_target.infor_hero;
			break;
		case UnitType.AUTOMAT_WCHAT:
			game_entity = obj.helper.attack_target.infor_automat_center;
			break;
		case UnitType.TROVANT_THOUSE:
			game_entity = obj.helper.attack_target.infor_trovant_center;
			break;
		}
		Message msg = game_entity.ObtainMessage(option, v);
		game_entity.DispatchMessage(msg);
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
