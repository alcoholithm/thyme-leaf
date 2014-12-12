using UnityEngine;
using System.Collections;

public abstract class Spawner<T> : MonoBehaviour {

	public const string TAG = "[Spawner]";

	public T Allocate()
	{
		return DynamicInstantiate();
	}

	public void Free(GameObject gameObject)
	{
		Destroy(gameObject);
	}

	protected abstract T DynamicInstantiate();
}
