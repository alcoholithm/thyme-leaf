using UnityEngine;
using System.Collections;

/// <summary>
/// Facade of MessageDialog
/// </summary>
public class MessageDialog : MonoBehaviour
{
    [SerializeField]
    UILabel _title;
    [SerializeField]
    UILabel _message;

    public void SetMessage(string message)
    {
        _message.text = message;
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
