using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class Weapon : View
{
    [SerializeField]
    private int criticalDamage;
    [SerializeField]
    private int criticalProbability;

    /*
     * Followings are attributes
     */
    public const string TAG = "[Weapon]";
}
