using UnityEngine;
using System.Collections;

public class DialogView : Singleton<DialogView>, IView
{
    private LobbyController controller;

    [SerializeField]
    private LobbyView _view;
    
    //private UserAdministrator model;

    [SerializeField]
    private GameObject _messageDialog;

    [SerializeField]
    private GameObject _confirmDialog;

    void Awake()
    {
        controller = _view.Controller;
    }

    public void ShowMessageDialog(string message)
    {
        _messageDialog.GetComponent<MessageDialog>().SetMessage(message);
        SetVisible(_messageDialog, true);
    }
    public void ShowConfirmDialog(string message)
    {
        _confirmDialog.GetComponent<ConfirmDialog>().SetMessage(message);
        SetVisible(_confirmDialog, true);
    }

    public void SetVisible(GameObject gameObject, bool active)
    {
        gameObject.SetActive(active);
    }

    public void ActionPerformed(string ActionCommand)
    {
        throw new System.NotImplementedException();
    }

    public void UpdateUI()
    {
        throw new System.NotImplementedException();
    }

    public void DeleteTemp()
    {
        Debug.Log("click!");
        controller.DeleteNameFunc("A");
    }

    //public IController Controller
    //{
    //    get
    //    {
    //        throw new System.NotImplementedException();
    //    }
    //    set
    //    {
    //        throw new System.NotImplementedException();
    //    }
    //}

    //public IModel Model
    //{
    //    get
    //    {
    //        throw new System.NotImplementedException();
    //    }
    //    set
    //    {
    //        throw new System.NotImplementedException();
    //    }
    //}
    public const string TAG = "[DialogView]";
}
