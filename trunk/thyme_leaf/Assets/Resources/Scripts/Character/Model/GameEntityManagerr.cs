﻿using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class GameEntityManagerr
{
    [SerializeField]
    private List<W_Chat> w_chats;

    [SerializeField]
    private int ss;

    public int Ss
    {
        get { return ss; }
        set { ss = value; }
    }
}