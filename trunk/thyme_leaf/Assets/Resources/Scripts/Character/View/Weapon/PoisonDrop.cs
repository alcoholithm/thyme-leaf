using UnityEngine;
using System.Collections;

public class PoisonDrop : Projectile
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (!target.collider2D.Equals(other))
            return;

        movingSpeed = 0;

        GameObject zone = Spawner.Instance.GetPoisonCloud(EffectType.POISON_CLOUD);
        zone.transform.position = this.transform.position;

        this.animName = "PoisonGas_";
        fx.PlayOneShot(animName, new VoidFunction(() => Spawner.Instance.Free(this.gameObject)));
    }

    /*
     * Followings are Attributes
     */
    public new const string TAG = "[PoisonDrop]";
}
