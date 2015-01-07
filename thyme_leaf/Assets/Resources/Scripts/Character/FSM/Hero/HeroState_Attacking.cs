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
		owner.controller.setStateName (Naming.ATTACKING);
		owner.AnimationName = Naming.Instance.BuildAnimationName(owner.gameObject, owner.model.StateName);
        owner.helper.attack_delay_counter = 0;  //attack delay setting...  testcode...

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

			if(other.obj == null || owner.model.ID == other.nameID) continue;
			
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
            //if(Network.isServer && owner != null)
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
				SendMessageAttack(ref owner, (int)owner.model.AttackDamage);

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
			return obj.infor_hero == null ? -1 : obj.infor_hero.model.HP;
		case UnitType.AUTOMAT_WCHAT:
			return obj.infor_automat_center.Model == null ? -1 : obj.infor_automat_center.Model.HP;
		case UnitType.TROVANT_THOUSE:
			return obj.infor_trovant_center.Model == null ? -1 : obj.infor_trovant_center.Model.HP;
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

	private void SendMessageAttack(ref Hero obj, int v)
	{
		GameEntity game_entity = null;
		MessageTypes option = MessageTypes.MSG_NORMAL_DAMAGE;
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

		switch(obj.model.UnitTypeName)
		{
		case AudioUnitType.FALSTAFF_TYPE1:
		case AudioUnitType.COMMA:
		case AudioUnitType.PYTHON:
			option = MessageTypes.MSG_NORMAL_DAMAGE;
			break;
		case AudioUnitType.FRANSCIS_TYPE1:
			option = MessageTypes.MSG_BURN_DAMAGE;
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
