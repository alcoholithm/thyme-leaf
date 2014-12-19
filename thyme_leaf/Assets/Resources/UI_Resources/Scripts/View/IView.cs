using UnityEngine;
using System.Collections;

/// <summary>
/// for UI
/// </summary>
public interface IView
{
    //void SetVisible(GameObject gameObject, bool active);
    void ActionPerformed(string actionCommand);
    void UpdateUI();
}
