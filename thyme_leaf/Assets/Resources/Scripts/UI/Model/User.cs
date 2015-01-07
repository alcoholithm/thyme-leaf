using UnityEngine;
using System.Collections.Generic;

public class User : IObservable
{
    private string name;
    private int gold;

    // observers
    private Dictionary<ObserverTypes, List<IObserver>> observers =
        new Dictionary<ObserverTypes, List<IObserver>>();

    public User(string name)
    {
        this.name = name;
    }

    public User(string name, int gold)
    {
        this.name = name;
        this.gold = gold;
    }

    /*
    * Followings are public member functions
    */
    public bool HasEnoughMoney()
    {
        return gold > 0;
    }

    /*
    * Followings are implemented methods of "IObservable"
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
        if (observers.ContainsKey(field))
            observers[field].ForEach(o => o.Refresh(field));
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
     * Followings are attributes
     */
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public int Gold
    {
        get { return gold; }
        set
        {
            if (value <= 0)
                gold = 0;
            else
                gold = value;

            NotifyObservers(ObserverTypes.Gold);
        }
    }

}