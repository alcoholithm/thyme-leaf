using UnityEngine;
using System.Collections;

public interface IView
{
    //void SetVisible(GameObject gameObject, bool active);
    void ActionPerformed(string actionCommand);
    void UpdateUI();
}
