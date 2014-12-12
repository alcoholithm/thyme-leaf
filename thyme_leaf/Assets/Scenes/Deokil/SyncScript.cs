using UnityEngine;
using System.Collections;

public class SyncScript : MonoBehaviour {

    private Vector3 currPos;
    

	// Use this for initialization
	void Start () {
        currPos = new Vector3(0, 0, 0);
	}

	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * Time.deltaTime);
            Debug.Log("up");
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * Time.deltaTime);
            Debug.Log("down");
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * Time.deltaTime);
            Debug.Log("down");
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * Time.deltaTime);
            Debug.Log("down");
        }
	}


    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        if (stream.isWriting)
        {
            Vector3 writePos = currPos;
            stream.Serialize(ref writePos);
        }
        else
        {
            Vector3 receivePos = new Vector3(0,0,0);
            stream.Serialize(ref receivePos);
            currPos = receivePos;
        }
    }
}
