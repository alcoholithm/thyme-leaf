using UnityEngine;
using System.Collections;

public class Bullet : Projectile
{
    void OnEnable()
    {
        //transform.LookAt(target.transform.position);
        movingSpeed = 0.7f;

        anim.Pause();

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
        this._animName = "PoisonGas_";
        anim.PlayOneShot(_animName, new VoidFunction(() => Spawner.Instance.Free(this.gameObject)));
    }

    /*
     * Followings are Attributes
     */
    public new const string TAG = "[Bullet]";
}
