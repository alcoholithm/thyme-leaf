using System;
using System.Collections.Generic;

public abstract class Unit : IObservable
{
    protected string name;
    protected string description;
    protected int maxHp;
    protected int currHp;
    protected int defense;
    protected UnitType type;

    private Dictionary<ObserverTypes, List<IObserver>> observers =
        new Dictionary<ObserverTypes, List<IObserver>>();

    /*
    * followings are implemented methods of "IObservable"
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
        throw new NotImplementedException();
    }

    public void SetChanged()
    {
        throw new NotImplementedException();
    }


    /*
     * followings are attributes
     */
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public string Description
    {
        get { return description; }
        set { description = value; }
    }
    public int MaxHP
    {
        get { return maxHp; }
        set { maxHp = value; }
    }
    public int HP
    {
        get { return currHp; }
        set
        {
            if (currHp > maxHp)
                currHp = maxHp;
            else
                currHp = value;

            NotifyObservers(ObserverTypes.Health);
        }
    }
    public int DEF
    {
        get { return defense; }
        set { defense = value; }
    }
    public UnitType Type
    {
        get { return type; }
        set { type = value; }
    }

    public const string TAG = "[Unit]";
}
