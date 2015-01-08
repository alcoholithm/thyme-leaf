using UnityEngine;
using System.Collections;

/// <summary>
/// managing Scene
/// </summary>
public class SceneManager : Manager<SceneManager>
{
    public const string LOBBY = "1_Lobby";
    public const string WORLD_MAP = "2_WorldMap";
    public const string BATTLE = "3_Battle";
    public const string BATTLE_MULTI = "3_BattleMultiplay";
    public const string TOWER = "4_TowerSetting";
    public const string AUTO = "5_AutomartSetting";
    public const string MULTI = "MultiplayScene";


    /*
     * Followings are unity callback methods
     */
    void Awake()
    {
        IsGlobal = true;

        base.Awake();
    }

    /*
     * Followings are member functions
     */
    IEnumerator LoadLevel()
    {
        Application.LoadLevel(currentScene);

        yield return new AsyncOperation();
    }

    /*
     * Followings are attributes
     */
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

    public new const string TAG = "[SceneManager]";
}