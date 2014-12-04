using UnityEngine;
using System.Collections;

public class ModalView : MonoBehaviour, IView
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

    public void UpdateUI()
    {
        throw new System.NotImplementedException();
    }
}
