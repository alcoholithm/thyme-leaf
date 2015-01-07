using UnityEngine;
using System.Collections;

public class SkillLauncher : Weapon, ILauncher
{
    private bool isFired;

    /*
     * Followings are unity callback methods
     */ 

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(Input.mousePosition);
            Debug.Log("M " + Camera.main.ScreenToWorldPoint(Input.mousePosition));

            this.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            Fire(this.transform);
        }
    }

    /*
     * Followings are overrided methods of "IWeapon"
     */
    public void Fire(Transform target)
    {
        if (target == null)
            return;

        Projectile projectile = Spawner.Instance.GetProjectile(ProjectileType.METEO);

        if (Network.peerType == NetworkPeerType.Disconnected) // Single mode
        {
            projectile.transform.position = transform.position;
            projectile.transform.localPosition += new Vector3(0, 500, 0);
            projectile.transform.localScale = Vector3.one;

            Debug.Log("D " + target.transform.position);
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
