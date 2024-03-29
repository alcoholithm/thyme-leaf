﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Memory pool
/// </summary>
public class MemoryPool<T>
{
    public const string TAG = "[MemoryPool]";

    private const int CAPACITY = 1000;

    //private T[] objectList;

    //private BitArray enabledArr = new BitArray(CAPACITY, true);

    //private int usingValue; // 지금 사용하고 있는 메시지의 수

    // 아이디어 1. 저장소를 두개 쓰는 방법 (가용, 불용)
    // 가용에서 불용으로, 불용에서 가용으로
    // 아이디어 2. 저장소는 한개 쓰되 int값으로 조절
    // 쓰이고 있는 객체는 뒤로 보내고
    // 반납된 객체는 현재 쓰이고 있는 객체의 맨 앞에 놈과 바꿈

    // 힙이 아닌 스택에서 관리하도록 하자!
    public MemoryPool()
    {
        //messages = new T[CAPACITY];
        //for (int i = 0; i < messages.Length; i++)
        //{
        //    messages[i] = new T();
        //}
    }

    public MemoryPool(T[] objList)
    {
        //this.objectList = objList;
    }

    public T Allocate()
    {
        //for (int i = 0; i < enabledArr.Length; i++)
        //{
        //    if (enabledArr[i])
        //        return messages[i];
        //}

        throw new System.NotImplementedException();
    }

    public void Free()
    {
        throw new System.NotImplementedException();
    }

    bool IsFull()
    {
        return false;
    }
}

