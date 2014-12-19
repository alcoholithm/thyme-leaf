using UnityEngine;
using System.Collections;

public class TowerReadyCommand : ICommand
{
    private ATT_Type1 tower;

    public TowerReadyCommand(ATT_Type1 tower)
    {
        this.tower = tower;
    }
    public void Execute()
    {
        tower.StateMachine.ChangeState(TowerState_Idling.Instance);
    }
}
