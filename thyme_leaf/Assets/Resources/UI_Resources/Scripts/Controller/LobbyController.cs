using UnityEngine;
using System.Collections;

public class LobbyController
{
    private UserAdministrator model;
    private LobbyView view;

    public LobbyController(LobbyView view, UserAdministrator model)
    {
        this.model = model;
        this.view = view;
    }

    // 여기서 비즈니스 로직을 구현하면 안 된다!
    // 요청만 전달해야한다!
    // 뷰를 제어하기 위한 동작은 괜춘

    /*
    * followings are public functions
    */
    public void Start()
    {
        view.SetVisible(view.StartButton, false);
        if (model.IsEmpty())
            view.SetVisible(view.RegisterUserFrame, true);
        else
            view.SetVisible(view.PlayerSelectFrame, true);
    }

    public void Register(string userName)
    {
        string str;
        if (model.RegisterUser(userName))
        {
            str = "Welcome!!";
            view.SetVisible(view.RegisterUserFrame, false);
            view.SetVisible(view.PlayerSelectFrame, true);
        }
        else
        {
            //str = model.Status();
            str = "error!!";
        }
        DialogView.Instance.ShowMessageDialog(str);
        //view.StartCoroutine(model.Login(id, passwd));
    }

    public void PrepareLobby(string userName)
    {
        User currUser = model.Users.Find(user => user.Name.Equals(userName));
        model.CurrentUser = currUser;
        view.SetVisible(view.PlayerSelectFrame, false);
        view.PrepareLobby();
    }

    public const string TAG = "[LoginController]";
}