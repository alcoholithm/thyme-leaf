using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public abstract class Unit : MonoBehaviour, IObservable //ScriptableObject, 
{
    [SerializeField]
    protected int id;
    [SerializeField]
    protected string name;
    [SerializeField]
    protected string description;

    [SerializeField]
    protected int _maxHp;
    [SerializeField]
    protected int _currHp;
    [SerializeField]
    protected int defense;

    protected UnitType type;

	[SerializeField]
	protected AudioUnitType unit_type_name;

    private Dictionary<ObserverTypes, List<IObserver>> observers =
        new Dictionary<ObserverTypes, List<IObserver>>();

    protected virtual void Awake()
    {
        //Naming naming = Naming.Instance;
        //naming.BuildAutomatNameWithState(Naming.FALSTAFF, 1, Naming.ATTACKING);

        id = Naming.maxId++;
        _currHp = _maxHp;
    }
    /*
     * Followings are unity callback methods
     */


    /*
     * Followings are public member functions
     */
    public bool IsDead()
    {
        return _currHp <= 0;
    }

    public void TakeDamageWithDEF(int damage)
    {
        damage = (damage - DEF) > 0 ? damage - DEF : 0;

        HP -= damage;
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
    public int ID
    {
        get { return id; }
        set { id = value; }
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

	public AudioUnitType UnitTypeName
	{
		get { return unit_type_name; }
		set { unit_type_name = value; }
	}

    public const string TAG = "[Unit]";
}
