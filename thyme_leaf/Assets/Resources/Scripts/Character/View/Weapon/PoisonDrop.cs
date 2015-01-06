using UnityEngine;
using System.Collections;

public class PoisonDrop : Projectile
{
    [SerializeField]
    private GameObject _poisonCloudeZone;

    bool flag = false;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (!target.collider2D.Equals(other))
            return;

        //if (flag)
        //    return;

        flag = true;

        movingSpeed = 0;

        GameObject zone = Instantiate(_poisonCloudeZone) as GameObject;
        zone.transform.parent = GameObject.Find("AutomatBuildings").transform;
        zone.transform.position = this.transform.position;

        this.animName = "PoisonGas_";
        fx.PlayOneShot(animName, new VoidFunction(() => Spawner.Instance.Free(this.gameObject)));

        //GameEntity entity = target.GetComponent<GameEntity>();
        //Message msg = entity.ObtainMessage(MessageTypes.MSG_NORMAL_DAMAGE, attackDamage);
        //entity.DispatchMessage(msg);
    }

    /*
     * Followings are Attributes
     */
    public new const string TAG = "[PoisonDrop]";
}
