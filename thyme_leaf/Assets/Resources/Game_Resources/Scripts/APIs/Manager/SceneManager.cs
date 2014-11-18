using UnityEngine;
using System.Collections;

/// <summary>
/// managing Scene
/// </summary>
public class SceneManager : Manager<SceneManager>
{
    public const string TAG = "[SceneManager]";

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
            StartCoroutine(LoadLevel());
        }
    }

    IEnumerator LoadLevel()
    {
        Application.LoadLevel(currentScene);

        return null;
    }
}