using UnityEngine;
using System.Collections;

public class DialogFacade : Singleton<DialogFacade>
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

    void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        //this.controller = _view.Controller;
    }

    public void ShowMessageDialog(string message)
    {
        _messageDialog.GetComponent<MessageDialog>().SetMessage(message);
        _messageDialog.SetActive(true);
    }
    
    public void ShowConfirmDialog(string message)
    {
        _confirmDialog.GetComponent<ConfirmDialog>().SetMessage(message);
        _confirmDialog.SetActive(true);
    }
    public void CloseConfirmDialog()
    {
        _confirmDialog.SetActive(false);
    }

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


    public new const string TAG = "[DialogFacade]";
}
