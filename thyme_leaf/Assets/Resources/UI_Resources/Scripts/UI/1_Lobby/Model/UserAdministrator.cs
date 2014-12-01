using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UserAdministrator : IUserAdministrator, IObservable_User
{
    private List<IObserver_User> observers;
    private List<User> users;

    public List<User> Users
    {
        get { return users; }
        set { users = value; }
    }

    private int nUserMax = 3;

    private User currentUser;
    public User CurrentUser
    {
        get { return currentUser; }
        set { currentUser = value; }
    }

    private string statusMessage;

    private UserAdministrator()
    {
        this.observers = new List<IObserver_User>();
        this.users = new List<User>();
    }
    // networking... thread
    // need proxy
    // wwwscript같은 네트워크 클래스에게 요청을 위임한다.

    /*
    * followings are implemented methods of interface
    */
    public bool RegisterUser(string userName)
    {
        bool result = false;
        if (userName != null && userName.Length > 0)
        {
            if (users.Count >= nUserMax)
                result = false;
            else
                users.Add(new User(userName));

            result = true;
        }

        return result;
    }

    public bool RemoveUser(string userName)
    {
        throw new System.NotImplementedException();
    }

    public bool RenameUser(string oldOne, string newOne)
    {
        throw new System.NotImplementedException();
    }

    public bool IsEmpty()
    {
        return users.Count == 0;
    }

    /*
    * followings are Observer pattern methods.
    */
    public void RegisterObserver(IObserver_User o)
    {
        observers.Add(o);
    }

    public void RemoveObserver(IObserver_User o)
    {
        observers.Remove(o);
    }

    public void NotifyObservers<TObserver>()
    {
        observers.ForEach(delegate(IObserver_User o)
        {
            o.Refresh<TObserver>();
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

    public const string TAG = "[UserAdministrator]";
    private static UserAdministrator instance = new UserAdministrator();
    public static UserAdministrator Instance
    {
        get { return instance; }
    }
}
