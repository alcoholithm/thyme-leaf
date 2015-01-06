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
        else if ((Network.isServer && multiplayType != MultiplayType.SERVER)
            || (Network.isClient && multiplayType != MultiplayType.CLIENT))
        {
            Destroy(gameObject.GetComponent<UIButton>());
        }
    }
}
