using UnityEngine;
using System.Collections;

public class TowerState_Dying : State<Agt_Type1>
{
    private string animName = "Tower_Dying_";

    private TowerState_Dying()
    {
        Successor = TowerState_Hitting.Instance;
    }


    /*
     * followings are overrided methods of "State"
     */
    public override void Enter(Agt_Type1 owner)
    {
        Debug.Log(TAG + "Enter");
        owner.Anim.PlayOneShot(animName, new VoidFunction2<Agt_Type1>(x => x.ChangeState(TowerState_None.Instance), owner));
    }

    public override void Execute(Agt_Type1 owner)
    {
    }

    public override void Exit(Agt_Type1 owner)
    {
        Debug.Log(TAG + "Exit");

        //temporary code
        owner.gameObject.transform.parent.tag = Tag.TagTowerSpot;

        Spawner.Instance.PerfectFree(owner.gameObject);
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
