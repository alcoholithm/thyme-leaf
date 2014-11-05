using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LobbyModel : IObservable {
	public const string TAG = "LoginModel";


    protected static LobbyModel instance = new LobbyModel();
    public static LobbyModel Instance { get { return instance; } }

    private LobbyModel()
    {
        observers = new List<IObserver>();
    }


    /*
     * followings are member functions
     */

    public void GameStart()
    {
        Debug.Log("do something...");
        NotifyObservers();
    }

    public void Settings()
    {
        NotifyObservers();
    }






    /*
     * followings are member functions
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
}
