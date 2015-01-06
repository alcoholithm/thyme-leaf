using UnityEngine;
using System.Collections;

public class BattleLoader : Manager<BattleLoader>
{
    void Awake()
    {
        base.Awake();
        UserAdministrator.Instance.CurrentUser = new User("Test", 1000);
    }
}
