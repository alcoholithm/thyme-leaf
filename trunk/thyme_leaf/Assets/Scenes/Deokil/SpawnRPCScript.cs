using UnityEngine;
using System.Collections;

public class SpawnRPCScript : MonoBehaviour {

    [RPC]
    void INIT_SPAWNED_OBJECT(NetworkViewID babyId)
    {
        GameObject baby = NetworkView.Find(babyId).gameObject;
        baby.transform.parent = transform;
        baby.SetActive(false);
    }

    [RPC]
    void ACTIVE_OBJECT(NetworkViewID id)
    {
        GameObject baby = NetworkView.Find(id).gameObject;
        baby.SetActive(true);
        Debug.Log("ACTIVE_OBJECT " + id);
    }
}
