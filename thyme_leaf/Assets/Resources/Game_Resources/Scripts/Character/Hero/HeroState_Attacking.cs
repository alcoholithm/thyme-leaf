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
		if((owner.gameObject.layer == (int)Layer.Automart)){
			owner.Anim.Play("Comma_Attacking_Normal_");
		}else if((owner.gameObject.layer == (int) Layer.Trovant)) {
			owner.Anim.Play("Python_Attacking_Normal_");
		}

        owner.helper.attack_delay_counter = 0;  //attack delay setting...
    }

    public override void Execute(Hero owner)
    {
		//area checking...
		bool isCharacter = false;
		for(int i=0;i<UnitPoolController.GetInstance().CountUnit();i++)
		{
			GameObject other = UnitPoolController.GetInstance().ElementUnit(i);
			
			bool check = false;
			switch(owner.gameObject.layer)
			{
			case 9:  //automart
				if(other.layer == (int)Layer.Automart) check = true;
				break;
			case 10:  //trovant
				if(other.layer == (int) Layer.Trovant) check = true;
				break;
			default:
				check = true;
				break;
			}
			
			if(!check)
			{
				float range = owner.helper.collision_range + 50;
				if(Vector3.SqrMagnitude(other.transform.localPosition - owner.helper.getPos()) < range * range)
				{
					isCharacter = true;
					Hero other_infor = other.GetComponent<Hero>();
					if(owner.helper.attack_target.model.getName() != other_infor.model.getName())
						owner.helper.attack_target = other.GetComponent<Hero>();
					break;
				}
			}
		}
		
		if(!isCharacter)
		{
			owner.StateMachine.ChangeState(HeroState_Moving.Instance);
		}

		if(owner.helper.attack_target != null)
		{
			//attack...  & test
			owner.helper.attack_delay_counter += Time.deltaTime;
			if (owner.helper.attack_delay_counter >= 1)
			{
				Message msg = owner.ObtainMessage(MessageTypes.MSG_DAMAGE, new HeroDamageCommand(owner.helper.attack_target));
				owner.DispatchMessage(msg);
				owner.helper.attack_delay_counter = 0;
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
            case MessageTypes.MSG_DAMAGE:
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
