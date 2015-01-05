using UnityEngine;
using System.Collections;

public class BattleLoader : MonoBehaviour
{
    void Awake()
    {
        UserAdministrator.Instance.CurrentUser = new User("Test", 1000);
    }
}
