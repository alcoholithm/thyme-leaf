using UnityEngine;
using System.Collections;

public class HeroState_Hitting : State<Hero>
{
    private HeroState_Hitting()
    {
        Successor = null;
    }

    /*
     * Followings are overrided methods of "State"
     */ 
    public override void Enter(Hero owner)
    {
        //Debug.Log("Hitting Enter *********************");
        // throw new System.NotImplementedException ();
    }

    public override void Execute(Hero owner)
    {

    }

    public override void Exit(Hero owner)
    {
        //		Debug.Log("Hitting  Exit *********************");
        // throw new System.NotImplementedException ();
    }

    public override bool HandleMessage(Message msg)
    {
		//ok...
        switch (msg.what)
        {
            case MessageTypes.MSG_NORMAL_DAMAGE:
                (msg.receiver as Hero).TakeDamage(msg.arg1);
                return true;
            case MessageTypes.MSG_POISON_DAMAGE:
                Hero receriver = msg.receiver as Hero;
                if (!receriver.Views.Contains(receriver.FxPoisoning))
                    receriver.Add(receriver.FxPoisoning);
                receriver.TakeDamage(msg.arg1);
                return true;
            case MessageTypes.MSG_BURN_DAMAGE:
                receriver = msg.receiver as Hero;
                if (!receriver.Views.Contains(receriver.FxBurn))
                    receriver.Add(receriver.FxBurn);
                receriver.TakeDamage(msg.arg1);
                return true;
        }
        return false;
    }

    private static HeroState_Hitting instance = new HeroState_Hitting();
    public static HeroState_Hitting Instance
    {
        get { return HeroState_Hitting.instance; }
        set { HeroState_Hitting.instance = value; }
    }

}
