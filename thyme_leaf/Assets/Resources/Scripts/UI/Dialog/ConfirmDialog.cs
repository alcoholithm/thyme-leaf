﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Facade of MessageDialog
/// </summary>
public class ConfirmDialog : MonoBehaviour
{
    [SerializeField]
    private UILabel _title;
    [SerializeField]
    private UILabel _message;

    void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
    }

    public void SetMessage(string message)
    {
        _message.text = message;
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

}
