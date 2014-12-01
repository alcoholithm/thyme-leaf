using UnityEngine;
using System.Collections;

/// <summary>
/// Facade of WelcomeFrame
/// </summary>
/// // iview 를 상속받아야하나?? 컴포지트로 가야할 듯..
public class WelcomeFrame : MonoBehaviour
{
    [SerializeField]
    LobbyView _view;

    [SerializeField]
    UILabel _userName;

    void Start()
    {
        User user = (_view.Model as UserAdministrator).CurrentUser;
        SetUserName(user.Name);
    }

    public void SetUserName(string name)
    {
        Debug.Log(name);
        _userName.text = name;
    }
}
