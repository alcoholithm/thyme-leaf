﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : Manager<Spawner>
{
    public new const string TAG = "[Spawner]";

    [SerializeField]
    private GameObject[] automats;
    [SerializeField]
    private GameObject[] towers;
    [SerializeField]
    private GameObject[] projectiles;
    [SerializeField]
    private GameObject[] trovants;

    [SerializeField]
    private int initPoolSize = 100;
    [SerializeField]
    private int maxPoolSize = 200;

    private GameObject automatPool;
    private GameObject trovantPool;
    private GameObject automatBuildingPool;
    private GameObject trovantBuildingPool;


    /**********************************/
    
    public void OnNetworkLoadedLevel()
    {
        //Debug.Log("Start Object Pool");
        //foreach (GameObject automat in automats)
        //{
        //    ObjectPoolingManager.Instance.CreatePool(gameObject, automat, initPoolSize, maxPoolSize, false);
        //}
    }

    /**********************************/

    void Start()
    {
        if (automatPool == null) automatPool = GameObject.Find("AutomatPool");
        if (trovantPool == null) trovantPool = GameObject.Find("TrovantPool");
        if (automatBuildingPool == null) automatBuildingPool = GameObject.Find("AutomatBuildingPool");
        if (trovantBuildingPool == null) trovantBuildingPool = GameObject.Find("TrovantBuildingPool");

        if (Network.isServer || Network.peerType == NetworkPeerType.Disconnected)
        {
            if(automats != null)
                for (int i = 0; i < automats.Length; i++)
                {
                    GameObject go = automats[i];
                    ObjectPoolingManager.Instance.CreatePool(automatPool, go, UnitType.AUTOMART_CHARACTER, i, initPoolSize, maxPoolSize, false);
                }

            if (towers != null)
                for (int i = 0; i < towers.Length; i++)
                {
                    GameObject go = towers[i];
                    ObjectPoolingManager.Instance.CreatePool(automatPool, go, UnitType.AUTOMART_TOWER, i, initPoolSize, maxPoolSize, false);
                }

            if (projectiles != null)
                for (int i = 0; i < projectiles.Length; i++)
                {
                    GameObject go = projectiles[i];
                    ObjectPoolingManager.Instance.CreatePool(automatPool, go, UnitType.AUTOMAT_PROJECTILE, i, initPoolSize, maxPoolSize, false);
                }

            if (trovants != null)
                for (int i = 0; i < trovants.Length; i++)
                {
                    GameObject go = towers[i];
                    ObjectPoolingManager.Instance.CreatePool(automatPool, go, UnitType.TROVANT_CHARACTER, i, initPoolSize, maxPoolSize, false);
                }
        }
    }

    /**********************************/

    public Hero GetHero(AutomatType type)
    {
        return GetHero((int)type);
    }

    public Hero GetHero(int type)
    {
        GameObject go = ObjectPoolingManager.Instance.GetObject(automats[type].name);
        InitHero(ref go);
        return go.GetComponent<Hero>();
    }

    public Hero GetTrovant(TrovantType type)
    {
        return GetTrovant((int)type);
    }

    public Hero GetTrovant(int type)
    {
        GameObject go = ObjectPoolingManager.Instance.GetObject(trovants[type].name);
        InitTrovant(ref go);
        return go.GetComponent<Hero>();
    }

    public Agt_Type1 GetTower(TowerType type)
    {
        return GetTower((int)type);
    }

    public Agt_Type1 GetTower(int type)
    {
        GameObject go = ObjectPoolingManager.Instance.GetObject(towers[type].name);
        InitTower(ref go);
        return go.GetComponent<Agt_Type1>();
    }

    public Projectile GetProjectile(ProjectileType type)
    {
        return GetProjectile((int)type);
    }

    public Projectile GetProjectile(int type)
    {
        GameObject go = ObjectPoolingManager.Instance.GetObject(projectiles[type].name);
        InitProjectile(ref go);
        return go.GetComponent<Projectile>();
    }


    /**********************************/

    public void PerfectFree(GameObject gameObject)
    {
        Destroy(gameObject);
    }

    public void Free(GameObject gameObject)
    {
        //Do you need some more handling the object?
        //gameObject.transform.parent = transform;
        gameObject.SetActive(false);
    }

    /**********************************/
    // Initialize Object

    private void InitHero(ref GameObject go)
    {
        //Transform temp = GameObject.Find ("AutomatUnits").transform;
        Hero hero = go.GetComponent<Hero>();
        Debug.Log("character init");

		//active...
		go.SetActive (true);

		//main setting...
        //hero.transform.parent = temp;
		hero.transform.localScale = Vector3.one;
		hero.transform.localPosition = new Vector3 (0, 0, 0);
		
		//unit detail setting...
		hero.controller.StartPointSetting(StartPoint.AUTOMART_POINT);
		hero.CollisionSetting (true);
		
		hero.controller.setType (UnitType.AUTOMART_CHARACTER);
		hero.controller.setName (UnitNameGetter.GetInstance ().getNameAutomart ());
		
		//move trigger & unit pool manager setting <add>...
		//moving state...
		hero.StateMachine.ChangeState (HeroState_Moving.Instance);
		//moveing enable...
		hero.controller.setMoveTrigger(true);
		//hp bar setting...
		hero.HealthUpdate ();
		
		//test...
		hero.my_name = hero.model.Name;
		
		//unit pool insert...
		UnitObject u_obj = new UnitObject (hero.gameObject, hero.model.Name, hero.model.Type);
		UnitPoolController.GetInstance ().AddUnit (u_obj);
    }

    private void InitTrovant(ref GameObject go)
    {
        //Transform temp = GameObject.Find ("TrovantUnits").transform;
		Hero hero = go.GetComponent<Hero> ();
		Debug.Log ("character init");
		
		//active...
		go.SetActive (true); 

        //hero.transform.parent = temp;
		hero.transform.localScale = Vector3.one;
		hero.transform.localPosition = new Vector3 (0, 0, 0);


		//unit detail setting...
		hero.controller.StartPointSetting(StartPoint.TROVANT_POINT);
		hero.CollisionSetting (true);
		
		hero.controller.setType (UnitType.TROVANT_CHARACTER);
		hero.controller.setName (UnitNameGetter.GetInstance ().getNameTrovant ());
		
		//move trigger & unit pool manager setting <add>...
		//moving state...
		hero.StateMachine.ChangeState (HeroState_Moving.Instance);
		//moveing enable...
		hero.controller.setMoveTrigger(true);
		//hp bar setting...
		hero.HealthUpdate ();
		
		//test...
		hero.my_name = hero.model.Name;
		
		//unit pool insert...
		UnitObject u_obj = new UnitObject (hero.gameObject, hero.model.Name, hero.model.Type);
		UnitPoolController.GetInstance ().AddUnit (u_obj);
    }

    private void InitTower(ref GameObject go)
    {
        go.transform.position = Vector3.zero;
        go.transform.localScale = Vector3.one;
    }

    private void InitProjectile(ref GameObject go)
    {
        go.transform.position = Vector3.zero;
        go.transform.localScale = Vector3.one;
    }


    /**********************************/
    // RPC Methods

    [RPC]
    void INIT_SPAWNED_OBJECT(NetworkViewID spawnerID, NetworkViewID bornerID)
    {
        //if (pool == null)
        //    pool = GameObject.Find("Pool").gameObject;
        Debug.Log("INIT_SPAWNED_OBJECT " + bornerID + " FROM " + spawnerID);
        GameObject borner = NetworkView.Find(bornerID).gameObject;
        GameObject spawner = NetworkView.Find(spawnerID).gameObject;

        borner.transform.parent = spawner.transform;
        borner.SetActive(false);
    }

    [RPC]
    void ACTIVE_OBJECT(NetworkViewID id)
    {
        GameObject baby = NetworkView.Find(id).gameObject;
        baby.SetActive(true);
        Debug.Log("ACTIVE_OBJECT " + id);
    }

    [RPC]
    void TEST(NetworkViewID spawnerID, NetworkViewID viewID, UnitType unitType, int type)
    {
        GameObject nObj = null;
        GameObject spawner = NetworkView.Find(spawnerID).gameObject;
        switch (unitType)
        {
            case UnitType.AUTOMART_CHARACTER:
                nObj = GameObject.Instantiate(automats[type], Vector3.zero, Quaternion.identity) as GameObject;
                break;
            case UnitType.AUTOMART_TOWER:
                nObj = GameObject.Instantiate(towers[type], Vector3.zero, Quaternion.identity) as GameObject;
                break;
            case UnitType.AUTOMAT_PROJECTILE:
                nObj = GameObject.Instantiate(projectiles[type], Vector3.zero, Quaternion.identity) as GameObject;
                break;
            case UnitType.TROVANT_CHARACTER:
                nObj = GameObject.Instantiate(trovants[type], Vector3.zero, Quaternion.identity) as GameObject;
                break;

            default:
                Debug.Log("Not found Unit Type : " + unitType);
                break;
        }
        nObj.networkView.viewID = viewID;
    }
}

