using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : Manager<Spawner>
{
    public new const string TAG = "[Spawner]";

    [SerializeField] private GameObject[] automats;
    [SerializeField] private GameObject[] towers;
    [SerializeField] private GameObject[] projectiles;
    [SerializeField] private GameObject[] trovants;
    [SerializeField] private GameObject[] wchats;
    [SerializeField] private GameObject[] thouses;

    [SerializeField] private int initPoolSize = 100;
    [SerializeField] private int maxPoolSize = 200;

    private GameObject automatPool;
    private GameObject trovantPool;
    private GameObject automatBuildingPool;
    private GameObject trovantBuildingPool;

    void Start()
    {
        if (automatPool == null) automatPool = GameObject.Find("AutomatPool");
        if (trovantPool == null) trovantPool = GameObject.Find("TrovantPool");
        if (automatBuildingPool == null) automatBuildingPool = GameObject.Find("AutomatBuildingPool");
        if (trovantBuildingPool == null) trovantBuildingPool = GameObject.Find("TrovantBuildingPool");

        if (Network.peerType == NetworkPeerType.Disconnected)
        {
            if (automats != null)
                for (int i = 0; i < automats.Length; i++)
                {
                    GameObject go = automats[i];
                    ObjectPoolingManager.Instance.CreatePool(automatPool, go, initPoolSize, maxPoolSize, false);
                }

            if (towers != null)
                for (int i = 0; i < towers.Length; i++)
                {
                    GameObject go = towers[i];
                    ObjectPoolingManager.Instance.CreatePool(automatBuildingPool, go, initPoolSize, maxPoolSize, false);
                }

            if (projectiles != null)
                for (int i = 0; i < projectiles.Length; i++)
                {
                    GameObject go = projectiles[i];
                    ObjectPoolingManager.Instance.CreatePool(automatBuildingPool, go,  initPoolSize, maxPoolSize, false);
                }

            if (trovants != null)
                for (int i = 0; i < trovants.Length; i++)
                {
                    GameObject go = trovants[i];
                    ObjectPoolingManager.Instance.CreatePool(trovantPool, go, initPoolSize, maxPoolSize, false);
                }

            if (wchats != null)
                for (int i = 0; i < wchats.Length; i++)
                {
                    GameObject go = wchats[i];
                    ObjectPoolingManager.Instance.CreatePool(automatBuildingPool, go, 1, maxPoolSize, false);
                }

            if (thouses != null)
                for (int i = 0; i < thouses.Length; i++)
                {
                    GameObject go = thouses[i];
                    ObjectPoolingManager.Instance.CreatePool(trovantBuildingPool, go, 5, maxPoolSize, false);
                }
			
			PathManager.Instance.ShootMap();
        }
    }

    /**********************************/

    public THouse DynamicGetThouse(THouseType type)
    {
        if (Network.peerType == NetworkPeerType.Disconnected)
        {
            GameObject go = GameObject.Instantiate(thouses[(int)type], Vector3.zero, Quaternion.identity) as GameObject;
            go.SetActive(false);
            go.transform.parent = trovantBuildingPool.transform;
            go.SetActive(true);
            return go.GetComponent<THouse>();
        }
        else
        {
            return GetThouse(type);
        }
    }

    public THouse GetThouse(THouseType type)
    {
        if (Network.peerType == NetworkPeerType.Disconnected)
        {
            THouse entity = GetThouse((int)type);
            EntityManager.Instance.RegisterEntity(entity);
            return entity;
        }
        else
        {
            NetworkViewID viewID = Network.AllocateViewID();
            networkView.RPC("NetworkGetThouse", RPCMode.All, viewID, (int)type);
            GameObject go = NetworkView.Find(viewID).gameObject;
            return go.GetComponent<THouse>();
        }
    }

    private THouse GetThouse(int type)
    {
		Debug.Log ("GetThouse : " + (THouseType) type);
        GameObject go = ObjectPoolingManager.Instance.GetObject(thouses[type].name);
		InitThouse(ref go);
        return go.GetComponent<THouse>();
    }

    /**********************************/

    public WChat DynamicGetWChat(WChatType type)
    {
        if (Network.peerType == NetworkPeerType.Disconnected)
        {
            GameObject go = GameObject.Instantiate(wchats[(int)type], Vector3.zero, Quaternion.identity) as GameObject;
            go.SetActive(false);
            go.transform.parent = automatBuildingPool.transform;
            go.SetActive(true);
            return go.GetComponent<WChat>();
        }
        else
        {
            return GetWChat(type);
        }
    }

    public WChat GetWChat(WChatType type)
    {
        if (Network.peerType == NetworkPeerType.Disconnected)
        {
            WChat entity = GetWChat((int)type);
            EntityManager.Instance.RegisterEntity(entity);
            return entity;
        }
        else
        {
            NetworkViewID viewID = Network.AllocateViewID();
            networkView.RPC("NetworkGetWChat", RPCMode.All, viewID, (int)type);
            GameObject go = NetworkView.Find(viewID).gameObject;
            return go.GetComponent<WChat>();
        }
    }

    private WChat GetWChat(int type)
    {
        GameObject go = ObjectPoolingManager.Instance.GetObject(wchats[type].name);
        InitWChat(ref go);
        return go.GetComponent<WChat>();
    }

    /**********************************/

    public Hero DynamicGetHero(AutomatType type)
    {
        if (Network.peerType == NetworkPeerType.Disconnected)
        {
            GameObject go = GameObject.Instantiate(automats[(int)type], Vector3.zero, Quaternion.identity) as GameObject;
            go.SetActive(false);
            go.transform.parent = automatPool.transform;
            go.SetActive(true);
            return go.GetComponent<Hero>();
        }
        else
        {
            return GetHero(type);
        }
    }

    public Hero GetHero(AutomatType type)
    {
        if (Network.peerType == NetworkPeerType.Disconnected)
        {
            return GetHero((int)type);
        }
        else
        {
            NetworkViewID viewID = Network.AllocateViewID();
            networkView.RPC("NetworkGetHero", RPCMode.All, viewID, (int) type);
            GameObject go = NetworkView.Find(viewID).gameObject;
            return go.GetComponent<Hero>();
        }
    }
    
    private Hero GetHero(int type)
    {
        GameObject go = ObjectPoolingManager.Instance.GetObject(automats[type].name);
        InitHero(ref go);
        return go.GetComponent<Hero>();
    }

    //

    public Hero DynamicGetTrovant(TrovantType type)
    {
        if (Network.peerType == NetworkPeerType.Disconnected)
        {
            GameObject go = GameObject.Instantiate(trovants[(int)type], Vector3.zero, Quaternion.identity) as GameObject;
            return go.GetComponent<Hero>();
        }
        else
        {
            return GetTrovant(type);
        }
    }

    public Hero GetTrovant(TrovantType type)
    {
        if (Network.peerType == NetworkPeerType.Disconnected)
            return GetTrovant((int)type);
        else
        {
            NetworkViewID viewID = Network.AllocateViewID();
            networkView.RPC("NetworkGetTrovant", RPCMode.All, viewID, (int)type);
            GameObject go = NetworkView.Find(viewID).gameObject;
            return go.GetComponent<Hero>();
        }
    }

    private Hero GetTrovant(int type)
    {
        GameObject go = ObjectPoolingManager.Instance.GetObject(trovants[type].name);
        InitTrovant(ref go);
        return go.GetComponent<Hero>();
    }

    //

    public Agt_Type1 DynamicGetTower(TowerType type)
    {
        if (Network.peerType == NetworkPeerType.Disconnected)
        {
            GameObject go = GameObject.Instantiate(towers[(int)type], Vector3.zero, Quaternion.identity) as GameObject;
            return go.GetComponent<Agt_Type1>();
        }
        else
        {
            return GetTower(type);
        }
    }

    public Agt_Type1 GetTower(TowerType type)
    {
        if (Network.peerType == NetworkPeerType.Disconnected)
            return GetTower((int)type);
        else
        {
            NetworkViewID viewID = Network.AllocateViewID();
            networkView.RPC("NetworkGetTower", RPCMode.All, viewID, (int)type);
            GameObject go = NetworkView.Find(viewID).gameObject;
            return go.GetComponent<Agt_Type1>();
        }
    }

    private Agt_Type1 GetTower(int type)
    {
        GameObject go = ObjectPoolingManager.Instance.GetObject(towers[type].name);
        InitTower(ref go);
        return go.GetComponent<Agt_Type1>();
    }


    //

    public Projectile DynamicGetProjectile(ProjectileType type)
    {
        if (Network.peerType == NetworkPeerType.Disconnected)
        {
            GameObject go = GameObject.Instantiate(projectiles[(int)type], Vector3.zero, Quaternion.identity) as GameObject;
            return go.GetComponent<Projectile>();
        }
        else
        {
            return GetProjectile(type);
        }
    }

    public Projectile GetProjectile(ProjectileType type)
    {
        if (Network.peerType == NetworkPeerType.Disconnected)
            return GetProjectile((int)type);
        else
        {
            if (Network.isClient) return null;

            NetworkViewID viewID = Network.AllocateViewID();
            networkView.RPC("NetworkGetProjectile", RPCMode.All, viewID, (int)type);
            GameObject go = NetworkView.Find(viewID).gameObject;
            return go.GetComponent<Projectile>();
        }
    }

    private Projectile GetProjectile(int type)
    {
        GameObject go = ObjectPoolingManager.Instance.GetObject(projectiles[type].name);
        InitProjectile(ref go);
        return go.GetComponent<Projectile>();
    }


    /**********************************/
    // Dying Methods

    public void PerfectFree(GameObject gameObject)
    {
        if (Network.peerType == NetworkPeerType.Disconnected)
            Destroy(gameObject);
        else
            Free(gameObject);
    }

    public void Free(GameObject gameObject)
    {
        if (Network.peerType == NetworkPeerType.Disconnected)
        {
            EntityManager.Instance.RemoveEntity(gameObject.GetComponent<GameEntity>());
            gameObject.SetActive(false);
        }
        else
        {
            if (gameObject.networkView.isMine){
                Debug.Log("Network Free to ALL Users");
                networkView.RPC("NetworkFree", RPCMode.All, gameObject.networkView.viewID);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    
    /**********************************/
    // Initializing Methods

	private void InitThouse(ref GameObject go)
	{
		go.transform.parent = GameObject.Find("TrovantBuildings").transform;
		go.transform.localScale = new Vector3(1, 1, 1);

		Define.center_list.Add (go);
	}

    private void InitWChat(ref GameObject go)
    {
        go.transform.parent = GameObject.Find("AutomatBuildings").transform;
        go.transform.localScale = new Vector3(1, 1, 1);

		Define.center_list.Add (go);
    }

    private void InitHero(ref GameObject go)
    {
		InitUint (ref go, StartPoint.AUTOMAT_POINT);
    }

    private void InitTrovant(ref GameObject go)
    {
		InitUint (ref go, StartPoint.TROVANT_POINT);
    }

	private void InitUint(ref GameObject go, StartPoint type)
	{
		Hero hero = go.GetComponent<Hero>();
		Debug.Log("character init");

        hero.GetComponent<UISprite>().MakePixelPerfect();
		hero.transform.localScale = Vector3.one;
		hero.transform.localPosition = new Vector3 (0, 0, 0);
		
		if(type == StartPoint.AUTOMAT_POINT)
			hero.controller.StartPointSetting(StartPoint.AUTOMAT_POINT);
		else
			hero.controller.StartPointSetting(StartPoint.TROVANT_POINT);
		hero.CollisionSetting (true);

		if(type == StartPoint.AUTOMAT_POINT)
		{
			hero.controller.setType (UnitType.AUTOMAT_CHARACTER);
		}
		else
		{
			hero.controller.setType (UnitType.TROVANT_CHARACTER);
		}
		hero.controller.setNodeOffsetStruct (Define.path_node_off.getNodeOffset ());

		hero.StateMachine.ChangeState (HeroState_Moving.Instance);
		hero.controller.setMoveTrigger(true);

		hero.my_name = hero.model.ID.ToString ();
		UnitObject u_obj = new UnitObject (hero.gameObject, hero.model.ID, hero.model.Type);
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
    void NetworkGetThouse(NetworkViewID viewID, int type)
    {
        GameObject go = GameObject.Instantiate(thouses[type], Vector3.zero, Quaternion.identity) as GameObject;
        go.SetActive(false);
        go.transform.parent = trovantBuildingPool.transform;
        go.SetActive(true);
        go.networkView.viewID = viewID;
        InitThouse(ref go);
    }

    [RPC]
    void NetworkGetWChat(NetworkViewID viewID, int type)
    {
        GameObject go = GameObject.Instantiate(wchats[type], Vector3.zero, Quaternion.identity) as GameObject;
        go.SetActive(false);
        go.transform.parent = automatBuildingPool.transform;
        go.SetActive(true);
        go.networkView.viewID = viewID;
        InitWChat(ref go);
    }

    [RPC]
    void NetworkGetHero(NetworkViewID viewID, int type)
    {
        GameObject go = GameObject.Instantiate(automats[type], Vector3.zero, Quaternion.identity) as GameObject;
        go.SetActive(false);
        go.transform.parent = automatPool.transform;
        go.SetActive(true);
        go.networkView.viewID = viewID;
        InitHero(ref go);
    }

    [RPC]
    void NetworkGetTrovant(NetworkViewID viewID, int type)
    {
        GameObject go = GameObject.Instantiate(trovants[type], Vector3.zero, Quaternion.identity) as GameObject;
        go.SetActive(false);
        go.transform.parent = trovantPool.transform;
        go.SetActive(true);
        go.networkView.viewID = viewID;
        InitTrovant(ref go);
    }

    [RPC]
    void NetworkGetTower(NetworkViewID viewID, int type)
    {
        GameObject go = GameObject.Instantiate(towers[type], Vector3.zero, Quaternion.identity) as GameObject;
        go.SetActive(false);
        go.transform.parent = automatBuildingPool.transform;
        go.SetActive(true);
        go.networkView.viewID = viewID;
        InitTower(ref go);
    }

    [RPC]
    void NetworkGetProjectile(NetworkViewID viewID, int type)
    {
        GameObject go = GameObject.Instantiate(projectiles[type], Vector3.zero, Quaternion.identity) as GameObject;
        go.SetActive(false);
        go.transform.parent = automatBuildingPool.transform;
        go.SetActive(true);
        go.networkView.viewID = viewID;
        InitProjectile(ref go);
    }

    [RPC]
    void NetworkFree(NetworkViewID viewID)
    {
        if (!viewID.isMine) return;
        GameObject go = NetworkView.Find(viewID).gameObject;
        Destroy(go);
    }
}

