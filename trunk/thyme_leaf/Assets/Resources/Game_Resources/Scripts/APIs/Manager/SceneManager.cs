using UnityEngine;
using System.Collections;

/// <summary>
/// managing Scene
/// </summary>
public class SceneManager : Manager<SceneManager>
{
    public new const string TAG = "[SceneManager]";

    public const string LOGIN = "1_Login";
    public const string LOBBY = "2_Lobby";
    public const string GAMEPLAY = "3_GamePlay";

	public const string USERSELECT = "UserSelectScene";
	public const string AUTOMART = "Scene_AutomartTab";
	public const string TOWER = "Scene_TowerScene";
	public const string SETTING = "SettingScene";
	public const string ALARM = "AlarmScene";
	public const string PLAYERSELECT = "PlayerSelectScene";
	public const string LOBBYS = "LobbyScene";
	public const string BEGIN = "BeginScene";

    private string currentScene = BEGIN;
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