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

public class BattleView : View, IActionListener
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





    /*
     * Followings are unity callback methods
     */
    void Awake()
    {
        // MVC
        this.controller = new BattleController(this);

        // Set children
        this.Add(_commandCenterCommands.GetComponent<CommandCenterCommands>());
        this.Add(_towerSpotCommands.GetComponent<TowerSpotCommands>());
        this.Add(_towerCommands.GetComponent<TowerCommands>());
    }


    /*
    * Followings are public member functions.
    */
    public void HideAllCommands()
    {
        views.ForEach(v => { (v as View).gameObject.SetActive(false); });
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
            controller.ShowTowerCommands();
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

    public const string TAG = "[BattleView]";
}
