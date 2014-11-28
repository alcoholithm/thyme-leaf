using UnityEngine;
using System.Collections;

public class LoginController
{
    private LoginModel model;
    private LoginView view;

    public LoginController(LoginView view, LoginModel model)
    {
        this.model = model;
        this.view = view;
    }

    // 여기서 비즈니스 로직을 구현하면 안 된다!
    // 요청만 전달해야한다!
    public void Login(string id, string passwd)
    {
        view.setText("loging in...");
        view.StartCoroutine(model.Login(id, passwd));
    }

    public const string TAG = "[LoginController]";
}