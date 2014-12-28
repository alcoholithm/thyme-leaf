using UnityEngine;
using System.Collections;

/// <summary>
/// managing Scene
/// </summary>
public class SceneManager : Manager<SceneManager>
{
    public new const string TAG = "[SceneManager]";

    public const string LOBBY = "1_Lobby";
    public const string WORLD_MAP = "2_WorldMap";
    public const string BATTLE = "3_Battle";
    public const string TOWER = "4_TowerSetting";
    public const string AUTO = "5_AutomartSetting";

    private string currentScene = LOBBY;
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

        yield return new AsyncOperation();
    }
}