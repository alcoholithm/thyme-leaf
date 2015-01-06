using UnityEngine;
using System.Collections;

public class BattleModel
{
    private GameObject selectedObject;

    /*
     * Followings are attributes
     */ 
    public GameObject SelectedObject
    {
        get { return selectedObject; }
        set { selectedObject = value; }
    }

    private static BattleModel instance = new BattleModel();
    public static BattleModel Instance
    {
        get { return instance; }
    }

    public const string TAG = "[BattleModel]";
}

public class TowerSpotCommands : View, IActionListener
{
    // mvc

    // children
    [SerializeField]
    GameObject _buildAGT;
    [SerializeField]
    GameObject _buildASPT;
    [SerializeField]
    GameObject _buildAPT;
    [SerializeField]
    GameObject _cancelButton;

    private BattleModel model;
    private TowerSpotCommandsController controller;

    /*
     * followings are unity callback methods
     */
    void Awake()
    {
        this.model = BattleModel.Instance;
        this.controller = new TowerSpotCommandsController(this, this.model);
    }

    /*
     * followings are implemented methods of interface
     */
    public void ActionPerformed(GameObject source)
    {
        if (source.Equals(_buildAGT))
        {
            controller.BuildTower(TowerType.AGT);
        }
        else if (source.Equals(_buildASPT))
        {
            controller.BuildTower(TowerType.ASPT);
        }
        else if (source.Equals(_buildAPT))
        {
            controller.BuildTower(TowerType.APT);
        }
        else if (source.Equals(_cancelButton))
        {
            controller.Cancel();
        }
    }
}
