using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSelectFrame : View, IActionListener
{
    private int ClickFlag;

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
        ClickFlag = num;
        for (int i = 0; i < 3; i++ )
        {
            if( i == num)
                _playerSlots[num].transform.GetChild(1).gameObject.SetActive(true);
            else
                _playerSlots[i].transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    public void isEmpty(int num)
    {
        string slotText;
        slotText = _playerSlots[num].GetComponentInChildren<UILabel>().text;
        slotText = slotText.ToLower();

        if( slotText.Equals("empty"))
        {
            view.Controller.PrepareLobby(_playerSlots[num].GetComponentInChildren<UILabel>().text);
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
            isEmpty(0);
        }
        else if (actionCommand.Equals(_playerSlots[1].name))
        {
            isClick(1);
            isEmpty(1);
        }
        else if (actionCommand.Equals(_playerSlots[2].name))
        {
            isClick(2);
            isEmpty(2);
        }
        else if(actionCommand.Equals(_renameButton.name))
        {
            view.Controller.RenameFunc(_playerSlots[ClickFlag].GetComponentInChildren<UILabel>().text,ClickFlag);
        }
        else if(actionCommand.Equals(_deleteButton.name))
        {
            DialogFacade.Instance.ShowMessageDialog("Really Delete?");
        }
        else if (actionCommand.Equals(_closeButton.name))
        {
            Close();
        }
    }

    public override void UpdateUI()
    {
        initialize();
    }

    /*
     * 
     */
    public const string TAG = "[PlayerSelectFrame]";
}