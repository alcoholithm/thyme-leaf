using UnityEngine;
using System.Collections;

public class HeroSpawnerScript : Singleton<HeroSpawnerScript> {

    // use only test
    public Hero GetHero()
    {
        return GetHero(AutomatType.FRANSIS_TYPE1);
    }

    public Hero GetHero(AutomatType type)
    {
        return Spawner.Instance.GetHero(type);
    }
}
