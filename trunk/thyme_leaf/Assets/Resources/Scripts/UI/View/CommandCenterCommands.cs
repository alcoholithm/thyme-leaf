using UnityEngine;
using System.Collections;


public class CommandCenterCommandsController
{
    // mvc
    private BattleModel model;
    private CommandCenterCommands view;

    public CommandCenterCommandsController(CommandCenterCommands view, BattleModel model)
    {
        this.view = view;
        this.model = model;
    }

    public void SpawnHero()
    {
        view.gameObject.SetActive(false);

        Transform temp = GameObject.Find("TrovantUnits").transform;
        Hero hero = Spawner.Instance.GetHero(AutomatType.FRANSIS_TYPE1);

        //main setting...
        hero.transform.parent = temp;
        hero.transform.localScale = Vector3.one;
        hero.transform.localPosition = new Vector3(0, 0, 0);

        //unknown code..
        hero.gameObject.SetActive(false);
        hero.gameObject.SetActive(true);

        //unit detail setting...
        hero.setLayer(Layer.Trovant);
        if (hero.getLayer() == Layer.Automart)
        {
            hero.controller.StartPointSetting(StartPoint.AUTOMART_POINT);
        }
        else if (hero.getLayer() == Layer.Trovant)
        {
            hero.controller.StartPointSetting(StartPoint.TROVANT_POINT);
        }
        hero.CollisionSetting(true);

        hero.controller.setType(UnitType.TROVANT_CHARACTER);
        hero.controller.setName(UnitNameGetter.GetInstance().getNameTrovant());

        //move trigger & unit pool manager setting <add>...
        //moving state...
        hero.StateMachine.ChangeState(HeroState_Moving.Instance);
        //moveing enable...
        hero.controller.setMoveTrigger(true);
        //hp bar setting...
        hero.HealthUpdate();

        //test...
        hero.my_name = hero.model.Name;

        //unit pool insert...
        UnitObject u_obj = new UnitObject(hero.gameObject, hero.model.Name, hero.model.Type);
        UnitPoolController.GetInstance().AddUnit(u_obj);
        //another   
        //Message msg = tower.ObtainMessage(MessageTypes.MSG_BUILD_TOWER, new TowerBuildCommand(tower));
        //tower.DispatchMessage(msg);
    }

    public void Cancel()
    {
        model.SelectedObject = null;
        view.gameObject.SetActive(false);
    }
}

public class CommandCenterCommands : View, IActionListener
{
    // mvc

    // children
    [SerializeField]
    GameObject _spawnHero;
    [SerializeField]
    GameObject _cancelButton;

    private BattleModel model;
    private CommandCenterCommandsController controller;

    /*
     * followings are unity callback methods
     */
    void Awake()
    {
        this.model = BattleModel.Instance;
        this.controller = new CommandCenterCommandsController(this, this.model);
    }

    /*
     * followings are implemented methods of interface
     */
    public void ActionPerformed(GameObject source)
    {
        if (source.Equals(_spawnHero))
        {
            controller.SpawnHero();
        }
        else if (source.Equals(_cancelButton))
        {
            controller.Cancel();
        }
    }
}
