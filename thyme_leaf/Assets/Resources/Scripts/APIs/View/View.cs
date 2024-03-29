﻿using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Class "View" is the root of the all views.
/// Every view has "View" as a superclass.
/// </summary>
public abstract class View : MonoBehaviour, IView
{
    private View parent;
    private List<IView> views;

    public void Add(IView view)
    {
        if (views == null)
            views = new List<IView>(1);

        view.Parent = this;
        views.Add(view);
    }

    public void Remove(IView view)
    {
        if (views != null)
            views.Remove(view);
        else
            Debug.LogError(new System.NotSupportedException());

    }

    public IView GetChild(int index)
    {
        if (views != null)
            return views[index];

        Debug.LogError(new System.NotSupportedException());
        return null;
    }

    public virtual void PrepareUI()
    {
        if (views != null)
            views.ForEach(v => v.PrepareUI());
        else
            Debug.LogError(new System.NotSupportedException());
    }

    public virtual void UpdateUI()
    {
        if (views != null)
            views.ForEach(v => v.UpdateUI());
        else
            Debug.LogError(new System.NotSupportedException());
    }


    /*
     * Followings are attributes
     */ 
    public View Parent
    {
        get { return parent; }
        set { parent = value; }
    }

    public List<IView> Views
    {
        get { return views; }
        set { views = value; }
    }
}
