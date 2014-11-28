using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UserAdministrator : IUserAdministrator, IObservable_User
{
    private List<IObserver_User> observers;
    private List<User> users;

    private int nUserMax = 3;

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


    /*
    * followings are implemented methods of interface
    */
    public bool RegisterUser(string userName)
    {
        if (users.Count >= nUserMax)
            return false;

        users.Add(new User(userName));
        return true;
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

    public const string TAG = "[UserAdministrator]";
    private static UserAdministrator instance = new UserAdministrator();
    public static UserAdministrator Instance
    {
        get { return instance; }
    }
}
