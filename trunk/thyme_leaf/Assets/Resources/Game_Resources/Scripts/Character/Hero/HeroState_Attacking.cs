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
		if((owner.gameObject.layer == Layer.Automart())){
			owner.PlayAnimation("Comma_Attacking_Normal_");
		}else if((owner.gameObject.layer == Layer.Trovant())) {
			owner.PlayAnimation("Python_Attacking_Normal_");
		}

        owner.helper.attack_delay_counter = 0;  //attack delay setting...
    }

    public override void Execute(Hero owner)
    {
        owner.helper.attack_delay_counter += Time.deltaTime;
        if (owner.helper.attack_delay_counter >= 1)
        {
            Message msg = owner.ObtainMessage(MessageTypes.MSG_DAMAGE, new HeroDamageCommand(owner.target));
            owner.DispatchMessage(msg);
            owner.helper.attack_delay_counter = 0;
        }

        //if.. target is null -> mode change moving state...
        owner.TargetingInitialize();
    }

    public override void Exit(Hero owner)
    {
        //Debug.Log("Attack  Exit ************************");
        //throw new System.NotImplementedException ();
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
