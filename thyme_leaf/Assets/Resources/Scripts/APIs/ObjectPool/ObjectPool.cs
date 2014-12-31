using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class ObjectPool
{
    private List<GameObject> pooledObjects;
    private GameObject pooledObj;
    private int maxPoolSize;
    private int initialPoolSize;

    private static GameObject poolMgr;
    private GameObject spawner = null;

    public ObjectPool(GameObject spawner, GameObject obj, UnitType unitType, int type, int initialPoolSize, int maxPoolSize, bool shouldShrink)
    {
        if (this.spawner == null)
        {
            this.spawner = spawner;
        }

        if (Network.isClient)
        {
            return;
        }

        pooledObjects = new List<GameObject>();

        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject nObj = null;
            if (Network.peerType == NetworkPeerType.Disconnected)
            {
                //single-play
                nObj = GameObject.Instantiate(obj, Vector3.zero, Quaternion.identity) as GameObject;
                nObj.transform.parent = this.spawner.transform;
                nObj.SetActive(false);
            }            
            else if (Network.isServer)
            {
                //multi-play
                NetworkViewID viewID = Network.AllocateViewID();

                //nObj = GameObject.Instantiate(obj, Vector3.zero, Quaternion.identity) as GameObject;


                //nObj = Network.Instantiate(obj, Vector3.one, Quaternion.identity, 0) as GameObject;
                

                //spawner.GetComponent<NetworkView>().networkView.RPC(RPCMethod.INIT_SPAWNED_OBJECT, RPCMode.All, nObj);
                                
                
                //nObj.networkView.viewID = Network.AllocateViewID();
                // Network.Instantiate method allocates network viewID automatically
                Spawner.Instance.GetComponent<NetworkView>().networkView.RPC("TEST", RPCMode.All, 
                    this.spawner.networkView.viewID, viewID, unitType, type);

                Debug.Log(this.spawner + " creates " + nObj + " (" + nObj.networkView.viewID + ") " + " that's parent is " + nObj.transform.parent);
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
                if (Network.peerType != NetworkPeerType.Disconnected)
                {
                    if (spawner != null)
                    {
                        spawner.networkView.RPC("ACTIVE_OBJECT", RPCMode.All, pooledObjects[i].networkView.viewID);
                    }
                    else
                    {
                        Debug.Log("Spawner is null in " + GetHashCode());
                    }
                }
                else
                {
                    pooledObjects[i].SetActive(true);
                }
                return pooledObjects[i];
            }
        }

        if (this.maxPoolSize > this.pooledObjects.Count)
        {
            GameObject nObj = null;
            if (Network.peerType == NetworkPeerType.Connecting)
            {
                nObj = Network.Instantiate(pooledObj, Vector3.zero, Quaternion.identity, 0) as GameObject;
            }
            else
            {
                nObj = GameObject.Instantiate(pooledObj, Vector3.zero, Quaternion.identity) as GameObject;
            }
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