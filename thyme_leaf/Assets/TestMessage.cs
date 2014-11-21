using UnityEngine;
using System.Collections;

public class TestMessage : MonoBehaviour {

    public Tower receiver;

    public void Send()
    {
        Message msg = MessageSystem.Instance.ObtainMessage();
        msg.arg1 = 3;
        msg.arg2 = 30;
        msg.receiver = receiver;

        MessageSystem.Instance.Dispatch(msg);
    }
}
