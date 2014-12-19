﻿using UnityEngine;
using System.Collections;

public class TowerState_Hitting : State<ATT_Type1>
{
    //private string animName = "Tower_Hitting_";

    private TowerState_Hitting()
    {
        Successor = null;
    }

    /*
     * followings are overrided methods
     */

    // 여긴 불리지 않을꺼야
    public override void Enter(ATT_Type1 owner)
    {
        Debug.Log(TAG + " Enter");
    }

    // -주의- 전역상태이기 때문에 계속 실행된다.
    public override void Execute(ATT_Type1 owner)
    {
        //Debug.Log(TAG);
        
        //testTime -= Time.deltaTime;

        //if (testTime < 0)
        //{
        //    Debug.Log("RevertToPreviousState");


        //    // 애매하다.
        //    owner.RevertToPreviousState();
        //}

        owner.CheckDeath();
    }

    public override void Exit(ATT_Type1 owner)
    {
        Debug.Log(TAG + " Exit");
    }

    public override bool HandleMessage(Message msg)
    {
        switch (msg.what)
        {
            case MessageTypes.MSG_DAMAGE:
                (msg.receiver as ATT_Type1).TakeDamage(msg.arg1);
                return true;
        }

        return false;
    }

    /*
     * for singleton
     */
    public new const string TAG = "[TowerState_Hitting]";
    private static TowerState_Hitting instance = new TowerState_Hitting(); // lazy 하게 생성해준다고 한다. 믿어 봐야지 뭐
    public static TowerState_Hitting Instance
    {
        get { return instance; }
        set { instance = value; }
    }
}
