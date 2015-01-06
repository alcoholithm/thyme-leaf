using UnityEngine;
using System.Collections;

public class BattleController
{
    private BattleView view;

    public BattleController(BattleView view)
    {
        this.view = view;
    }

    public void ShowCommandCenterCommands()
    {
        view.HideAllCommands();
        view.CommandCenterCommands.SetActive(true);
    }

    public void ShowTowerSpotCommands(GameObject selectedObject)
    {
        BattleModel.Instance.SelectedObject = selectedObject;
        view.HideAllCommands();
        view.TowerSpotCommands.SetActive(true);
    }

    public void ShowTowerCommands()
    {
        view.HideAllCommands();
        view.TowerCommands.SetActive(true);
    }
}

public class BattleView : View, IActionListener, IObserver
{
    //-------------------- MVC
    private BattleController controller;

    //--------------------


    // children
    [SerializeField]
    private GameObject _commandCenterCommands;
    [SerializeField]
    private GameObject _towerSpotCommands;
    [SerializeField]
    private GameObject _towerCommands;
    [SerializeField]
    private GameObject _victoryFrame;
    [SerializeField]
    private GameObject _defeatFrame;
    [SerializeField]
    private GameObject _goldFrame;





    /*
     * Followings are unity callback methods
     */
    void Awake()
    {
        // singleton
        instance = this;

        // MVC
        this.controller = new BattleController(this);

        // set children
        this.Add(_goldFrame.GetComponent<GoldFrame>());

    }

    void Start()
    {
        UserAdministrator.Instance.CurrentUser.RegisterObserver(this, ObserverTypes.Gold);
        UpdateUI();
    }


    /*
    * Followings are public member functions.
    */
    public void HideAllCommands()
    {
        _commandCenterCommands.SetActive(false);
        _towerSpotCommands.SetActive(false);
        _towerCommands.SetActive(false);
    }

    public void ShowVictoryFrame()
    {
        _victoryFrame.SetActive(true);
    }

    public void ShowDefeatFrame()
    {
        _defeatFrame.SetActive(true);
    }

    /*
    * Followings are implemented methods of "IActionListener"
    */
    public void ActionPerformed(GameObject source)
    {
        if (source.tag.Equals(Tag.TagCommandCenter))
        {
            controller.ShowCommandCenterCommands();
        }
        else if (source.tag.Equals(Tag.TagTowerSpot))
        {
            controller.ShowTowerSpotCommands(source);
        }
        else if (source.tag.Equals(Tag.TagTower))
        {
            //controller.ShowTowerCommands();
        }
    }

    /*
    * Followings are implemented methods of "IObserver"
    */
    public void Refresh(ObserverTypes field)
    {
        if (field == ObserverTypes.Gold)
        {
            UpdateUI();
        }
    }

    /*
     * Followings are attributes
     */ 
    public GameObject CommandCenterCommands
    {
        get { return _commandCenterCommands; }
        set { _commandCenterCommands = value; }
    }

    public GameObject TowerSpotCommands
    {
        get { return _towerSpotCommands; }
        set { _towerSpotCommands = value; }
    }

    public GameObject TowerCommands
    {
        get { return _towerCommands; }
        set { _towerCommands = value; }
    }

    private static BattleView instance;
    public static BattleView Instance
    {
        get { return instance; }
    }

    public const string TAG = "[BattleView]";

}
