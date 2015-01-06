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
    [SerializeField] private GameObject[] effects;

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

            if (effects != null)
                for (int i = 0; i < effects.Length; i++)
                {
                    GameObject go = effects[i];
                    ObjectPoolingManager.Instance.CreatePool(automatBuildingPool, go, initPoolSize, maxPoolSize, false);
                }
        }
        PathManager.Instance.ShootMap();
        CreateWChats();
    }

    public void CreateWChats() 
    {
        Debug.Log("CREATE WCHATS");
        if (Network.peerType == NetworkPeerType.Disconnected)
            GetWChat(WChatType.WCHAT_TYPE1, PathManager.single_position);
        else if (Network.isServer)
            GetWChat(WChatType.WCHAT_TYPE1, PathManager.server_position);
        else if (Network.isClient)
            GetWChat(WChatType.WCHAT_TYPE1, PathManager.client_position);
    }

    

    /**********************************/
    // Effects
    // Poison Cloud

    public GameObject GetPoisonCloud(EffectType type)
    {
        if (Network.peerType == NetworkPeerType.Disconnected)
        {
            return GetPoisonCloud((int)type);
        }
        else
        {
            NetworkViewID viewID = Network.AllocateViewID();
            networkView.RPC("NetworkGetPoisonCloud", RPCMode.All, viewID, (int)type);
            GameObject go = NetworkView.Find(viewID).gameObject;
            return go;
        }
    }

    private GameObject GetPoisonCloud(int type)
    {
        GameObject go = ObjectPoolingManager.Instance.GetObject(effects[type].name);
        InitPoisonCloud(ref go);
        return go;
    }
    


    /**********************************/

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

    public WChat GetWChat(WChatType type, Vector3 pos)
    {
        WChat entity = null;
        if (Network.peerType == NetworkPeerType.Disconnected)
        {
            entity = GetWChat((int)type, pos);
        }
        else
        {
            NetworkViewID viewID = Network.AllocateViewID();

            Debug.Log("CREATE WCHAT NETWORK VIEW " + viewID);

            networkView.RPC("NetworkGetWChat", RPCMode.All, viewID, (int)type, pos);
            GameObject go = NetworkView.Find(viewID).gameObject;
            entity = go.GetComponent<WChat>();
        }
        EntityManager.Instance.RegisterEntity(entity);
        return entity;
    }

    private WChat GetWChat(int type, Vector3 pos)
    {
        GameObject go = ObjectPoolingManager.Instance.GetObject(wchats[type].name);
        InitWChat(ref go, pos);
        return go.GetComponent<WChat>();
    }

    /**********************************/
    
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

    public AutomatTower GetTower(TowerType type)
    {
        if (Network.peerType == NetworkPeerType.Disconnected)
            return GetTower((int)type);
        else
        {
            NetworkViewID viewID = Network.AllocateViewID();
            networkView.RPC("NetworkGetTower", RPCMode.All, viewID, (int)type);
            GameObject go = NetworkView.Find(viewID).gameObject;
            return go.GetComponent<AutomatTower>();
        }
    }

    private AutomatTower GetTower(int type)
    {
        GameObject go = ObjectPoolingManager.Instance.GetObject(towers[type].name);
        InitTower(ref go);
        return go.GetComponent<AutomatTower>();
    }


    //
    
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


    private void InitPoisonCloud(ref GameObject go)
    {
        go.transform.parent = GameObject.Find("AutomatBuildings").transform;
        go.transform.position = Vector3.zero;
        go.transform.localScale = Vector3.one;
    }

	private void InitThouse(ref GameObject go)
	{
		go.transform.parent = GameObject.Find("TrovantBuildings").transform;
		go.transform.localScale = new Vector3(1, 1, 1);

		Define.THouse_list.Add (go);
	}

    private void InitWChat(ref GameObject go, Vector3 pos)
    {
        Debug.Log("InitWChat pos : " + pos);
        go.transform.parent = GameObject.Find("AutomatBuildings").transform;
        go.transform.localScale = new Vector3(1, 1, 1);
        go.transform.localPosition = pos;

        WChat wchat = go.GetComponent<WChat>();
        wchat.PositionNode = pos;
        wchat.ChangeState(WChatState_Idling.Instance);
        
		Define.THouse_list.Add (go);
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

		//selected center node input...
		//trovant is not input value...
		if(type == StartPoint.AUTOMAT_POINT) hero.controller.StartPointSetting (Define.selected_center);
		else hero.controller.StartPointSetting ( StartPoint.TROVANT_POINT);   //test code...after..remove code...

		hero.CollisionSetting (true);

		if(type == StartPoint.AUTOMAT_POINT)
		{
			hero.controller.setType (UnitType.AUTOMAT_CHARACTER);
			hero.controller.setId (UnitNameGetter.GetInstance ().getNameAutomart ());
		}
		else
		{
			hero.controller.setType (UnitType.TROVANT_CHARACTER);
			hero.controller.setId (UnitNameGetter.GetInstance ().getNameTrovant());
		}
		hero.controller.setNodeOffsetStruct (Define.path_node_off.getNodeOffset ());

		hero.StateMachine.ChangeState (HeroState_Moving.Instance);
		hero.controller.setMoveTrigger(true);

		hero.my_name = hero.model.ID.ToString (); //test code...
		hero.MyUnit = new UnitObject (hero.gameObject, hero.model.ID, hero.model.Type);
		UnitPoolController.GetInstance ().AddUnit (hero.MyUnit);
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
    void NetworkGetPoisonCloud(NetworkViewID viewID, int type)
    {
        GameObject go = GameObject.Instantiate(effects[type], Vector3.zero, Quaternion.identity) as GameObject;
        go.SetActive(false);
        go.transform.parent = automatBuildingPool.transform;
        go.SetActive(true);
        go.networkView.viewID = viewID;
        InitPoisonCloud(ref go);
    }

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
    void NetworkGetWChat(NetworkViewID viewID, int type, Vector3 pos)
    {
        Debug.Log("NetworkGetWChat");
        GameObject go = GameObject.Instantiate(wchats[type], Vector3.zero, Quaternion.identity) as GameObject;
        go.SetActive(false);
        go.transform.parent = automatBuildingPool.transform;
        go.SetActive(true);
        go.networkView.viewID = viewID;
        InitWChat(ref go, pos);
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
        EntityManager.Instance.RemoveEntity(go.GetComponent<GameEntity>());
        Destroy(go);
    }
}

