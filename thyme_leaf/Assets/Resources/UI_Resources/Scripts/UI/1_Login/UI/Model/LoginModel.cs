using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public sealed class LoginModel : IObservable {
    public const string TAG = "LoginModel";

    private const string ID = "test";
    private const string PASSWD = "1234";

	public string testt = "test";

    private static LoginModel instance = new LoginModel();
    public static LoginModel Instance { get { return instance; } }

    private bool isLogin;
    public bool IsLogin
    {
        get { return isLogin; }
        set { isLogin = value; }
    }

	public void shoot() {
		testt = "change";
		Debug.Log (testt);
	}

    private LoginModel()
    {
        observers = new List<IObserver>();
        isLogin = false;
    }

    /*
     * followings are member functions
     */
    public void Login(string id, string passwd)
    {
        // networking... thread
        // need proxy
        if (id.Equals(ID) && passwd.Equals(PASSWD))
        {
            SuccessToLogin();
            NotifyObservers();
        }
        else
        {
            Debug.Log("Failed to login");
			shoot ();
        }
    }

    void SuccessToLogin()
    {
        isLogin = true;
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
        //observers.ForEach(delegate(IObserver o)
        //{
        //    o.Update(this);
        //});

        foreach (IObserver o in observers)
        {
            o.Refresh();
        }
    }

    public void HasChanged()
    {
        throw new System.NotImplementedException();
    }

    public void SetChanged()
    {
        throw new System.NotImplementedException();
    }
}
