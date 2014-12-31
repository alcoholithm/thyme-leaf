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
        Spawner.Instance.GetHero(AutomatType.FRANSIS_TYPE1);
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
