using UnityEngine;
using System.Collections;

public class LoginController {
    
    private LoginModel model;
    private LoginView view;

    public LoginController(LoginView view)
    {
        model = LoginModel.Instance;
        this.view = view;
    }

    public void Login(string id, string passwd)
    {
        if (!model.IsLogin)
            model.Login(id, passwd);
        else {
            Debug.Log("Already has been login.");
		}
    }  
}