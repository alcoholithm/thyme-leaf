using UnityEngine;
using System.Collections;

public class LobbyController {

    private LobbyModel model;
    private LobbyView view;

    public LobbyController(LobbyView view)
    {
        model = LobbyModel.Instance;
        this.view = view;
    }

    public void GameStart()
    {
        model.GameStart();
    }

    public void GoSettings()
    {
        model.Settings();
    }
}
