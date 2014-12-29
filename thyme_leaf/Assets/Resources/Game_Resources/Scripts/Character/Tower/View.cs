﻿using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Class "View" is the root of the all views. Every view has "View" as a superclass.
/// </summary>
public abstract class View : MonoBehaviour, IView
{
    protected List<IView> views;

    public void Add(IView view)
    {
        if (views == null)
            views = new List<IView>(1);

        views.Add(view);
    }

    public void Remove(IView view)
    {
        if (views != null)
            views.Remove(view);
        else
            throw new System.NotSupportedException();
    }

    public IView GetChild(int index)
    {
        if (views != null)
            return views[index];

        throw new System.NotSupportedException();
    }

    public virtual void UpdateUI()
    {
        if (views != null)
            views.ForEach(v => v.UpdateUI());

        throw new System.NotSupportedException();
    }
}
