using UnityEngine;
using System.Collections;

public class TrovantSpawnerScript : Singleton<TrovantSpawnerScript>
{

    public Hero GetTrovant()
    {
        return GetTrovant(TrovantType.COMMA);
    }

    public Hero GetTrovant(TrovantType type)
    {
        return Spawner.Instance.GetTrovant(type);
    }
}
