using UnityEngine;
using System.Collections;

/// <summary>
/// Memory pool
/// </summary>
public class MessagePool : Singleton<MessagePool>
{
    public const string TAG = "[MessagePool]";

    private const int CAPACITY = 1000;

    private Message[] messages;

    void Awake()
    {
        messages = new Message[CAPACITY];
    }

    public static void Allocate()
    {

    }

    public void Free()
    {

    }
}
