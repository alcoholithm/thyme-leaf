using UnityEngine;
using System.Collections;

/// <summary>
/// Facade of WelcomeFrame
/// </summary>
/// // iview 를 상속받아야하나?? 컴포지트로 가야할 듯..
public class WelcomeFrame : MonoBehaviour
{
    private LobbyView view;

    [SerializeField]
    UILabel _userName;

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
        User user = view.Model.CurrentUser;
        SetUserName(user.Name);
    }

    void SetUserName(string name)
    {
        _userName.text = name;
    }
}
