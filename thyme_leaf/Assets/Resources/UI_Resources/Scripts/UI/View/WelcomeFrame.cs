using UnityEngine;
using System.Collections;

/// <summary>
/// Facade of WelcomeFrame
/// </summary>
/// // iview 를 상속받아야하나?? 컴포지트로 가야할 듯..
public class WelcomeFrame : MonoBehaviour, IView
{
    private LobbyView view;

    [SerializeField]
    UILabel _userName;

    [SerializeField]
    GameObject _addUserButton;

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

    public void ActionPerformed(string actionCommand)
    {
        if (actionCommand.Equals(_addUserButton.name))
        {
            view.SetVisible(view.PlayerSelectFrame, true);
        }
    }

    public void UpdateUI()
    {
    }
}
