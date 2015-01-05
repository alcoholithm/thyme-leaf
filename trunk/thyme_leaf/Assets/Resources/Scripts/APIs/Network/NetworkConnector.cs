using UnityEngine;
using System.Collections;

public delegate void NetworkActionPerform(NetworkResult result);

public class NetworkConnector : Singleton<NetworkConnector>
{
    // Server IP & Port
    private const string masterSeverIP = "210.118.69.150";
    private const int masterServerPort = 23466;
    private const string facilitatorIP = "210.118.69.150";
    private const int facilitatorPort = 50005;

    private static NetworkActionPerform networkActionDel;
    private const string typeName = "Thyme Leaf";
    private string gameName = "Custom Room Name";

    private HostData[] hostList;

    // Var. for Testing
    public Transform prefab;

    void Awake()
    {
        networkView.group = 1;
        MasterServer.ipAddress = masterSeverIP;
        MasterServer.port = masterServerPort;
        Network.natFacilitatorIP = facilitatorIP;
        Network.natFacilitatorPort = facilitatorPort;
        Network.minimumAllocatableViewIDs = 10000;        
    }

    // Public API Methods
    // step 1
    public void MakeRoom()
    {
        MakeRoom(gameName);
    }

    // step 2
    public void JoinRoom(NetworkActionPerform networkAction)
    {
        NetworkConnector.networkActionDel = networkAction;
        if (NetworkConnector.networkActionDel == null)
            Debug.Log("NetworkConnector.networkActionDel is null");
        RequestRoomInfos();
    }

    // step 3
    public void NetworkLoadLevel()
    {
        if (Network.peerType == NetworkPeerType.Disconnected)
        {
            Debug.Log("NOT CONNECTED NETWORK ERROR : You should check if network is connected correctly");
            return;
        }
        else
        {
            networkView.RPC("LoadLevel", RPCMode.All);
        }
    }




    // Private API Methods
    private void MakeRoom(string gameName)
    {
        Network.InitializeServer(1, 25005, !Network.HavePublicAddress());
        MasterServer.RegisterHost(typeName, gameName);
    }

    private void RequestRoomInfos()
    {
        MasterServer.RequestHostList(typeName);
    }

    private void JoinServer(HostData hostData)
    {
        Debug.Log("...Connecting to Server");
        Network.Connect(hostData);
    }


    // Unity API Methods

    // Client Side
    void OnConnectedToServer()
    {
        Debug.Log("Client(Me) Joined Server");
        //Network.Instantiate(prefab, transform.position, transform.rotation, 0);
        //Application.LoadLevel("MultiplayScene");
    }

    void OnMasterServerEvent(MasterServerEvent msEvent)
    {
        if (msEvent == MasterServerEvent.HostListReceived)
        {
            hostList = MasterServer.PollHostList();
            if (NetworkConnector.networkActionDel != null && hostList[0] != null)
                JoinServer(hostList[0]);
            else
                networkActionDel(NetworkResult.EMPTY_ROOM);
        }
    }

    void OnDisconnectedFromServer()
    {
        Application.LoadLevel("MultiplayInitScene");
    }


    // Server Side
    void OnPlayerConnected()
    {
        Debug.Log("Player Joined to Server(Me)");
        if (NetworkConnector.networkActionDel != null)
            networkActionDel(NetworkResult.SUCCESS_FOUND);
        else
            Debug.Log("You must attach the delegate for networking");
    }

    void OnServerInitialized()
    {
        Debug.Log("OnServerInitialized()");        
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
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(1.0f);
    }


    // Method for Tutorial 
    void TestNetworkActionPerform(NetworkResult result)
    {
        Debug.Log("Let's perform network");
        switch (result)
        {
            case NetworkResult.SUCCESS_FOUND:
                break;
            case NetworkResult.EMPTY_ROOM:
                break;
            case NetworkResult.FAIL:
                break;
            default:
                Debug.Log("FAIL TO HANDLE NETWORK RESULT : " + result);
                break;
        }
        Debug.Log(result);
    }

    void OnGUI()
    {
        if (!Network.isClient && !Network.isServer)
        {
            if (GUI.Button(new Rect(0, 0, 300, 50), "Step 1 : Make Room"))
            {
                NetworkConnector.Instance.MakeRoom();
            }

            if (GUI.Button(new Rect(0, 50 + 10, 300, 50), "Step 2 : Join Room"))
            {
                Debug.Log("...Joining Room");
                NetworkActionPerform nap = TestNetworkActionPerform;
                NetworkConnector.Instance.JoinRoom(nap);
            }


            //if (GUI.Button(new Rect(0, 50 + 10, 100, 50), "Refresh Hosts"))
            //{
            //    MasterServer.RequestHostList(typeName);
            //}

            //if (hostList != null)
            //{
            //    for (int i = 0; i < hostList.Length; i++)
            //    {
            //        if (GUI.Button(new Rect(0, 130 + (110 * i), 100, 50), hostList[i].gameName))
            //            JoinServer(hostList[i]);
            //    }
            //}
            //else
            //{

            //}
        }
    }
}
