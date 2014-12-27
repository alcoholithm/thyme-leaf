using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class ObjectPool : MonoBehaviour
{
    private List<GameObject> pooledObjects;
    private GameObject pooledObj;
    private int maxPoolSize;
    private int initialPoolSize;

    [RPC]
    void ttt(NetworkViewID id)
    {
        Transform ttt = GameObject.Find("Pool").transform;
        GameObject g = NetworkView.Find(id).gameObject;
        g.transform.parent = ttt;
        g.SetActive(false);
    }

    public ObjectPool(GameObject spawner, GameObject obj, int initialPoolSize, int maxPoolSize, bool shouldShrink)
    {
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject nObj = null;
            if (Network.peerType == NetworkPeerType.Disconnected)
            {
                //single play
                nObj = GameObject.Instantiate(obj, Vector3.zero, Quaternion.identity) as GameObject;
                nObj.transform.parent = obj.transform;
                nObj.SetActive(false);
            }
            else
            {
                //multi play
                nObj = Network.Instantiate(obj, Vector3.zero, Quaternion.identity, 0) as GameObject;
                //nObj.networkView.viewID = Network.AllocateViewID();
                Debug.Log(spawner + " creates " + nObj +" ("+ nObj.networkView.viewID +") "+" that's parent is " + nObj.transform.parent);

                //Transform ttt = GameObject.Find("Pool").transform;
                //nObj.transform.parent = ttt;
                //nObj.SetActive(false);
                GameObject.Find("Pool").gameObject.GetComponent<NetworkView>().networkView.RPC("ttt",RPCMode.All,nObj.networkView.viewID);
            }

            pooledObjects.Add(nObj);
            GameObject.DontDestroyOnLoad(nObj);
        }

        this.maxPoolSize = maxPoolSize;
        this.pooledObj = obj;
        this.initialPoolSize = initialPoolSize;

        /*
        if (shouldShrink)
        {
            //listen to the game state manager's event for all pools should shrink
            //back to their initial size.
            GameStateManager.Instance.ShrinkPools += new GameStateManager.GameEvent(this.Shrink);
        }
        */
    }

    public GameObject GetObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {            
            if (pooledObjects[i].activeSelf == false)
            {
                pooledObjects[i].SetActive(true);
                return pooledObjects[i];
            }
        }

        if (this.maxPoolSize > this.pooledObjects.Count)
        {            
            GameObject nObj = GameObject.Instantiate(pooledObj, Vector3.zero, Quaternion.identity) as GameObject;
            nObj.SetActive(true);
            pooledObjects.Add(nObj);
            return nObj;
        }
        return null;
    }

    /// <param name="eventArgs">The arguments for this event.</param>
    public void Shrink(object sender)
    {
        int objectsToRemoveCount = pooledObjects.Count - initialPoolSize;
        if (objectsToRemoveCount <= 0) return;

        for (int i = pooledObjects.Count - 1; i >= 0; i--)
        {
            if (!pooledObjects[i].activeSelf)
            {
                GameObject obj = pooledObjects[i];
                pooledObjects.Remove(obj);
            }
        }
    }

}