using UnityEngine;
using System.Collections;

/// <summary>
/// Facade of WelcomeFrame
/// </summary>
public class WelcomeFrame : MonoBehaviour
{
    [SerializeField]
    UILabel _userName;

    public void SetUserName(string name)
    {
        _userName.text = name;
    }
}
