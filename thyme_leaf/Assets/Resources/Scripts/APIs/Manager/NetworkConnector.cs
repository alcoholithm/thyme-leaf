using UnityEngine;
using System.Collections;

public delegate void OnConnectedActionDelegate(NetworkResult result);
public delegate void OnDisconnectedActionDelegate();

public class NetworkConnector : Manager<NetworkConnector>
{
	// Server IP & Port
	private const string masterSeverIP = "210.118.69.150";
	private const int masterServerPort = 23466;
	private const string facilitatorIP = "210.118.69.150";
	private const int facilitatorPort = 50005;
	private const int allocatableViewIDs = 4096;
	
	// Listeners
	private static OnConnectedActionDelegate onConnectedActionListener;
	private static OnDisconnectedActionDelegate onDisconnectedActionListener;
	
	private const string typeName = "Thyme Leaf";
	private string gameName = "Time Leap";
	private HostData[] hostList;
    //private static object syncRoot = new System.Object();
    //private static NetworkConnector instance;
	
	
	// ??? 
	// Var. for Testing
	public Transform prefab;
	
	
    //private NetworkConnector() {}
    //public static NetworkConnector Instance
    //{
    //    get
    //    {
    //        if (instance == null) 
    //        {
    //            lock(syncRoot)
    //            {
    //                instance = new NetworkConnector();
    //            }
    //        }
    //        return instance;
    //    }
    //}
	
	void Awake()
	{
		networkView.group = 1;
		MasterServer.ipAddress = masterSeverIP;
		MasterServer.port = masterServerPort;
		Network.natFacilitatorIP = facilitatorIP;
		Network.natFacilitatorPort = facilitatorPort;
		Network.minimumAllocatableViewIDs = allocatableViewIDs;  
	}
	
	// Public Network API Methods
	
	// Step 1 : Attach Listeners
	public NetworkConnector SetOnNetworkConnectedListener(OnConnectedActionDelegate networkActionListener)
	{
		NetworkConnector.onConnectedActionListener = networkActionListener;
		return NetworkConnector.Instance;
	}
	
	public NetworkConnector SetOnNetworkDisconnectedListener(OnDisconnectedActionDelegate networkActionListener)
	{
		NetworkConnector.onDisconnectedActionListener = networkActionListener;
		return NetworkConnector.Instance;
	}
	
	// Step 2 
	public void JoinRoom()
	{
		Debug.Log("... Joining Room");
		if (IsAttachedListener ())
			RequestRoomInfo();	
		else
			Debug.LogError("NullPointerException : NetworkActionListener");
	}
	
	// Step 3
	public void NetworkLoadLevel(string loadLevel)
	{
		if (Network.peerType == NetworkPeerType.Disconnected)
        {
			Debug.Log("NOT CONNECTED NETWORK ERROR : You should check if network is connected correctly");
        }
        else if (Network.isServer)
        {
            //if (GetComponent<NetworkView>().networkView == null)
            //    Debug.Log("network view is null");
            //else 
            NetworkConnector.Instance.networkView.RPC("OnNetworkLoadLevel", RPCMode.All, loadLevel);
        }
	}
	
	// Step 4
	public void ExitRoom()
	{
		Debug.Log ("... Exiting Room");
		Network.Disconnect();
		MasterServer.UnregisterHost();
	}
	
	
	/*******************************************************************/
	// Tutorial 
	
    //void TestOnConnectedActionPerform(NetworkResult result)
    //{
    //    switch (result)
    //    {
    //    case NetworkResult.SUCCESS_TO_CONNECT:
    //        NetworkConnector.Instance.NetworkLoadLevel(SceneManager.BATTLE_MULTI);
    //        break;
    //    case NetworkResult.EMPTY_ROOM:
    //        Debug.Log("Create room because there is no room");
    //        NetworkConnector.Instance.CreateRoom();
    //        break;
    //    case NetworkResult.FAIL:
    //        Debug.Log("Fail to connect to server");
    //        break;
    //    default:
    //        Debug.Log("NETWORK RESULT ERROR : " + result);
    //        break;
    //    }
    //}
	
