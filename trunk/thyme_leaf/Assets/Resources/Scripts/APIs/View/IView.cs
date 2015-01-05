using UnityEngine;
using System.Collections;

/// <summary>
/// for UI
/// </summary>
public interface IView
{
    void Add(IView view);
    void Remove(IView view);
    IView GetChild(int index);
    void PrepareUI();
    void UpdateUI();
}
