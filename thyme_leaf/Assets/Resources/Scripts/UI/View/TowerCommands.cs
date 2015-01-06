using UnityEngine;
using System.Collections;


public class TowerCommandsController
{
    // mvc
    private BattleModel model;
    private TowerCommands view;

    public TowerCommandsController(TowerCommands view, BattleModel model)
    {
        this.view = view;
        this.model = model;
    }

    public void UpgradeTower()
    {
        Debug.Log("AAAAAAAA");

        AutomatTower tower = model.SelectedObject.transform.GetComponentInChildren<AutomatTower>();

        Debug.Log(tower.ToString());
        Debug.Log(tower.StateMachine.ToString());
        Debug.Log(tower.StateMachine.CurrentState.ToString());

        tower.ChangeState(TowerState_Building.Instance);

        //model.SelectedObject.transform.GetComponentInChildren<Agt_Type1>().ChangeState(TowerState_Idling.Instance);
        //Message msg = tower.ObtainMessage(MessageTypes.MSG_TOWER_READY, new TowerReadyCommand(model.SelectedObject.transform.GetComponentInChildren<Agt_Type1>()));
        //tower.DispatchMessageDelayed(msg, 1.5f);
        //throw new System.NotImplementedException();
        //model.SelectedObject.GetComponent<Agt_Type1>().Upgrade();
    }

    public void SellTower()
    {
        throw new System.NotImplementedException();
        //model.SelectedObject.GetComponent<Agt_Type1>().Sell();
    }

    public void Cancel()
    {
        model.SelectedObject = null;
        view.gameObject.SetActive(false);
    }
}

public class TowerCommands : View, IActionListener
{
    [SerializeField]
    GameObject _upgradeButton;
    [SerializeField]
    GameObject _sellButton;
    [SerializeField]
    GameObject _cancelButton;

    private TowerCommandsController controller;
    private BattleModel model;

    /*
     * followings are unity callback methods
     */
    void Awake()
    {
        this.model = BattleModel.Instance;
        this.controller = new TowerCommandsController(this, this.model);
    }

    /*
     * followings are implemented methods of interface
     */
    public void ActionPerformed(GameObject source)
    {
        if (source.Equals(_upgradeButton))
        {
            controller.UpgradeTower();
        }
        else if (source.Equals(_sellButton))
        {
            controller.SellTower();
        }
        else if (source.Equals(_cancelButton))
        {
            controller.Cancel();
        }
    }
}
