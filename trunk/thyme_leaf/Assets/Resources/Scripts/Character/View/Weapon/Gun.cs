using UnityEngine;
using System.Collections;

public class Gun : Weapon, ILauncher
{
    /*
     * Followings are overrided methods of "IWeapon"
     */
    public void Fire(GameEntity target)
    {
        if (target == null)
            return;

        Projectile projectile = Spawner.Instance.GetProjectile(ProjectileType.BULLET);

        if (Network.peerType == NetworkPeerType.Disconnected) // Single mode
        {
            projectile.transform.position = transform.position;
            projectile.transform.localScale = Vector3.one;
            projectile.Move(target);
        }
        else if (projectile.gameObject.networkView.isMine)  // Multi mode
        {
            projectile.gameObject.GetComponent<SyncStateScript>().NetworkInitProjectile(Owner, target);
        }
    }

    /*
    * Followings are overrided methods of "View"
    */
    public override void PrepareUI()
    {
    }

    public override void UpdateUI()
    {
        Fire((Owner as AutomatTower).Model.CurrentTarget);
    }


    /*
     * Followings are Attributes
     */
    public new const string TAG = "[PoisonLauncher]";
}
