using UnityEngine;
using System.Collections;

public class PoisonLauncher : Weapon, ILauncher
{
    /*
     * Followings are overrided methods of "IWeapon"
     */
    public void Fire(Transform target)
    {
        if (target == null)
            return;

        Projectile projectile = Spawner.Instance.GetProjectile(ProjectileType.POISON, transform.position);

        if (projectile == null)
            return;


        if (Network.peerType == NetworkPeerType.Disconnected) // Single mode
        {
            projectile.transform.position = transform.position;
            projectile.transform.localScale = Vector3.one;
            projectile.Move(target);
        }
        else if (projectile.gameObject.networkView.isMine)  // Multi mode
        {
            projectile.gameObject.GetComponent<SyncStateScript>().NetworkInitProjectile(Parent as GameEntity, target.GetComponent<GameEntity>());
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
        Fire((Parent as AutomatTower).Model.CurrentTarget.transform);
    }


    /*
     * Followings are Attributes
     */
    public new const string TAG = "[PoisonLauncher]";
}
