﻿using UnityEngine;
using System.Collections;

public class HeroState_Chasing : State<Hero>
{

    private HeroState_Chasing()
    {
        Successor = HeroState_Hitting.Instance;
    }

    public override void Enter(Hero owner)
    {
        Debug.Log("Chasing Enter");
    }

    public override void Execute(Hero owner)
    {
        Debug.Log("Chasing Execute");
    }

    public override void Exit(Hero owner)
    {
        Debug.Log("Chasing Exit");
    }

    public override bool HandleMessage(Message msg)
    {
        throw new System.NotImplementedException();
    }

    private static HeroState_Chasing instance = new HeroState_Chasing();
    public static HeroState_Chasing Instance
    {
        get { return HeroState_Chasing.instance; }
        set { HeroState_Chasing.instance = value; }
    }

}
