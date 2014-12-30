using UnityEngine;
using System.Collections;

/// <summary>
/// Facade of WelcomeFrame
/// </summary>
public class WelcomeFrame : View, IActionListener
{
    private LobbyView view;

    [SerializeField]
    private UILabel _userName;
    [SerializeField]
    private GameObject _addUserButton;

    void Awake()
    {
        view = transform.parent.GetComponent<LobbyView>();
    }

    void OnEnable()
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

    public void ActionPerformed(GameObject source)
    {
        if (source.name.Equals(_addUserButton.name))
        {
            view.SetVisible(view.PlayerSelectFrame, true);
        }
    }

    public override void UpdateUI()
    {
        throw new System.NotImplementedException();
    }
}
