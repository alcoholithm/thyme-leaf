using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// for UI
/// </summary>
public interface IView
{
    View Parent { get; set; }
    List<IView> Views { get; set; }

    void Add(IView view);
    void Remove(IView view);
    IView GetChild(int index);
    void PrepareUI();
    void UpdateUI();
}
