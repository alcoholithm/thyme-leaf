using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSelectFrame : MonoBehaviour, IView
{
    private LobbyView view;

    [SerializeField]
    private GameObject[] _playerSlots;

    [SerializeField]
    private UIButton _renameButton;

    [SerializeField]
    private UIButton _deleteButton;

    [SerializeField]
    private UIButton _closeButton;

    /*
     * followings are unity callback methods
     */
    void Awake()
    {
        view = transform.parent.GetComponent<LobbyView>();
    }

    void OnEnable()
    {
        UpdateUI();
    }


    /*
     * following are member functions
     */
    private void initialize()
    {
        setUserNames();
    }
    private void setUserNames()
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

    public void isClick(int num)
    {
        for (int i = 0; i < 3; i++ )
        {
            if( i == num)
                _playerSlots[num].transform.GetChild(1).gameObject.SetActive(true);
            else
                _playerSlots[i].transform.GetChild(1).gameObject.SetActive(false);
        }
    }



    /*
     * following are overrided methods
     */
    public void ActionPerformed(string actionCommand)
    {
        if (actionCommand.Equals(_playerSlots[0].name))
        {
            isClick(0);
            //view.Controller.PrepareLobby(_playerSlots[0].GetComponentInChildren<UILabel>().text);
        }
        else if (actionCommand.Equals(_playerSlots[1].name))
        {
            isClick(1);
            //view.Controller.PrepareLobby(_playerSlots[1].GetComponentInChildren<UILabel>().text);
        }
        else if (actionCommand.Equals(_playerSlots[2].name))
        {
            isClick(2);
            //view.Controller.PrepareLobby(_playerSlots[2].GetComponentInChildren<UILabel>().text);
        }
        else if(actionCommand.Equals(_renameButton.name))
        {
            int userCnt = view.Model.Users.Count;
            int userMax = view.Model.NUserMax;

            if (userCnt < userMax)
                view.Controller.RenameAdd();
            else
                DialogView.Instance.ShowMessageDialog("Exceed number!");
        }
        else if(actionCommand.Equals(_deleteButton.name))
        {
            DialogView.Instance.ShowMessageDialog("Really Delete?");
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

    /*
     * 
     */
    public const string TAG = "[PlayerSelectFrame]";
}