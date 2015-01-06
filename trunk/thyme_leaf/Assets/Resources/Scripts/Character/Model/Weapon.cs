using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class Weapon : View
{
    [SerializeField]
    private int criticalDamage;
    [SerializeField]
    private int criticalProbability;

    private GameEntity owner;


    /*
     * Followings are unity callback methods
     */
    protected virtual void Awake()
    {
        owner = transform.parent.GetComponent<GameEntity>();
    }

    /*
     * Followings are attributes
     */
    protected GameEntity Owner
    {
        get { return owner; }
        set { owner = value; }
    }

    public const string TAG = "[Weapon]";
}
