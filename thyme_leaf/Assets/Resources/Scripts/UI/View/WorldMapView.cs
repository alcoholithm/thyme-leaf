using UnityEngine;
using System.Collections;

public class WorldMapView : View, IActionListener
{
    private WorldMapController controller;
    private UserAdministrator model;

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

    void Awake() {
        this.model = UserAdministrator.Instance;
        this.controller = new WorldMapController(this, this.model);
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
            DialogFacade.Instance.ChangeMsgDialogTitle("Multi Play Mode");
            DialogFacade.Instance.ShowMessageDialog("Waiting for minuate....");

            NetworkConnector.Instance.SetOnNetworkConnectedListener(OnonnectedActionDelegate).
                SetOnNetworkDisconnectedListener(OnisconnectedActionDelegate).JoinRoom();

        }
    }

    void OnonnectedActionDelegate(NetworkResult result){
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
                break;
            default:
                Debug.Log("NETWORK RESULT ERROR : " + result);
                break;
        }
    }

    void OnisconnectedActionDelegate(){
        Debug.Log("Disconnected");
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
