using UnityEngine;
using System.Collections;

public class LobbyView : MonoBehaviour, IObserver{

    public GameObject _start;
    public GameObject _settings;

    private LobbyController controller;
	 
    void Awake()
    {
        LobbyModel.Instance.RegisterObserver(this);
        controller = new LobbyController(this);
    }

    public void GameStart()
    {
        controller.GameStart();
    }

    public void GoSettings()
    {

    }

    void paint()
    {

    }

    public void repaint()
    {

    }

    public void Refresh()
    {
        GameObject.Find("Manager").GetComponent<SceneManager>().CurrentScene = SceneManager.GAMEPLAY;
        Debug.Log("scene loading...");
    }
}
