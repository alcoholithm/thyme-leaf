using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSelectFrame : MonoBehaviour
{
    [SerializeField]
    LobbyView _view;

    [SerializeField]
    UILabel[] _userName;

    void Start()
    {
        List<User> users = (_view.Model as UserAdministrator).Users;

        for (int i = 0; i < users.Count; i++)
        {
            _userName[i].text = users[i].Name;
        }
    }
}