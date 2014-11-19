using UnityEngine;
using System.Collections;

/// <summary>
/// Memory pool
/// </summary>
public class MessagePool : Singleton<MessagePool>
{
    public new const string TAG = "[MessagePool]";

    private const int CAPACITY = 1000;

    private static Message[] messages;

    private BitArray enabledArr = new BitArray(CAPACITY, true);

    private int usingValue; // 지금 사용하고 있는 메시지의 수

    // 아이디어 1. 저장소를 두개 쓰는 방법 (가용, 불용)
    // 가용에서 불용으로, 불용에서 가용으로
    // 아이디어 2. 저장소는 한개 쓰되 int값으로 조절
    // 쓰이고 있는 객체는 뒤로 보내고
    // 반납된 객체는 현재 쓰이고 있는 객체의 맨 앞에 놈과 바꿈
    void Awake()
    {
        messages = new Message[CAPACITY];
        for (int i = 0; i < messages.Length; i++)
        {
            messages[i] = new Message();
        }
    }

    public Message Allocate()
    {
        for (int i = 0; i < enabledArr.Length; i++)
        {
            if (enabledArr[i])
                return messages[i];
        }

        return DynamicInstantiate();
    }

    public void Free()
    {

    }

    bool IsFull()
    {
        return false;
    }

    Message DynamicInstantiate()
    {
        return new Message();
    }
}

