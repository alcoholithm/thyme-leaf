using UnityEngine;
using System.Collections;

public class TowerState_None : State<Tower>
{
    private static TowerState_None instance = new TowerState_None(); // lazy 하게 생성해준다고 한다. 믿어 봐야지 뭐
    public static TowerState_None Instance
    {
        get { return TowerState_None.instance; }
        set { TowerState_None.instance = value; }
    }

    private TowerState_None() { }

    public void Enter(Tower owner)
    {
    }

    public void Execute(Tower owner)
    {
    }

    public void Exit(Tower owner)
    {
    }

    //void Awake()
    //{
    //    owner = GetComponent<Tower>();
    //}

    //void OnEnable()
    //{
    //}

    //void Update()
    //{
    //}

    //void OnDisable()
    //{
    //}
}