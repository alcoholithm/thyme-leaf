using UnityEngine;
using System.Collections;

public class TowerReadyCommand : ICommand
{
    private Tower tower;

    public TowerReadyCommand(Tower tower)
    {
        this.tower = tower;
    }
    public void Execute()
    {
        tower.StateMachine.ChangeState(TowerState_Idling.Instance);
    }
}
