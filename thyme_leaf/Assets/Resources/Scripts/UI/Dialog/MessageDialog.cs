using UnityEngine;
using System.Collections;

/// <summary>
/// Facade of MessageDialog
/// </summary>
public class MessageDialog : MonoBehaviour
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

    public void SetTitle(string title)
    {
        _title.text = title;
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

}
