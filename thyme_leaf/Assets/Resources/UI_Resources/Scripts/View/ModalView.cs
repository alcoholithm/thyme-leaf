using UnityEngine;
using System.Collections;

public class ModalView : View, IActionListener
{
    [SerializeField]
    GameObject _settingsButton;

    [SerializeField]
    GameObject _settings;

    public void ActionPerformed(string actionCommand)
    {
        if (actionCommand.Equals(_settingsButton.name))
        {
            _settings.SetActive(true);
        }
    }
}
