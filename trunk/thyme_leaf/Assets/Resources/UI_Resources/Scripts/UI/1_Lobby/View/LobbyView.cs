﻿using UnityEngine;
using System.Collections;

public class LobbyView : MonoBehaviour, IView, IObserver_User
{
    private LobbyController controller;
    private UserAdministrator model;

    [SerializeField]
    GameObject _registerUserFrame;
    [SerializeField]
    GameObject _playerSelectFrame;
    [SerializeField]
    GameObject _welcomeFrame;
    [SerializeField]
    GameObject _goToWorldMapButton;
    [SerializeField]
    GameObject _settingsButton;
    [SerializeField]
    GameObject _startButton;
    [SerializeField]
    GameObject _registerButton;
    [SerializeField]
    UILabel _userName;

    /*
     * followings are unity callback methods
     */
    void Awake()
    {
        this.model = UserAdministrator.Instance;
        this.controller = new LobbyController(this, UserAdministrator.Instance);
        this.model.RegisterObserver(this);
    }

    /*
     * followings are member functions
     */
    private void Begin()
    {
        controller.Start();
    }
    private void AddUser()
    {
        controller.Register(_userName.text);
    }

    /*
     * followings are implemented methods of interface
     */
    public void Refresh()
    {
        //if (model.IsLogin)
        //{
        //    _id.GetComponentInChildren<UILabel>().text = "Login has succeeded";
        //    Debug.Log("Login has succeeded");
        //}
        //else
        //{
        //    _id.GetComponentInChildren<UILabel>().text = "Login has failed";
        //    Debug.Log("Login has failed");
        //}
    }

    public void SetVisible(GameObject gameObject, bool active)
    {
        gameObject.SetActive(active);
    }

    public void prepareLobby()
    {
        SetVisible(SettingsButton, true);
        SetVisible(GoToWorldMapButton, true);
        SetVisible(WelcomeFrame, true);
    }

    public void ActionPerformed(string actionCommand)
    {
        if (actionCommand.Equals(_startButton.name))
        {
            Begin();
        }
        else if (actionCommand.Equals(_registerButton.name))
        {
            AddUser();
        }
    }

    public void UpdateUI()
    {
        throw new System.NotImplementedException();
    }


    public void Refresh<T>()
    {
        //if (typeof(T) is IScoreObserver)
        //{

        //}
        //else if (typeof(T) is IRenewalObserver)
        //{

        //}
    }

    /*
     * Followings are attributes.
     */
 

    public GameObject RegisterUserFrame
    {
        get { return _registerUserFrame; }
        set { _registerUserFrame = value; }
    }

    public GameObject PlayerSelectFrame
    {
        get { return _playerSelectFrame; }
        set { _playerSelectFrame = value; }
    }

    public GameObject WelcomeFrame
    {
        get { return _welcomeFrame; }
        set { _welcomeFrame = value; }
    }


    public GameObject GoToWorldMapButton
    {
        get { return _goToWorldMapButton; }
        set { _goToWorldMapButton = value; }
    }

    public GameObject SettingsButton
    {
        get { return _settingsButton; }
        set { _settingsButton = value; }
    }

    public GameObject StartButton
    {
        get { return _startButton; }
        set { _startButton = value; }
    }

    public const string TAG = "[LobbyView]";


    public IController Controller
    {
        get
        {
            return controller;
        }
        set
        {
            controller = value as LobbyController;
        }
    }

    public IModel Model
    {
        get
        {
            return model;
        }
        set
        {
            model = value as UserAdministrator;
        }
    }
}
