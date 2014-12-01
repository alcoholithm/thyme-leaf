using UnityEngine;
using System.Collections;

public interface IView
{
    IController Controller
    {
        get;
        set;
    }

    IModel Model
    {
        get;
        set;
    }
    void SetVisible(GameObject gameObject, bool active);
    void ActionPerformed(string ActionCommand);
    void UpdateUI();
}
