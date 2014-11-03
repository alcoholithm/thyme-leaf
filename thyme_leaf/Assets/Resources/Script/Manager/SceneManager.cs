using UnityEngine;
using System.Collections;

public class SceneManager : Manager
{
    public const string LOGIN = "1_Login";
    public const string LOBBY = "2_Lobby";
    public const string GAMEPLAY = "3_GamePlay";

    private string currentScene = LOGIN;
    public string CurrentScene
    {
        get { return currentScene; }
        set
        {
            currentScene = value;
            StartCoroutine("LoadLevel");
        }
    }

    void LoadLevel()
    {
        Application.LoadLevel(currentScene);
    }
}