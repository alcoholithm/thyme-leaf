using UnityEngine;
using System.Collections;

public class MessagePool : MemoryPool<Message>
{
    private static MessagePool instance = new MessagePool();
    public static MessagePool Instance
    {
        get { return MessagePool.instance; }
    }

    private MessagePool() { }
}
