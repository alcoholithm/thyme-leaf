using UnityEngine;
using System.Collections;

public class TowerState_Dying : State<AutomatTower>
{
    private string animName = "Tower_Dying_";

    private TowerState_Dying()
    {
        Successor = TowerState_Hitting.Instance;
    }


    /*
     * followings are overrided methods of "State"
     */
    public override void Enter(AutomatTower owner)
    {
        Debug.Log(TAG + "Enter");
        owner.Anim.PlayOneShot(animName, new VoidFunction2<AutomatTower>(x => x.ChangeState(TowerState_None.Instance), owner));
    }

    public override void Execute(AutomatTower owner)
    {
    }

    public override void Exit(AutomatTower owner)
    {
        Debug.Log(TAG + "Exit");

        //temporary code
        owner.gameObject.transform.parent.tag = Tag.TagTowerSpot;

        Spawner.Instance.Free(owner.gameObject);
    }

    public override bool HandleMessage(Message msg)
    {
        throw new System.NotImplementedException();
    }

    /*
     * for singleton
     */
    private static TowerState_Dying instance = new TowerState_Dying(); // lazy 하게 생성해준다고 한다. 믿어 봐야지 뭐
    public static TowerState_Dying Instance
    {
        get { return TowerState_Dying.instance; }
    }

    public new const string TAG = "[TowerState_Dying]";
}
