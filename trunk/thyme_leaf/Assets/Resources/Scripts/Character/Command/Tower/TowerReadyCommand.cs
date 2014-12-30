using UnityEngine;
using System.Collections;

public class TowerReadyCommand : ICommand
{
    private Agt_Type1 tower;

    public TowerReadyCommand(Agt_Type1 tower)
    {
        this.tower = tower;
    }
    public void Execute()
    {
        tower.StateMachine.ChangeState(TowerState_Idling.Instance);
    }
}
