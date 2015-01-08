using UnityEngine;
using System.Collections;

public class DialogFacade : MonoBehaviour
{
    //private LobbyController controller;

    //[SerializeField]
    //private LobbyView _view;
    
    //private UserAdministrator model;

    [SerializeField]
    private GameObject _messageDialog;

    [SerializeField]
    private GameObject _confirmDialog;

    [SerializeField]
    private GameObject _inputDialog;


    /*
     * Followings are unity callback methods
     */ 
    void Awake()
    {
        Initialize();
    }

    /*
     * Followings are member functions
     */ 
    private void Initialize()
    {
        instance = this;
        //this.controller = _view.Controller;
    }

    /* MessageDialog */
    public void ShowMessageDialog(string message)
    {
        _messageDialog.GetComponent<MessageDialog>().SetMessage(message);
        _messageDialog.SetActive(true);
    }
    public void ChangeMsgDialogTitle(string title)
    {
        _messageDialog.GetComponent<MessageDialog>().SetTitle(title);
    }
    public void ChangeMsgDialogBtnText(string btnText)
    {
        _messageDialog.GetComponent<MessageDialog>().SetBtnText(btnText);
    }
    public void CloseMessageDialog()
    {
        _messageDialog.SetActive(false);
    }

    /* ConfirmDialog */
    public void ShowConfirmDialog(string message)
    {
        _confirmDialog.GetComponent<ConfirmDialog>().SetMessage(message);
        _confirmDialog.SetActive(true);
    }
    public void CloseConfirmDialog()
    {
        _confirmDialog.SetActive(false);
    }

    /* InputDialog */
    public void ShowInputDialog()
    {
        _inputDialog.SetActive(true);
    }
    public void CloseInputDialog()
    {
        _inputDialog.SetActive(false);
    }


    public void SetVisible(GameObject gameObject, bool active)
    {
        gameObject.SetActive(active);
    }

    /*
     * Followings are attributes
     */ 
    private static DialogFacade instance;
    public static DialogFacade Instance
    {
        get { return DialogFacade.instance; }
        set { DialogFacade.instance = value; }
    }

    public const string TAG = "[DialogFacade]";
}
