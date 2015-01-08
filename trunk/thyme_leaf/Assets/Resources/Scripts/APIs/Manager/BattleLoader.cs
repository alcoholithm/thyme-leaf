using UnityEngine;
using System.Collections;

public class BattleLoader : Manager<BattleLoader>
{
    protected override void Awake()
    {
        base.Awake();

        User user = UserAdministrator.Instance.CurrentUser;

        if (user == null)
        {
            Debug.Log("AAAAAAAAAaa");
            UserAdministrator.Instance.CurrentUser = new User("test", 1000);
        }
        else
        {
            Debug.Log("BBBBBBBBBB");
            UserAdministrator.Instance.CurrentUser = new User(user.Name, 1000);
        }
    }
}
