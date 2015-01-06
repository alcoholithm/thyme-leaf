using UnityEngine;
using System.Collections;

public class SettingData
{
    public SettingData()
    {
        RememberUserNameEnabled = PlayerPrefs.GetInt(Settings.REMEMBER_USERNAME_ENABLED) == 0 ? false : true;
        UserName = PlayerPrefs.GetString(Settings.USERNAME);

        MusicEnabled = PlayerPrefs.GetInt(Settings.MUSIC_ENABLED) == 0 ? false : true;
        MusicVolume = PlayerPrefs.GetFloat(Settings.MUSIC_VOLUME);

        SoundEffectsEnabled = PlayerPrefs.GetInt(Settings.SOUNDEFFECTS_ENABLED) == 0 ? false : true;
        SoundEffectsVolume = PlayerPrefs.GetFloat(Settings.SOUNDEFFECTS_VOLUME);
    }

    public SettingData(SettingData other)
    {
        this._rememberUserNameEnabled = other._rememberUserNameEnabled;
        this._userName = other._userName;
        this._musicEnabled = other._musicEnabled;
        this._musicVolume = other._musicVolume;
        this._soundEffectsEnabled = other._soundEffectsEnabled;
        this._soundEffectsVolume = other._soundEffectsVolume;
    }

    private bool _rememberUserNameEnabled;
    public bool RememberUserNameEnabled
    {
        get { return _rememberUserNameEnabled; }
        set { _rememberUserNameEnabled = value; }
    }

    private string _userName;
    public string UserName
    {
        get { return _userName; }
        set { _userName = value; }
    }

    private bool _musicEnabled;
    public bool MusicEnabled
    {
        get { return _musicEnabled; }
        set { _musicEnabled = value; }
    }

    private float _musicVolume;
    public float MusicVolume
    {
        get { return _musicVolume; }
        set { _musicVolume = value; }
    }

    private bool _soundEffectsEnabled;
    public bool SoundEffectsEnabled
    {
        get { return _soundEffectsEnabled; }
        set { _soundEffectsEnabled = value; }
    }

    private float _soundEffectsVolume;
    public float SoundEffectsVolume
    {
        get { return _soundEffectsVolume; }
        set { _soundEffectsVolume = value; }
    }
}

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

    public static SettingData CurrentSettingData = new SettingData();
    public static SettingData PrevSettingData = new SettingData();

    public static void Awake() // 프로그램이 실행되면 세팅값대로 세팅되어야하니까 전의 세팅을 불러와야됨 그래서 넣어줌 프로그램 매니저에서 세팅을 관리할수도잇을듯
    {
        if (PlayerPrefs.GetInt(INIT_SETTING) == 0)
        {
            Debug.Log("INIT SETTING");
            InitValues();
        }
        ImportValues(ref PrevSettingData);
        ImportValues(ref CurrentSettingData);
        Debug.Log("Settings Frame Awkae()");
        AudioManager.Instance.UpdateValues(); 
    }

    public static void InitValues()
    {
        Debug.Log("initiate player prefs");
        PlayerPrefs.SetInt(INIT_SETTING, 1);
        PlayerPrefs.SetInt(MUSIC_ENABLED, 1);
        PlayerPrefs.SetFloat(MUSIC_VOLUME, 1.0f);
        PlayerPrefs.SetInt(SOUNDEFFECTS_ENABLED, 1);
        PlayerPrefs.SetFloat(SOUNDEFFECTS_VOLUME, 1.0f);
    }

    public static void ImportValues(ref SettingData settingData)
    {
        Debug.Log("import player prefs values into settings");
        settingData.RememberUserNameEnabled = PlayerPrefs.GetInt(REMEMBER_USERNAME_ENABLED) == 0 ? false : true;
        settingData.UserName = PlayerPrefs.GetString(USERNAME);

        settingData.MusicEnabled = PlayerPrefs.GetInt(MUSIC_ENABLED) == 0 ? false : true;
        settingData.MusicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME);

        settingData.SoundEffectsEnabled = PlayerPrefs.GetInt(SOUNDEFFECTS_ENABLED) == 0 ? false : true;
        settingData.SoundEffectsVolume = PlayerPrefs.GetFloat(SOUNDEFFECTS_VOLUME);
    }

    public static void ExportValues()
    {
        ExportValues(ref CurrentSettingData);
    }

    public static void ExportValues(ref SettingData settingData)
    {
        Debug.Log("export settings values into player prefs");
        PlayerPrefs.SetInt(REMEMBER_USERNAME_ENABLED, settingData.RememberUserNameEnabled ? 1 : 0);
        PlayerPrefs.SetString(USERNAME, settingData.UserName);

        PlayerPrefs.SetInt(MUSIC_ENABLED, settingData.MusicEnabled ? 1 : 0);
        PlayerPrefs.SetFloat(MUSIC_VOLUME, settingData.MusicVolume);

        PlayerPrefs.SetInt(SOUNDEFFECTS_ENABLED, settingData.SoundEffectsEnabled ? 1 : 0);
        PlayerPrefs.SetFloat(SOUNDEFFECTS_VOLUME, settingData.SoundEffectsVolume);
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

    private bool MusicEnabled;
    private bool SoundEffectsEnabled;
    private float MusicVolume;
    private float SoundEffectsVolume;

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

        //MusicEnabled = Settings.MusicEnabled;
        //SoundEffectsEnabled = Settings.SoundEffectsEnabled;
        //MusicVolume = Settings.MusicVolume;
        //SoundEffectsVolume = Settings.SoundEffectsVolume;
    }

    /*
     * Followings are implemented methods of "IActionListener"
     */ 
    public void ActionPerformed(GameObject source)
    {
        if (source.Equals(_closeButton)){
            Debug.Log("Canceled Setting");
            Settings.CurrentSettingData = new SettingData(Settings.PrevSettingData);
            controller.Okay();            
        }
        else if(source.Equals(_okayButton))
        {
            Debug.Log("Saved Setting");
            Settings.PrevSettingData = new SettingData(Settings.CurrentSettingData);
            Settings.ExportValues();
            controller.Okay();
        }
        else if (source.Equals(_musicCheckBox))
        {
            Settings.CurrentSettingData.MusicEnabled = !source.GetComponent<UIToggle>().value;
        }
        else if (source.Equals(_musicSlider))
        {
            Settings.CurrentSettingData.MusicVolume = source.GetComponent<UISlider>().value;
        }
        else if (source.Equals(_soundFXCheckBox))
        {
            Settings.CurrentSettingData.SoundEffectsEnabled = !source.GetComponent<UIToggle>().value;
        }
        else if (source.Equals(_soundFXSlider))
        {
            Settings.CurrentSettingData.SoundEffectsVolume = source.GetComponent<UISlider>().value;
        }
        AudioManager.Instance.UpdateValues();
    }
}