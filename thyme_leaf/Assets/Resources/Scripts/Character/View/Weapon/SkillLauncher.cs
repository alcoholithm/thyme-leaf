using UnityEngine;
using System.Collections.Generic;

public class SkillLauncher : Manager<SkillLauncher>, ILauncher, IObservable
{
    private bool doesFired;

    // observer
    private Dictionary<ObserverTypes, List<IObserver>> observers =
    new Dictionary<ObserverTypes, List<IObserver>>();

    protected override void Awake()
    {
        base.Awake();

        doesFired = true;
    }

    /*
     * Followings are unity callback methods
     */ 
    void Update()
    {
        if (!doesFired && Input.GetMouseButtonDown(0))
        {
            this.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Fire(this.transform);

            doesFired = true;

            NotifyObservers(ObserverTypes.Skill);
        }
    }

    /*
     * Followings are public member functions.
     */
    public void Prepare()
    {
        doesFired = false;
    }

    /*
     * Followings are overrided methods of "IWeapon"
     */
    public void Fire(Transform target)
    {
        if (target == null)
            return;

        Spawner.Instance.GetProjectilsWithTarget(ProjectileType.METEO, transform.position, target.transform.position);
        //if (projectile == null)
        //{
        //    Debug.Log("PROJECTILE IS NULL");
        //    return;
        //}

        //projectile.transform.position = transform.position;
        //projectile.transform.localPosition += new Vector3(0, 800, 0);
        //projectile.transform.localScale = Vector3.one;
        //projectile.Move(target);
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
        throw new System.NotImplementedException();
    }
    public void SetChanged()
    {
        throw new System.NotImplementedException();
    }

    public new const string TAG = "[SkillLauncher]";
}