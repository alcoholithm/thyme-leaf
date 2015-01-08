using UnityEngine;
using System.Collections;

public class PoisonDrop : Projectile
{
    protected override void OnEnable()
    {
        base.OnEnable();

        sprite.spriteName = "PoisonDrop_0";
        sprite.MakePixelPerfect();

        this._animName = "PoisonDrop_";
        //anim.namePrefix = _animName;
        //anim.ResetToBeginning();
        anim.Play(this._animName);
    }

    /*
    * Followings are overrided methods of "Projectile"
    */
    public override void Explode()
    {
        this._animName = "PoisonExplosion_";
        anim.PlayOneShot(_animName, new VoidFunction(() =>
        {
            GameObject zone = Spawner.Instance.GetPoisonCloud(EffectType.POISON_CLOUD);
            zone.transform.position = this.transform.position;

            Spawner.Instance.Free(this.gameObject);
        }));

    }

    /*
     * Followings are Attributes
     */
    public new const string TAG = "[PoisonDrop]";


}
