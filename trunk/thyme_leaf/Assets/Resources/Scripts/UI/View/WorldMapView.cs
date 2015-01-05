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
            SceneManager.Instance.CurrentScene = SceneManager.MULTI;
        }
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
