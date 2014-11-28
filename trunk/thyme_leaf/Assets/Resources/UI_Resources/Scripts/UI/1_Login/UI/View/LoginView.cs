using UnityEngine;
using System.Collections;

public class LoginView : MonoBehaviour, IObserver, IView
{
    private LoginController controller;
    private LoginModel model;

    [SerializeField]
    GameObject _id;
    [SerializeField]
    GameObject _passwd;
    [SerializeField]
    GameObject _loginButton;

    /*
     * followings are unity callback methods
     */ 
    void Awake()
    {
        this.model = LoginModel.Instance;
        this.controller = new LoginController(this, LoginModel.Instance);

        this.model.RegisterObserver(this);
    }

    /*
     * followings are member functions
     */ 
    public void setText(string p)
    {
        _id.GetComponentInChildren<UILabel>().text = p;
    }

    private void Login()
    {
        string id = _id.GetComponentInChildren<UILabel>().text;
        string passwd = _passwd.GetComponentInChildren<UILabel>().text;

        Debug.Log(id + "");
        Debug.Log(passwd + "");

        controller.Login(id, passwd);
    }


    /*
     * followings are implemented methods of interface
     */ 
    public void Refresh()
    {
        if (model.IsLogin)
        {
            _id.GetComponentInChildren<UILabel>().text = "Login has succeeded";
            Debug.Log("Login has succeeded");
        }
        else
        {
            _id.GetComponentInChildren<UILabel>().text = "Login has failed";
            Debug.Log("Login has failed");
        }
    }

    public void UpdateUI()
    {
        throw new System.NotImplementedException();
    }

    public void ActionPerformed(string actionCommand)
    {
        if (actionCommand.Equals(_loginButton.name))
        {
            Login();
        }
    }


}
