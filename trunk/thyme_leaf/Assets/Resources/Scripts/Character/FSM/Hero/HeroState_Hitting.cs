using UnityEngine;
using System.Collections;

public class HeroState_Hitting : State<Hero>
{

    private HeroState_Hitting()
    {
        Successor = null;
    }

    public override void Enter(Hero owner)
    {
        Debug.Log("Hitting Enter *********************");
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
        Debug.Log(msg);
        switch (msg.what)
        { 
            case MessageTypes.MSG_MOVE_HERO:
            case MessageTypes.MSG_DAMAGE:
                msg.command.Execute();
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
