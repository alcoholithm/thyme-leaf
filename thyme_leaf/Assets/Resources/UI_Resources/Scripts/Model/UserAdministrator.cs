using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class UserAdministrator : IUserAdministrator, IObservable
{
    private Dictionary<ObserverTypes, List<IObserver>> observers;
    private List<User> users;

    public List<User> Users
    {
        get { return users; }
        set { users = value; }
    }

    private int nUserMax = 3;
    public int NUserMax
    {
        get { return nUserMax; }
        set { nUserMax = value; }
    }

    private User currentUser;
    public User CurrentUser
    {
        get { return currentUser; }
        set { currentUser = value; }
    }

    private string statusMessage;

    private UserAdministrator()
    {
        this.observers = new Dictionary<ObserverTypes, List<IObserver>>();
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
        if (users.Count < nUserMax
            && userName != null
            && userName.Length > 0
            && users.Find(user => user.Name.Equals(userName)) == null)
        {
            users.Add(new User(userName));
            return true;
        }
        return false;
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
    public void RegisterObserver(IObserver o, ObserverTypes field)
    {
        if (!observers.ContainsKey(field))
        {
            observers.Add(field, new List<IObserver>());
        }
        observers[field].Add(o);
    }

    public void RemoveObserver(IObserver o, ObserverTypes field)
    {
        if (observers[field].Count <= 1)
            observers.Remove(field);
        else
            observers[field].Remove(o);
    }

    public void NotifyObservers(ObserverTypes field)
    {
        observers[field].ForEach(o => o.Refresh(field));
    }

    public void HasChanged()
    {
        throw new NotImplementedException();
    }

    public void SetChanged()
    {
        throw new NotImplementedException();
    }

    /*
     * 
     */ 
    public const string TAG = "[UserAdministrator]";
    private static UserAdministrator instance = new UserAdministrator();
    public static UserAdministrator Instance
    {
        get { return instance; }
    }


}
