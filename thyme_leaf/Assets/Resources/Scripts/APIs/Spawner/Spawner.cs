using UnityEngine;
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
            foreach (GameObject go in automats)
            {
                ObjectPoolingManager.Instance.CreatePool(automatPool, go, initPoolSize, maxPoolSize, false);
            }

            if (towers != null)
            foreach (GameObject go in towers)
            {
                ObjectPoolingManager.Instance.CreatePool(automatBuildingPool, go, initPoolSize, maxPoolSize, false);
            }

            if (projectiles != null)
            foreach (GameObject go in projectiles)
            {
                ObjectPoolingManager.Instance.CreatePool(automatBuildingPool, go, initPoolSize, maxPoolSize, false);
            }

            if (trovants != null)
            foreach (GameObject go in trovants)
            {
                ObjectPoolingManager.Instance.CreatePool(trovantPool, go, initPoolSize, maxPoolSize, false);
            }
        }
    }

    /**********************************/

    public Hero GetHero(AutomatType type)
    {
        GameObject go = ObjectPoolingManager.Instance.GetObject(automats[(int)type].name);
        InitHero(ref go);
        return go.GetComponent<Hero>();
    }

    public Hero GetTrovant(TrovantType type)
    {
        GameObject go = ObjectPoolingManager.Instance.GetObject(trovants[(int)type].name);
        InitTrovant(ref go);
        return go.GetComponent<Hero>();
    }

    public Agt_Type1 GetTower(TowerType type)
    {
        GameObject go = ObjectPoolingManager.Instance.GetObject(towers[(int)type].name);
        InitTower(ref go);
        return go.GetComponent<Agt_Type1>();
    }

    public Projectile GetProjectile(ProjectileType type)
    {
        GameObject go = ObjectPoolingManager.Instance.GetObject(projectiles[(int) type].name);
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
		if (Network.peerType != NetworkPeerType.Disconnected)
		{
			Debug.Log("Connected....");
		}
		Transform temp = GameObject.Find ("AutomatUnits").transform;
		Hero hero = go.GetComponent<Hero> ();
		Debug.Log ("character init");

		//active...
		go.SetActive (true);

		//main setting...
		hero.transform.parent = temp;
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
		if (Network.peerType != NetworkPeerType.Disconnected)
		{
			Debug.Log("Connected....");
		}
		Transform temp = GameObject.Find ("TrovantUnits").transform;
		Hero hero = go.GetComponent<Hero> ();
		Debug.Log ("character init");
		
		//active...
		go.SetActive (true); 

		hero.transform.parent = temp;
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
        //go.layer = (int)Layer.Tower;
        go.transform.position = Vector3.zero;
        go.transform.localScale = Vector3.one;
    }

    private void InitProjectile(ref GameObject go)
    {
        //go.layer = (int)Layer.Tower;
        go.transform.position = Vector3.zero;
        go.transform.localScale = Vector3.one;
    }


    /**********************************/

    [RPC]
    void INIT_SPAWNED_OBJECT(NetworkViewID spawnerID, NetworkViewID bornerID)
    {
        //if (pool == null)
        //    pool = GameObject.Find("Pool").gameObject;
        Debug.Log("INIT_SPAWNED_OBJECT " + bornerID);
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
}