using UnityEngine;
using System.Collections;

public class HeroState_None : State<Hero>
{
    private HeroState_None()
    {
        Successor = HeroState_Hitting.Instance;
    }

    /*
     * Followings are overrided methods of "State"
     */
    public override void Enter(Hero owner) { }
    public override void Execute(Hero owner) { }
    public override void Exit(Hero owner) { }
    public override bool HandleMessage(Message msg) {return false;}

    /*
     * 
     */
    private static HeroState_None instance = new HeroState_None(); // lazy 하게 생성해준다고 한다. 믿어 봐야지 뭐
    public static HeroState_None Instance
    {
        get { return HeroState_None.instance; }
        set { HeroState_None.instance = value; }
    }

    public new const string TAG = "[HeroState_None]";
}
