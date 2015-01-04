using System;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public abstract class Unit : IObservable
{
    // 종족의 이름이 필요하다고?
    protected string name;
    protected string description;

    [SerializeField]
    protected int _maxHp;
    [SerializeField]
    protected int _currHp;
    protected int defense;
    protected UnitType type;
	protected string species_name;

    private Dictionary<ObserverTypes, List<IObserver>> observers =
        new Dictionary<ObserverTypes, List<IObserver>>();


    /*
     * Followings are unity callback methods
     */
    public Unit()
    {
        _maxHp = 100;
        _currHp = _maxHp;
    }

    /*
     * Followings are public member functions
     */
    public bool IsDead()
    {
        return _currHp <= 0;
    }

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
	public string SpecieName
	{
		get { return species_name; }
		set { species_name = value; }
	}
    public string Description
    {
        get { return description; }
        set { description = value; }
    }
    public int MaxHP
    {
        get { return _maxHp; }
        set { _maxHp = value; }
    }
    public int HP
    {
        get { return _currHp; }
        set
        {
            if (value > _maxHp)
                _currHp = _maxHp;
            else
                _currHp = value;

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
