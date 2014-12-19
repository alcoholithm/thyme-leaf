using UnityEngine;
using System.Collections;

public class ClientHeroSpawner : HeroSpawner {
    void Awake()
    {
        if (Network.isClient)
        {
            foreach (GameObject automat in automats)
            {
                ObjectPoolingManager.Instance.CreatePool(gameObject, automat, initPoolSize, maxPoolSize, false);
            }
        }
        else
        {
            Destroy(this);
        }
    }
}
