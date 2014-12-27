using UnityEngine;
using System.Collections;

public class SpawnRPCScript : MonoBehaviour {

    //[RPC]
    //void INIT_SPAWNED_OBJECT(NetworkViewID id)
    //{
    //    Transform ttt = GameObject.Find("Pool").transform;
    //    GameObject g = NetworkView.Find(id).gameObject;
    //    g.transform.parent = ttt;
    //    g.SetActive(false);
    //}

    [RPC]
    void INIT_SPAWNED_OBJECT(NetworkViewID babyId)
    {
        GameObject baby = NetworkView.Find(babyId).gameObject;
        baby.transform.parent = transform;
        baby.SetActive(false);
    }
}
