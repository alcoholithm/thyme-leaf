using UnityEngine;
using System.Collections;

public class TowerState_Dying : State<Agt_Type1>
{
    private string animName = "Tower_Dying_";

    private float time = 2f;

    private TowerState_Dying()
    {
        Successor = TowerState_Hitting.Instance;
    }

    /*
     * followings are member functions
     */ 

    /*
     * followings are overrided methods
     */

 
    public override void Enter(Agt_Type1 owner)
    {
        Debug.Log(TAG + "Enter");
        owner.Anim.PlayOneShot(animName, new VoidFunction2<Agt_Type1>(x => x.ChangeState(TowerState_None.Instance), owner));
    }

    public override void Execute(Agt_Type1 owner)
    {
        time -= Time.deltaTime;

        if (time < 0)
        {
            
        }
    }

    public override void Exit(Agt_Type1 owner)
    {
        Debug.Log(TAG + "Exit");
        TowerSpawner.Instance.Free(owner.gameObject);
    }

    public override bool HandleMessage(Message msg)
    {
        throw new System.NotImplementedException();
    }

    /*
     * for singleton
     */
    public new const string TAG = "[TowerState_Dying]";
    private static TowerState_Dying instance = new TowerState_Dying(); // lazy 하게 생성해준다고 한다. 믿어 봐야지 뭐
    public static TowerState_Dying Instance
    {
        get { return TowerState_Dying.instance; }
    }

  
}
