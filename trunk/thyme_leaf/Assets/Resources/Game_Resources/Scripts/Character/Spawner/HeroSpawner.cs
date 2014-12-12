using UnityEngine;
using System.Collections;

public sealed class HeroSpawner : Spawner<Hero>
{
	public new const string TAG = "[AutomatSpawner]";

	public Transform prefab;

	private static volatile HeroSpawner instance;
	private static object locker = new Object();

	private HeroSpawner(){}

	public static HeroSpawner Instance
	{
		get 
		{
			if(instance == null)
			{
				lock(locker)
				{
					GameObject singleton = new GameObject();
					instance = singleton.AddComponent<HeroSpawner>();
					singleton.name = typeof(HeroSpawner).ToString();
				}
			}
			return instance;

		}
	}

	protected override Hero DynamicInstantiate()
    {
        GameObject go;
        if (!Network.isServer && !Network.isClient)
        {
			Debug.Log("Transform : "+transform);
			Debug.Log("Child Count : "+transform.childCount);
//            go = Instantiate(transform.GetChild(0).gameObject) as GameObject;
			go = Instantiate(prefab) as GameObject;
        }
        else
        {
            Debug.Log("N_Instantiate");
//            Transform trans = transform.GetChild(0);
//            GameObject obj = Network.Instantiate(trans, transform.position, transform.rotation, 0) as GameObject;
//            return obj.GetComponent<Hero>();
			return null;
        }

        return go.GetComponent<Hero>();
    }

    
}