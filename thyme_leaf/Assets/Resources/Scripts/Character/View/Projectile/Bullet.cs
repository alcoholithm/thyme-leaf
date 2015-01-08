using UnityEngine;
using System.Collections;

public class Bullet : Projectile
{
    protected override void OnEnable()
    {
        base.OnEnable();

        sprite.spriteName = "BoltBall_00";
        //sprite.MakePixelPerfect();

        this._animName = "BoltBall_";
        //anim.namePrefix = _animName;
        //anim.ResetToBeginning();
        anim.Play(this._animName);
    }

    /*
    * Followings are overrided methods of "Projectile"
    */
    public override void Explode()
    {
        this._animName = "BoltBall_";
        anim.PlayOneShot(_animName, new VoidFunction(() => Spawner.Instance.Free(this.gameObject)));
    }

    /*
     * Followings are Attributes
     */
    public new const string TAG = "[Bullet]";
}
