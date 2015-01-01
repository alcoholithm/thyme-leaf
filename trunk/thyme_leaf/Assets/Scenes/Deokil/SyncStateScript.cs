using UnityEngine;
using System.Collections;

public class SyncStateScript : MonoBehaviour
{

    private Vector3 currPos;
    private Hero hero;

    // Use this for initialization
    void Start()
    {
        currPos = transform.position;
        if (hero == null)
            hero = gameObject.GetComponent<Hero>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Network.peerType != NetworkPeerType.Disconnected)
            if (hero != null && hero.controller.isGesture() && networkView.isMine)
            {
                networkView.RPC("OnArriveBranch", RPCMode.Others, transform.position);
            }
    }

    [RPC]
    void OnArriveBranch(Vector3 position)
    {
        transform.position = position;
    }

    public void NetworkInitTower(BattleModel model)
    {
        if (gameObject.networkView.isMine)
            networkView.RPC("OnNetworkInitTower", RPCMode.All, model.SelectedObject.networkView.viewID);
    }

    [RPC]
    void OnNetworkInitTower(NetworkViewID parentViewID)
    {
        Transform parentTransform = NetworkView.Find(parentViewID).transform;
        gameObject.transform.parent = parentTransform;
        gameObject.transform.localScale = Vector3.one;
        gameObject.transform.position = parentTransform.position;
        gameObject.GetComponent<Agt_Type1>().StateMachine.ChangeState(TowerState_Building.Instance);
    }
        
    //void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    //{
    //    Debug.Log("OnSerializeNetworkView");
    //    Vector3 syncPos = Vector3.zero;
    //    if (stream.isWriting)
    //    {
    //        syncPos = currPos;
    //        stream.Serialize(ref syncPos);
    //    }
    //    else
    //    {
    //        stream.Serialize(ref syncPos);
    //        currPos = syncPos;
    //    }
    //}
    
}
