using UnityEngine;
using System.Collections;

public class ModalController
{
    private ModalView view;

    public ModalController(ModalView view)
    {
        this.view = view;
    }

    public void ToggleSettingFrame()
    {
        view.Settings.gameObject.SetActive(!view.Settings.gameObject.activeInHierarchy); // toggle
    }
}

public class ModalView : View, IActionListener
{
    //mvc
    private ModalController controller;

    // set children
    [SerializeField]
    GameObject _settingsButton;

    [SerializeField]
    GameObject _settings;

    /*
     * followings are unity callback methods.
     */ 
    void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        //this.Add(_settings.GetComponent<Setting>());
        this.controller = new ModalController(this);
    }

    /*
     * Followings are implemented methods of "IActionListener"
     */ 
    public void ActionPerformed(GameObject source)
    {
        if (source.Equals(_settingsButton))
        {
            controller.ToggleSettingFrame();
        }
    }

    /*
     * Followings are Atrributes
     */ 
    public GameObject Settings
    {
      get { return _settings; }
      set { _settings = value; }
    }

    public const string TAG = "[ModalView]";
}
