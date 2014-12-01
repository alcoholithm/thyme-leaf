using UnityEngine;
using System.Collections;

public interface IController
{
    IView View
    {
        get;
        set;
    }

    IModel Model
    {
        get;
        set;
    }
}
