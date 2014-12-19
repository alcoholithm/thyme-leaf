﻿using UnityEngine;
using System.Collections;

public class ConnectScript : MonoBehaviour
{

    // Server IP & Port
    public string masterSeverIP = "210.118.69.150";
    public int masterServerPort = 23466;
    public string facilitatorIP = "210.118.69.150";
    public int facilitatorPort = 50005;

    // Never touch it
    private const string typeName = "Thyme Leaf";

    // User can type any room name by using gameName
    public string gameName = "Custom Room Name";

    private HostData[] hostList;



    // Var. for Testing
    public Transform prefab;

    void Start()
    {
        MasterServer.ipAddress = masterSeverIP;
        MasterServer.port = masterServerPort;
        Network.natFacilitatorIP = facilitatorIP;
        Network.natFacilitatorPort = facilitatorPort;
        Network.minimumAllocatableViewIDs = 1000;
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
        Debug.Log("Server Joined");
        //Network.Instantiate(prefab, transform.position, transform.rotation, 0);
        Application.LoadLevel("MultiplayScene");
    }
    private void JoinServer(HostData hostData)
    {
        Debug.Log("Connect Server");
        Network.Connect(hostData);
    }

    // Server Side
    void OnPlayerConnected()
    {
        Debug.Log("Player Joined to Server");
        Application.LoadLevel("MultiplayScene");
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



}
