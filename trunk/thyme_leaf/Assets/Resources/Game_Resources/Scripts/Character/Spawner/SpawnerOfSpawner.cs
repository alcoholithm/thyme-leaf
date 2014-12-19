using UnityEngine;
using System.Collections;

public class SpawnerOfSpawner : MonoBehaviour {

    public Transform heroSpawner;
    public Transform towerSpawner;
    public Transform projectileSpawner;

	// Use this for initialization
	void Start () {
        GameObject heroSpawnerObj = Instantiate(heroSpawner, Vector3.zero, Quaternion.identity) as GameObject;        
        heroSpawnerObj.transform.parent = transform;

        if (Network.isServer)
        {
            GameObject projectileSpawnerObj = Instantiate(projectileSpawner) as GameObject;
            projectileSpawnerObj.transform.parent = transform;
        }
        else if (Network.isClient)
        {

        }
        else
        {
            GameObject towerSpawnerObj = Instantiate(towerSpawner) as GameObject;
            towerSpawnerObj.transform.parent = transform;
            GameObject projectileSpawnerObj = Instantiate(projectileSpawner) as GameObject;
            projectileSpawnerObj.transform.parent = transform;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
