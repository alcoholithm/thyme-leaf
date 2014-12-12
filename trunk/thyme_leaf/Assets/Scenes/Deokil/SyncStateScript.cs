using UnityEngine;
using System.Collections;

public class SyncStateScript : MonoBehaviour
{

    private Vector3 currPos;

    // Use this for initialization
    void Start()
    {
        currPos = transform.position;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (!Network.isServer)
    //    {
    //        if (Input.GetKey(KeyCode.UpArrow))
    //        {
    //            transform.Translate(Vector3.up * Time.deltaTime * 2);
    //        }

    //        if (Input.GetKey(KeyCode.DownArrow))
    //        {
    //            transform.Translate(Vector3.down * Time.deltaTime * 2);
    //        }

    //        if (Input.GetKey(KeyCode.RightArrow))
    //        {
    //            transform.Translate(Vector3.right * Time.deltaTime * 2);
    //        }

    //        if (Input.GetKey(KeyCode.LeftArrow))
    //        {
    //            transform.Translate(Vector3.left * Time.deltaTime * 2);
    //        }
    //    }
    //}

    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        Debug.Log("OnSerializeNetworkView");
        Vector3 syncPos = Vector3.zero;
        if (stream.isWriting)
        {
            syncPos = currPos;
            stream.Serialize(ref syncPos);
        }
        else
        {
            stream.Serialize(ref syncPos);
            currPos = syncPos;
        }
    }
}
