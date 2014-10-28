using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour
{
    private bool _isLoaded = false;

    public void DontDestroy(Object target)
    {
        if (!_isLoaded)
        {
            DontDestroyOnLoad(target);
            _isLoaded = true;
        }
    }
}
