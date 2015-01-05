using UnityEngine;
using System.Collections;

/// <summary>
/// Facade of InputDialog
/// </summary>
public class InputDialog : MonoBehaviour
{
    void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

}
