using UnityEngine;
using System.Collections.Generic;

public class GameRuler : Manager<GameRuler>
{
    public void Judge(bool isWinner)
    {
        if (isWinner)
            BattleView.Instance.ShowVictoryFrame();
        else
            BattleView.Instance.ShowDefeatFrame();

        Time.timeScale = 0;
    }

    public new const string TAG = "[GameRuler]";
}
