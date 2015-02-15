using UnityEngine;
using System.Collections;

public class Missile : Projectile
{
    protected override void OnEnable()
    {
        base.OnEnable();

		sprite.spriteName = "Missile_0";
		sprite.MakePixelPerfect ();

        this._animName = "Missile_";
        //anim.namePrefix = _animName;
        //anim.ResetToBeginning();
        anim.Play(this._animName);
    }

    protected override void MoveToTarget()
    {
        //------------ 예외처리 시작
        if (isTouched)
            return;

        if (target == null)
            return;

        if (!target.gameObject.activeInHierarchy)
            Spawner.Instance.Free(this.gameObject);
        //------------ 예외처리 끝


        // 목표물까지의 이동 및 회전
        Vector3 direction = target.position - transform.position;

        if (direction.magnitude < deltaThreshold)
        {
            Stop();
            Explode();
            Attack();
        }
        else
        {
            direction.z = 0;
            transform.Translate(direction.normalized * movingSpeed * Time.deltaTime);

            // 회전 구현 부탁드림
        }
    }

    /*
    * Followings are overrided methods of "Projectile"
    */
    public override void Explode()
    {
        this._animName = "MissileFX_";
        anim.PlayOneShot(_animName, new VoidFunction(() => Spawner.Instance.Free(this.gameObject)));
    }

    /*
     * Followings are Attributes
     */
    public new const string TAG = "[Missile]";
}
