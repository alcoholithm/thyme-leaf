using UnityEngine;
using System.Collections;

public class BattleLoader : Manager<BattleLoader>
{

    //test
    [SerializeField]
    public GameObject flame;

    void Awake()
    {
        base.Awake();
        UserAdministrator.Instance.CurrentUser = new User("Test", 1000);
    }
}
