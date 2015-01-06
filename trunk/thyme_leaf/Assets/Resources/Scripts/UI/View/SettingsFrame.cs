using UnityEngine;
using System.Collections;

public class Settings
{
    //login properties
    public const string REMEMBER_USERNAME_ENABLED = "RememberUserNameEnabled";
    public const string USERNAME = "UserName";

    //music properties
    public const string INIT_SETTING = "INIT";
    public const string MUSIC_ENABLED = "MusicEnabled";
    public const string MUSIC_VOLUME = "MusicVolume";
    public const string SOUNDEFFECTS_ENABLED = "SoundEffectsEnabled";
    public const string SOUNDEFFECTS_VOLUME = "SoundEffectsVolume";

    private static bool _rememberUserNameEnabled;
    public static bool RememberUserNameEnabled
    {
        get { return _rememberUserNameEnabled; }
        set { _rememberUserNameEnabled = value; }
    }

    private static string _userName;
    public static string UserName
    {
        get { return _userName; }
        set { _userName = value; }
    }

    private static bool _musicEnabled;
    public static bool MusicEnabled
    {
        get { return _musicEnabled; }
        set { _musicEnabled = value; }
    }

    private static float _musicVolume;
    public static float MusicVolume
    {
        get { return _musicVolume; }
        set { _musicVolume = value; }
    }

    private static bool _soundEffectsEnabled;
    public static bool SoundEffectsEnabled
    {
        get { return _soundEffectsEnabled; }
        set { _soundEffectsEnabled = value; }
    }

    private static float _soundEffectsVolume;
    public static float SoundEffectsVolume
    {
        get { return _soundEffectsVolume; }
        set { _soundEffectsVolume = value; }
    }

    void Awake() // 프로그램이 실행되면 세팅값대로 세팅되어야하니까 전의 세팅을 불러와야됨 그래서 넣어줌 프로그램 매니저에서 세팅을 관리할수도잇을듯
    {
        if (PlayerPrefs.GetInt(INIT_SETTING) == 0)
            InitValues();
        else
            ImportValues();
        //DontDestroy(this.gameObject);
    }

    public static void InitValues()
    {
        PlayerPrefs.SetInt(INIT_SETTING, 1);
        PlayerPrefs.SetInt(MUSIC_ENABLED, 1);
        PlayerPrefs.SetFloat(MUSIC_VOLUME, 1.0f);
        PlayerPrefs.SetInt(SOUNDEFFECTS_ENABLED, 1);
        PlayerPrefs.SetFloat(SOUNDEFFECTS_VOLUME, 1.0f);
    }

    public static void ImportValues()
    {
        RememberUserNameEnabled = PlayerPrefs.GetInt(REMEMBER_USERNAME_ENABLED) == 0 ? false : true;
        UserName = PlayerPrefs.GetString(USERNAME);

        MusicEnabled = PlayerPrefs.GetInt(MUSIC_ENABLED) == 0 ? false : true;
        MusicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME);

        SoundEffectsEnabled = PlayerPrefs.GetInt(SOUNDEFFECTS_ENABLED) == 0 ? false : true;
        SoundEffectsVolume = PlayerPrefs.GetFloat(SOUNDEFFECTS_VOLUME);
    }

    public static void ExportValues()
    {
        PlayerPrefs.SetInt(REMEMBER_USERNAME_ENABLED, RememberUserNameEnabled ? 1 : 0);
        PlayerPrefs.SetString(USERNAME, UserName);

        PlayerPrefs.SetInt(MUSIC_ENABLED, MusicEnabled ? 1 : 0);
        PlayerPrefs.SetFloat(MUSIC_VOLUME, MusicVolume);

        PlayerPrefs.SetInt(SOUNDEFFECTS_ENABLED, SoundEffectsEnabled ? 1 : 0);
        PlayerPrefs.SetFloat(SOUNDEFFECTS_VOLUME, SoundEffectsVolume);
    }
}


public class SettingsController
{
    private SettingsFrame view;
    private Settings model;

    public SettingsController(SettingsFrame view, Settings model)
    {
        this.view = view;
        this.model = model;
    }

    public void Okay()
    {
        view.gameObject.SetActive(false);
    }
}

public class SettingsFrame : View, IActionListener
{
    //mvc
    private Settings model;
    private SettingsController controller;

    // children
    [SerializeField] private GameObject _closeButton;
    [SerializeField] private GameObject _okayButton;

    [SerializeField] private GameObject _musicCheckBox;
    [SerializeField] private GameObject _musicSlider;

    [SerializeField] private GameObject _soundFXCheckBox;
    [SerializeField] private GameObject _soundFXSlider;

    /*
     * Followings are unity callback methods
     */ 
    void Awake()
    {
        Initialize();
    }

    /*
     * Followings are member functions
     */ 
    private void Initialize()
    {
        // mvc
        this.model = new Settings();
        this.controller = new SettingsController(this, model);
    }

    /*
     * Followings are implemented methods of "IActionListener"
     */ 
    public void ActionPerformed(GameObject source)
    {
        bool MusicEnabled = Settings.MusicEnabled;
        bool SoundEffectsEnabled = Settings.SoundEffectsEnabled;
        float MusicVolume = Settings.MusicVolume;
        float SoundEffectsVolume = Settings.SoundEffectsVolume;

        if (source.Equals(_closeButton)){
            Debug.Log("Canceled Setting");
            controller.Okay();
            return;
        }
        else if(source.Equals(_okayButton))
        {
            Debug.Log("Saved Setting");
            Settings.MusicEnabled = MusicEnabled;
            Settings.ExportValues();            
            controller.Okay();
        }
        else if (source.Equals(_musicCheckBox))
        {
            MusicEnabled = !MusicEnabled;
        }
        else if (source.Equals(_musicSlider))
        {
            MusicVolume = source.GetComponent<UISlider>().value;
        }
        else if (source.Equals(_soundFXCheckBox))
        {
            SoundEffectsEnabled = !SoundEffectsEnabled;
        }
        else if (source.Equals(_soundFXSlider))
        {
            SoundEffectsVolume = source.GetComponent<UISlider>().value;
        }
    }
}