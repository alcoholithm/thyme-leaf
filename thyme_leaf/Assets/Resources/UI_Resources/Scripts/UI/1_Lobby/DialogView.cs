using UnityEngine;
using System.Collections;

public class DialogView : MonoBehaviour, IView
{
    //private LobbyController controller;
    //private UserAdministrator model;

    [SerializeField]
    GameObject _messageDialog;

    void Awake()
    {
        instance = this;
    }

    public void ShowMessageDialog(string message)
    {
        _messageDialog.GetComponent<MessageDialog>().SetMessage(message);
        SetVisible(_messageDialog, true);
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

    public const string TAG = "[DialogView]";
    private static DialogView instance;
    public static DialogView Instance
    {
        get { return DialogView.instance; }
    }
}
