using UnityEngine;
using System.Collections;

public interface IView
{
    void UpdateUI();
    void ActionPerformed(string ActionCommand);
}
