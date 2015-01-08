using UnityEngine;
using System.Collections;

//public class WorldMapController
//{
//    private WorldMapView view;
//    private UserAdministrator model;

//    public WorldMapController(WorldMapView view, UserAdministrator model)
//    {
//        this.view = view;
//        this.model = model;
//    }
//}

public class WorldMapView : View, IActionListener
{
    //private WorldMapController controller;
    //private UserAdministrator model;

    [SerializeField]
    GameObject _starDale;

    [SerializeField]
    GameObject _backBtn;

    [SerializeField]
    GameObject _inventoryBtn;

    [SerializeField]
    GameObject _towerSettingBtn;

    [SerializeField]
    GameObject _multiPlayBtn;

    [SerializeField]
    GameObject _networkClose;

    void Awake()
    {
        //this.model = UserAdministrator.Instance;
        //this.controller = new WorldMapController(this, this.model);
    }

    void Start()
    {

    }

    public void ActionPerformed(GameObject source)
    {
        if (source.Equals(_starDale))
        {
            SceneManager.Instance.CurrentScene = SceneManager.BATTLE;
        }
        else if (source.Equals(_backBtn))
        {
            SceneManager.Instance.CurrentScene = SceneManager.LOBBY;
        }
        else if (source.Equals(_inventoryBtn))
        {
            SceneManager.Instance.CurrentScene = SceneManager.TOWER;
        }
        else if (source.Equals(_towerSettingBtn))
        {
            SceneManager.Instance.CurrentScene = SceneManager.AUTO;
        }
        else if (source.Equals(_multiPlayBtn))
        {
            Debug.Log("join click");
            JoinRoom();
        }
        else if (source.Equals(_networkClose))
        {
            Debug.Log("close click");
            networkClose();
        }
    }

    private void JoinRoom()
    {

        DialogFacade.Instance.ChangeMsgDialogTitle("Multi Play Mode");
        DialogFacade.Instance.ChangeMsgDialogBtnText("Cancle");
        DialogFacade.Instance.ShowMessageDialog("Waiting for minuate....");

        NetworkConnector.Instance.SetOnNetworkConnectedListener(OnConnectedActionDelegate).
                SetOnNetworkDisconnectedListener(OnDisconnectedActionDelegate).JoinRoom();
    }

    private void networkClose()
    {

        NetworkConnector.Instance.ExitRoom();
        DialogFacade.Instance.CloseMessageDialog();
    }

    private void FailConnect()
    {
        DialogFacade.Instance.CloseMessageDialog();
        DialogFacade.Instance.ShowMessageDialog("Fail to connect to Server");
    }

    void OnConnectedActionDelegate(NetworkResult result)
    {
        switch (result)
        {
            case NetworkResult.SUCCESS_TO_CONNECT:
                NetworkConnector.Instance.NetworkLoadLevel(SceneManager.BATTLE_MULTI);
                break;
            case NetworkResult.EMPTY_ROOM:
                Debug.Log("Create room because there is no room");
                NetworkConnector.Instance.CreateRoom();
                break;
            case NetworkResult.FAIL:
                Debug.Log("Fail to connect to server");
                FailConnect();
                break;
            default:
                Debug.Log("NETWORK RESULT ERROR : " + result);
                break;
        }
    }

    void OnDisconnectedActionDelegate()
    {
        Debug.Log(Application.loadedLevelName);

        Debug.Log("Disconnected");
        DialogFacade.Instance.CloseMessageDialog();
        DialogFacade.Instance.ShowMessageDialog("Disconnected to Server");
    }

    /*
     * 
    */
    public GameObject BackButton
    {
        get { return _backBtn; }
        set { _backBtn = value; }
    }

    public GameObject InventoryButton
    {
        get { return _inventoryBtn; }
        set { _inventoryBtn = value; }
    }

    public GameObject TowerSettingButton
    {
        get { return _towerSettingBtn; }
        set { _towerSettingBtn = value; }
    }

    public GameObject MultiPlayBtn
    {
        get { return _multiPlayBtn; }
        set { _multiPlayBtn = value; }
    }

    public GameObject StarDale
    {
        get { return _starDale; }
        set { _starDale = value; }
    }
}
