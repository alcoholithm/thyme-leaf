using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoginModel : MonoBehaviour, ILoginModel, IObservable
{
    private const string CORRECT_ID = "test";
    private const string CORRECT_PASSWD = "1234";

    private bool isLogin;

    private LoginModel()
    {
        observers = new List<IObserver>();
        isLogin = false;
    }

    /*
     * followings are member functions
     */
    public IEnumerator Login(string id, string passwd)
    {
        // networking... thread
        // need proxy

        // wwwscript같은 네트워크 클래스에게 요청을 위임한다.
        yield return new WaitForSeconds(3);

        if (id.Equals(CORRECT_ID) && passwd.Equals(CORRECT_PASSWD))
        {
            SuccessToLogin();
        }
        else
        {
            FailedToLogin();
        }

        NotifyObservers();
    }

    void SuccessToLogin()
    {
        isLogin = true;
    }

    void FailedToLogin()
    {
        isLogin = false;
    }

    /*
     * followings are Observer pattern methods.
     */
    private List<IObserver> observers; // 각 옵저버 상태 구분은 state pattern
    public void RegisterObserver(IObserver o)
    {
        observers.Add(o);
    }

    public void RemoveObserver(IObserver o)
    {
        observers.Remove(o);
    }

    public void NotifyObservers()
    {
        observers.ForEach(delegate(IObserver o)
        {
            o.Refresh();
        });
    }

    public void HasChanged()
    {
        throw new System.NotImplementedException();
    }

    public void SetChanged()
    {
        throw new System.NotImplementedException();
    }

    public bool IsLogin
    {
        get { return isLogin; }
        set { isLogin = value; }
    }

    public const string TAG = "LoginModel";
    private static LoginModel instance = new LoginModel();
    public static LoginModel Instance
    {
        get { return instance; }
    }
}
