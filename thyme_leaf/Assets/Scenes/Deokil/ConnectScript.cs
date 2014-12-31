using UnityEngine;
using System.Collections;

public class ConnectScript : MonoBehaviour
{

    // Server IP & Port
    private const string masterSeverIP = "210.118.69.150";
    private const int masterServerPort = 23466;
    private const string facilitatorIP = "210.118.69.150";
    private const int facilitatorPort = 50005;

    // Never touch it
    private const string typeName = "Thyme Leaf";

    // User can type any room name by using gameName
    public string gameName = "Custom Room Name";

    private HostData[] hostList;



    // Var. for Testing
    public Transform prefab;

    void Awake()
    {
        DontDestroyOnLoad(this);
        networkView.group = 1;
    }

    void Start()
    {
        MasterServer.ipAddress = masterSeverIP;
        MasterServer.port = masterServerPort;
        Network.natFacilitatorIP = facilitatorIP;
        Network.natFacilitatorPort = facilitatorPort;
        Network.minimumAllocatableViewIDs = 10000;        
    }

    void Update()
    {
        if (!Network.isClient && !Network.isServer)
        {
            if (Input.GetKey("1"))
            {
                Debug.Log("Start Server");
                Network.InitializeServer(1, 25005, !Network.HavePublicAddress());
                MasterServer.RegisterHost(typeName, gameName);
            }
            if (Input.GetKey("2"))
            {
                Debug.Log("Refresh Hosts");
                MasterServer.RequestHostList(typeName);
            }
            if (hostList != null)
            {
                for (int i = 0; i < hostList.Length; i++)
                {
                    //Debug.Log("Host Name [" + i + "] : " + hostList[i].gameName);
                }
            }
            if (Input.GetKey("3"))
            {
                JoinServer(hostList[0]);
            }
        }
    }

    void OnGUI()
    {
        if (!Network.isClient && !Network.isServer)
        {
            if (GUI.Button(new Rect(0, 0, 100, 50), "Start Server"))
            {
                Network.InitializeServer(1, 25005, !Network.HavePublicAddress());
                MasterServer.RegisterHost(typeName, gameName);
            }

            if (GUI.Button(new Rect(0, 50 + 10, 100, 50), "Refresh Hosts"))
            {
                MasterServer.RequestHostList(typeName);
            }

            if (hostList != null)
            {
                for (int i = 0; i < hostList.Length; i++)
                {
                    if (GUI.Button(new Rect(0, 130 + (110 * i), 100, 50), hostList[i].gameName))
                        JoinServer(hostList[i]);
                }
            }
            else
            {
                //Debug.Log("Host List is NULL");
            }
        }
    }

    // Client Side
    void OnConnectedToServer()
    {
        Debug.Log("Client(Me) Joined Server");
        //Network.Instantiate(prefab, transform.position, transform.rotation, 0);
        //Application.LoadLevel("MultiplayScene");
    }
    private void JoinServer(HostData hostData)
    {
        Debug.Log("Connect Server");
        Network.Connect(hostData);
    }

    // Server Side
    void OnPlayerConnected()
    {
        Debug.Log("Player Joined to Server(Me)");
        networkView.RPC("LoadLevel",RPCMode.All);
        //Application.LoadLevel("MultiplayScene");
    }

    // Server Side
    void OnServerInitialized()
    {
        Debug.Log("OnServerInitialized()");        
    }

    void OnMasterServerEvent(MasterServerEvent msEvent)
    {
        if (msEvent == MasterServerEvent.HostListReceived)
        {
            hostList = MasterServer.PollHostList();
            Debug.Log("got host list");
        }
    }

    // Common Side
    void OnDisconnectedFromServer()
    {
        Application.LoadLevel("MultiplayInitScene");
    }

    [RPC]
    void LoadLevel()
    {
        Network.SetSendingEnabled(0, false);
        Network.isMessageQueueRunning = false;

        Network.SetLevelPrefix(5);        
        Application.LoadLevel("MultiplayScene");
        StartCoroutine(Fade());
        StartCoroutine(Fade());

        Network.isMessageQueueRunning = true;
        Network.SetSendingEnabled(0, true);

        Object[] gos = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in gos)
        {
            Debug.Log("Found " + go);
            go.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);
        }
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(1.0f);
    }
}
