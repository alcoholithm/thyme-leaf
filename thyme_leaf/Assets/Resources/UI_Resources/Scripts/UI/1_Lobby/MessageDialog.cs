using UnityEngine;
using System.Collections;

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
}
