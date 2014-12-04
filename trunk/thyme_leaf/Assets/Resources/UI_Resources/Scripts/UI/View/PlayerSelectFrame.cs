using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSelectFrame : MonoBehaviour, IView
{
    private LobbyView view;

    [SerializeField]
    GameObject[] _playerSlots;

    [SerializeField]
    UIButton _closeButton;

    /*
     * followings are unity callback methods
     */
    void Awake()
    {
        view = transform.parent.GetComponent<LobbyView>();
    }

    void Start()
    {
        //initialize();
        UpdateUI();
    }

    private void initialize()
    {
        setUserNames();
    }

    /*
     * following are member functions
     */
    void setUserNames()
    {
        List<User> users = view.Model.Users;

        for (int i = 0; i < users.Count; i++)
        {
            _playerSlots[i].GetComponentInChildren<UILabel>().text = users[i].Name;
        }
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
    }

    /*
     * following are member functions
     */
    public void ActionPerformed(string actionCommand)
    {
        if (actionCommand.Equals(_playerSlots[0].name))
        {
            view.Controller.PrepareLobby(_playerSlots[0].GetComponentInChildren<UILabel>().text);
        }
        else if (actionCommand.Equals(_playerSlots[1].name))
        {
            view.Controller.PrepareLobby(_playerSlots[1].GetComponentInChildren<UILabel>().text);
        }
        else if (actionCommand.Equals(_playerSlots[2].name))
        {
            view.Controller.PrepareLobby(_playerSlots[2].GetComponentInChildren<UILabel>().text);
        }
        else if (actionCommand.Equals(_closeButton.name))
        {
            Close();
        }
    }

    public void UpdateUI()
    {
        initialize();
    }
}