using UnityEngine;
using System.Collections;

/// <summary>
/// for UI
/// </summary>
public interface IView
{
    //void SetVisible(GameObject gameObject, bool active);
    //void ActionPerformed(string actionCommand);
    void Add(IView view);
    void Remove(IView view);
    IView GetChild(int index);
    void UpdateUI();
}
