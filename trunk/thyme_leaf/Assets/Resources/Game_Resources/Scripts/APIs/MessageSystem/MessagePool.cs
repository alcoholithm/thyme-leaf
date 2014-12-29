using UnityEngine;
using System.Collections;

/// <summary>
/// Class "MessagePool" is memory pool for preventing memory fragmentation
/// </summary>
public class MessagePool
{
    //private MemoryPool<Message> memoryPool;
    //private const int CAPACITY = 1000;

    private MessagePool()
    {
        //Message[] messages = new Message[CAPACITY];
        //for (int i = 0; i < messages.Length; i++)
        //{
        //    messages[i] = new Message();
        //}
        //memoryPool = new MemoryPool<Message>(messages);
    }

    /*
     * followings are member functions
     */
    public Message Allocate()
    {
        //Tower gameEntity = null; 
        //try
        //{
        //    gameEntity = memoryPool.Allocate();
        //}
        //catch (System.Exception e)
        //{
        //    Debug.LogException(e);
        //    gameEntity = DynamicInstantiate();
        //}
        //return gameEntity;

        return DynamicInstantiate();
    }

    public void Free()
    {
        throw new System.NotImplementedException();
    }

    private Message DynamicInstantiate()
    {
        return new Message(new NullCommand());
    }

    public const string TAG = "[MessagePool]";
    private static MessagePool instance = new MessagePool();
    public static MessagePool Instance
    {
        get { return MessagePool.instance; }
    }
}
