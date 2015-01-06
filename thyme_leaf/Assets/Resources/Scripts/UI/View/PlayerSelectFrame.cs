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

    [SerializeField]
    private UILabel _newName;

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

        Debug.Log(users.Count);

        for (int i = 0; i < users.Count; i++)
        {
            _playerSlots[i].GetComponentInChildren<UILabel>().text = users[i].Name;
        }
    }

    private void Close()
    {
        this.gameObject.SetActive(false);
    }

    private void isClick(int num)
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

    private void isEmpty(int num)
    {
        string slotText;
        slotText = _playerSlots[num].GetComponentInChildren<UILabel>().text;
        slotText = slotText.ToLower();

        if( slotText.Equals("empty"))
        {
            view.Controller.PrepareLobby(_playerSlots[num].GetComponentInChildren<UILabel>().text);
        }
    }

    public void RenameClick()
    {
        if (!(string.IsNullOrEmpty(_newName.text)))
            view.Controller.RenameFunc(_newName.text, ClickFlag);

        DialogFacade.Instance.CloseInputDialog();
    }

    public void DeleteClick()
    {
        if (view.Controller.DeleteNameFunc(_playerSlots[ClickFlag].GetComponentInChildren<UILabel>().text))
        {
            DialogFacade.Instance.CloseConfirmDialog();
            DialogFacade.Instance.ShowMessageDialog("Success Delete");

            _playerSlots[ClickFlag].GetComponentInChildren<UILabel>().text = "EMPTY";
            initialize();
        }
        else
        {
            DialogFacade.Instance.CloseConfirmDialog();
            DialogFacade.Instance.ShowMessageDialog("Fail, Try again");
        }
    }


    /*
     * following are overrided methods
     */
    public void ActionPerformed(GameObject source)
    {
        if (source.name.Equals(_playerSlots[0].name))
        {
            isClick(0);
            isEmpty(0);
        }
        else if (source.name.Equals(_playerSlots[1].name))
        {
            isClick(1);
            isEmpty(1);
        }
        else if (source.name.Equals(_playerSlots[2].name))
        {
            isClick(2);
            isEmpty(2);
        }
        else if (source.name.Equals(_renameButton.name))
        {
            DialogFacade.Instance.ShowInputDialog();
        }
        else if (source.name.Equals(_deleteButton.name))
        {
            DialogFacade.Instance.ShowConfirmDialog("Really Delete?");
        }
        else if (source.name.Equals(_closeButton.name))
        {
            Close();
        }
        else
        {
            Debug.Log("같지않다");
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