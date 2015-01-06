using UnityEngine;
using System.Collections;

public class TowerReadyCommand : ICommand
{
    private AutomatTower tower;

    public TowerReadyCommand(AutomatTower tower)
    {
        this.tower = tower;
    }
    public void Execute()
    {
        tower.StateMachine.ChangeState(TowerState_Idling.Instance);
    }
}
