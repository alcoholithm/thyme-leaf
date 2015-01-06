using UnityEngine;
using System.Collections;

public class TowerState_None : State<AutomatTower>
{
    private TowerState_None()
    {
        Successor = TowerState_Hitting.Instance;
    }

    /*
     * Followings are overrided methods of "State"
     */
    public override void Enter(AutomatTower owner){}
    public override void Execute(AutomatTower owner){}
    public override void Exit(AutomatTower owner) { }
    public override bool HandleMessage(Message msg) {return false;}

    /*
     * 
     */ 
    private static TowerState_None instance = new TowerState_None(); // lazy 하게 생성해준다고 한다. 믿어 봐야지 뭐
    public static TowerState_None Instance
    {
        get { return TowerState_None.instance; }
        set { TowerState_None.instance = value; }
    }

    public new const string TAG = "[TowerState_None]";
}
