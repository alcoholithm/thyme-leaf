using UnityEngine;
using System.Collections;

/// <summary>
/// This imitates like android looper almost.
/// </summary>
public class Looper : Singleton<Looper> {
    public new const string TAG = "[Looper]";

    private MessageQueue messageQueue = MessageQueue.Instance;

    public static void Prepare() { }
    public static void Loop() { }
    public static void Quit() { }
}