    //void TestOnDisconnectedActionPerform(){
    //    Debug.Log ("Network Disconnected");
    //}
	
    //void OnGUI()
    //{
    //    if (!Network.isClient && !Network.isServer)
    //    {
    //        if (GUI.Button(new Rect(0, 0, 250, 50), "Start"))
    //        {
    //            NetworkConnector.Instance
    //                .SetOnNetworkConnectedListener(TestOnConnectedActionPerform)
    //                    .SetOnNetworkDisconnectedListener(TestOnDisconnectedActionPerform)
    //                    .JoinRoom();
    //        }
    //    }
    //}
	
	/*******************************************************************/
	
	
	
	
	
	// Private Network API Methods
    private void CreateRoom()
    {
        Debug.Log("... Creating Room");
        if (IsAttachedListener())
            CreateRoom(gameName);
        else
            Debug.LogError("NullPointerException : NetworkActionListener");
    }

	private void CreateRoom(string gameName)
	{
		Network.InitializeServer(1, 25005, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
	}
	
	private void RequestRoomInfo()
	{
        Debug.Log("RequestHostList()");
		MasterServer.RequestHostList(typeName);
	}
	
	private void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
	}
	
	private bool IsAttachedListener()
	{
		return NetworkConnector.onConnectedActionListener != null 
            || NetworkConnector.onDisconnectedActionListener == null;
	}
	
	
	
	
	// Unity Network API Methods
	
	// Client Side
	void OnConnectedToServer()
	{
		Debug.Log("Client(Me) Joined Server");
	}
	
	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
        Debug.Log("OnMasterServerEvent : " + msEvent);
		if (msEvent == MasterServerEvent.HostListReceived)
		{
			hostList = MasterServer.PollHostList();
			if (NetworkConnector.onConnectedActionListener == null)
				Debug.LogError("You must attach the delegate when connected to network");
			else if(hostList == null || hostList.Length == 0)
				onConnectedActionListener(NetworkResult.EMPTY_ROOM);
			else
				JoinServer(hostList[0]);
		}
	}
	
	void OnDisconnectedFromServer(NetworkDisconnection info) {
		Debug.Log ("OnDisconnectedFromServer INFO : " + info);
		if (onDisconnectedActionListener != null)
			onDisconnectedActionListener ();
		else
			Debug.LogError("You must attach the delegate when disconnected from network");
	}
	
	
	// Server Side
	void OnServerInitialized()
	{
		Debug.Log("OnServerInitialized()");        
	}
	
	void OnPlayerConnected()
	{
		Debug.Log("Player Joined to Server(Me)");
		if (NetworkConnector.onConnectedActionListener != null)
			onConnectedActionListener(NetworkResult.SUCCESS_TO_CONNECT);
		else
			Debug.LogError("You must attach the delegate when connected to network");
	}
	
	void OnPlayerDisconnected(NetworkPlayer player) {
		Debug.Log("Clean up after player " + player);
		Network.RemoveRPCs(player);
		Network.DestroyPlayerObjects(player);
		
		Debug.Log ("OnPlayerDisconnected INFO : " + player);
		
		if (onDisconnectedActionListener != null)
			onDisconnectedActionListener ();
		else
			Debug.LogError("You must attach the delegate when disconnected from network");
	}
	
	
	[RPC]
	void OnNetworkLoadLevel(string level)
	{
		Network.SetSendingEnabled(0, false);
		Network.isMessageQueueRunning = false;
		
		//        Network.SetLevelPrefix(5);        
		Application.LoadLevel(level);
		StartCoroutine(Fade());
		StartCoroutine(Fade());
		
		Network.isMessageQueueRunning = true;
		Network.SetSendingEnabled(0, true);
	}
	
	IEnumerator Fade()
	{
		yield return new WaitForSeconds(1.0f);
	}
	
	
	
	
}
