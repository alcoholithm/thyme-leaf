using UnityEngine;
using System.Collections;

public class LobbyView : View, IActionListener
{
    //---------------------MVC
    private LobbyController controller;
    private UserAdministrator model;
    //---------------------

    [SerializeField]
    private GameObject _startButton;
    [SerializeField]
    private GameObject _registerUserFrame;

    [SerializeField]
    private GameObject _playerSelectFrame;
    [SerializeField]
    private GameObject _welcomeFrame;

    [SerializeField]
    private GameObject _goToWorldMapButton;

    [SerializeField]
    private GameObject _settingsButton;
    [SerializeField]
    private GameObject _registerButton;
    [SerializeField]
    private UILabel _userName;

    // 처음 접속하는 것인지 확인하는 변수, 초기값: false
    public static bool isGameReturn = false;

    /*
     * followings are unity callback methods
     */
    void Awake()
    {
        // mvc
        this.model = UserAdministrator.Instance;
        this.controller = new LobbyController(this, this.model);

        //this.model.RegisterObserver(this, ObserverTypes.Lobby);

        // set children
        this.Add(_playerSelectFrame.GetComponent<PlayerSelectFrame>());
        this.Add(_welcomeFrame.GetComponent<WelcomeFrame>());
    }

    void OnEnable()
    {
        if(isGameReturn)
        {
            controller.BackLobby();
        }
    }

    /*
     * followings are public member functions
     */
    public void PrepareLobby()
    {
        SetVisible(StartButton, false);
        SetVisible(SettingsButton, true);
        SetVisible(GoToWorldMapButton, true);
        WelcomeRefresh();
    }

    public void HideLobby()
    {
        SetVisible(SettingsButton, false);
        SetVisible(GoToWorldMapButton, false);
        SetVisible(WelcomeFrame, false);
        SetVisible(PlayerSelectFrame, false);
    }

    public void SetVisible(GameObject gameObject, bool active)
    {
        gameObject.SetActive(active);
    }

    public void WelcomeRefresh()
    {
        SetVisible(WelcomeFrame, false);
        SetVisible(WelcomeFrame, true);
    }

    public void CloseRegister()
    {
        SetVisible(RegisterUserFrame, false);
        SetVisible(StartButton, true);
    }

    /*
    * followings are implemented methods of "IActionListener"
    */
    public void ActionPerformed(GameObject source)
    {
        if (source.Equals(_startButton))
        {
            isGameReturn = true;
            controller.Start();
        }
        else if (source.Equals(_registerButton))
        {
            controller.Register(_userName.text);
        }
        else if (source.Equals(_goToWorldMapButton))
        {
            SceneManager.Instance.CurrentScene = SceneManager.WORLD_MAP;
        }
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

    public LobbyController Controller
    {
        get { return controller; }
        set { controller = value; }
    }

    public UserAdministrator Model
    {
        get { return model; }
        set { model = value; }
    }

    public const string TAG = "[LobbyView]";


}
