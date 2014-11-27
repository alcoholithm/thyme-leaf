using UnityEngine;
using System.Collections;

public class TowerBuildCommand : ICommand
{
    private Tower tower;

    public TowerBuildCommand(Tower tower)
    {
        this.tower = tower;
    }
    public void Execute()
    {
        tower.StateMachine.ChangeState(TowerState_Building.Instance);
    }
}
