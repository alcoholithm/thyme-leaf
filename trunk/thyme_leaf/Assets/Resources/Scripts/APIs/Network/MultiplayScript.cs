using UnityEngine;
using System.Collections;

public class MultiplayScript : MonoBehaviour {
    public MultiplayType multiplayType;

    void Start()
    {
        if (Network.peerType == NetworkPeerType.Disconnected)
        {
            Destroy(this);
        }
        else if (!networkView.isMine)
        {
            Destroy(gameObject.GetComponent<UIButton>());
        }
    }
}
