using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSelectFrame : MonoBehaviour
{
    private LobbyView view;

    [SerializeField]
    UILabel[] _userName;

    void Awake()
    {
        view = transform.parent.GetComponent<LobbyView>();
    }

    void Start()
    {
        initialize();
    }

    void initialize()
    {
        setUserNames();
    }

    void setUserNames()
    {
        List<User> users = view.Model.Users;

        for (int i = 0; i < users.Count; i++)
        {
            _userName[i].text = users[i].Name;
        }
    }
}