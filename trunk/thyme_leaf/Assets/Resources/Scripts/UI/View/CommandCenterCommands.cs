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

    public void SpawnHero(AutomatType type)
    {
        Spawner.Instance.GetHero(type);
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
    GameObject _spawnHero1;
    [SerializeField]
    GameObject _spawnHero2;
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
        if (source.Equals(_spawnHero1))
        {
            controller.SpawnHero(AutomatType.FRANSIS_TYPE1);
        }
        else if (source.Equals(_spawnHero2))
        {
            controller.SpawnHero(AutomatType.FALSTAFF_TYPE1);
        }
        else if (source.Equals(_cancelButton))
        {
            controller.Cancel();
        }
    }
}
