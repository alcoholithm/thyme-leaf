using UnityEngine;
using System.Collections;

public class Launcher : Weapon
{
    public Launcher(GameEntity owner)
        : base(owner) { }

    /*
     * Followings are overrided methods of "IWeapon"
     */
    public override void Fire(GameEntity target)
    {
        if (target == null)
            return;

        Projectile projectile = Spawner.Instance.GetProjectile(ProjectileType.POISON);

        if (Network.peerType == NetworkPeerType.Disconnected) // Single mode
        {
            projectile.transform.position = owner.transform.position;
            projectile.transform.localScale = Vector3.one;
            projectile.FireProcess(owner, target);
        }
        else if (projectile.gameObject.networkView.isMine)  // Multi mode
        {
            projectile.gameObject.GetComponent<SyncStateScript>().NetworkInitProjectile(owner, target);
        }
    }
}

public class Flamer : Weapon
{
    public Flamer(GameEntity owner)
        : base(owner) { }


    /*
    * Followings are overrided methods of "IWeapon"
    */
    public override void Fire(GameEntity target)
    {
        if (target == null)
            return;
    }
}

public interface IWeapon
{
    void Fire(GameEntity target);
}

[System.Serializable]
public abstract class Weapon : IWeapon
{
    [SerializeField]
    private int criticalDamage;
    [SerializeField]
    private int criticalProbability;

    protected GameEntity owner;

    public Weapon(GameEntity owner)
    {
        this.owner = owner;
    }


    /*
     * Followings are implemented methods of "IWeapon"
     */
    public abstract void Fire(GameEntity target);


    /*
     * Followings are attributes
     */
    public const string TAG = "[Weapon]";
}
