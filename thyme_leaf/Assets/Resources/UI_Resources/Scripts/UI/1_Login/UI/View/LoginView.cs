using UnityEngine;
using System.Collections;

public class LoginView : MonoBehaviour, IObserver {
    public GameObject _id;
    public GameObject _passwd;

    private LoginController controller;
    private LoginModel model;

    void Awake()
    {
        this.model = LoginModel.Instance;
        this.controller = new LoginController(this);

        this.model.RegisterObserver(this);
    }

    //view에서는 view control의 제어만 담당하면 된다.

    // 이런거는 컨트롤러 안에 들어가야 맞는 것
    //public void Login()
    //{
    //    string id = _id.GetComponentInChildren<UILabel>().text;
    //    string passwd = _passwd.GetComponentInChildren<UILabel>().text;

    //    Debug.Log(id + "");
    //    Debug.Log(passwd + "");
        
    //    controller.Login(id, passwd);
    //}

    void paint()
    {

    }

    public void FFF(ICommand command)
    {
        command.Execute();
    }

    public void repaint()
    {

    }

    public void Refresh()
    {
        if (model.IsLogin)
        {
            _id.GetComponentInChildren<UILabel>().text = "Login has succeeded";
            Debug.Log("Login has succeeded");
			// Next Scene Transmit
            GameObject.Find("Manager").GetComponent<SceneManager>().CurrentScene = SceneManager.LOBBY;
            Debug.Log("scene loading...");
        }
    }
}
