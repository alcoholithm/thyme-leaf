using UnityEngine;
using System.Collections;

public class ModalView : View, IActionListener
{
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
    }

    public void ActionPerformed(string actionCommand)
    {
        if (actionCommand.Equals(_settingsButton.name))
        {
            _settings.SetActive(true);
        }
    }
}
