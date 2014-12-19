using UnityEngine;
using System.Collections;

public class HostHeroSpawner : HeroSpawner
{
    void Awake()
    {
        if (Network.isServer)
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
