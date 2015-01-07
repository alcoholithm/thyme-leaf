﻿using UnityEngine;
using System.Collections.Generic;

public class SkillLauncher : Weapon, ILauncher, IObservable
{
    private bool doesFired;

    // observer
    private Dictionary<ObserverTypes, List<IObserver>> observers =
    new Dictionary<ObserverTypes, List<IObserver>>();

    void Awake()
    {
        instance = this;
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

        Projectile projectile = Spawner.Instance.GetProjectilsWithTarget(ProjectileType.METEO, transform.position, target.gameObject);
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
    * Followings are overrided methods of "View"
    */
    public override void PrepareUI()
    {
    }

    public override void UpdateUI()
    {
        Fire((Parent as AutomatTower).Model.CurrentTarget.transform);
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


    /*
     * Followings are Attributes
     */
    private static SkillLauncher instance;
    public static SkillLauncher Instance
    {
        get { return instance; }
    }

    public new const string TAG = "[SkillLauncher]";
}