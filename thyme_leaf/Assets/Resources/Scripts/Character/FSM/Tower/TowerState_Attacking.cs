using UnityEngine;
using System.Collections;

public class TowerState_Attacking : State<AutomatTower>
{
    private string animName = "Tower_Attacking_";

    private TowerState_Attacking()
    {
        Successor = TowerState_Hitting.Instance;
    }


    /*
     * followings are overrided methods
     */
    public override void Enter(AutomatTower owner)
    {
        Debug.Log(TAG + " Enter");

        animName = Naming.Instance.BuildAnimationName(owner.gameObject, Naming.ATTACKING) + "_";
        Debug.Log(animName);

        owner.Anim.Pause();
        owner.Anim.namePrefix = animName;
        owner.Anim.framesPerSecond = (int)((owner.Anim.frames / owner.Model.ReloadingTime) + 0.5f); // 반올림
        owner.Anim.Play(animName, new VoidFunction(() => owner.Controller.Attack()));
    }

    public override void Execute(AutomatTower owner)
    {
        // if there are no enemies anymore.
        owner.Model.TrimEnemies();

        if (!owner.Model.HasMoreEnemies())
        {
            owner.ChangeState(TowerState_Idling.Instance);
            return;
        }
    }

    public override void Exit(AutomatTower owner)
    {
        Debug.Log(TAG + " Exit");
        owner.Anim.Pause();
        owner.Anim.ClearCommand();
    }

    public override bool HandleMessage(Message msg)
    {
        return false;
    }

    /*
     * Followings are attributes
     */
    public new const string TAG = "[TowerState_Attacking]";
    private static TowerState_Attacking instance = new TowerState_Attacking(); // lazy 하게 생성해준다고 한다. 믿어 봐야지 뭐
    public static TowerState_Attacking Instance
    {
        get { return TowerState_Attacking.instance; }
    }
}
