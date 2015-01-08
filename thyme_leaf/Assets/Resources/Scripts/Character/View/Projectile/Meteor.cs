using UnityEngine;
using System.Collections;

public class Meteor : Projectile
{
    protected override void OnEnable()
    {
        base.OnEnable();

        deltaThreshold = 0.01f;

        sprite.spriteName = "FireBall_0001";
        sprite.MakePixelPerfect();

        this._animName = "FireBall_";
        //anim.namePrefix = _animName;
        //anim.ResetToBeginning();
        anim.framesPerSecond = 30;
        anim.Play(this._animName);
    }

    /*
    * Followings are member functions
    */
    protected override void MoveToTarget()
    {
        if (isTouched)
            return;

        //if (!target.gameObject.activeInHierarchy)
        //    Spawner.Instance.Free(this.gameObject);

        //Vector3 direction = target.position - transform.position;
        Vector3 direction = targetPosition - transform.position;

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
        }

        //if (!target.gameObject.activeInHierarchy)
        //    Spawner.Instance.Free(this.gameObject);

        //Vector3 direction = target.transform.position - transform.position;
        //direction.z = 0;
        //transform.Translate(direction.normalized * movingSpeed * Time.deltaTime);
    }

    /*
    * Followings are overrided methods of "Projectile"
    */
    public override void Explode()
    {
        this._animName = "FireExplosion_";
        anim.framesPerSecond = 10;
        anim.PlayOneShot(_animName, new VoidFunction(() =>
        {
            GameObject zone = Spawner.Instance.GetPoisonCloud(EffectType.POISON_CLOUD);
            zone.transform.position = this.transform.position;

            Spawner.Instance.Free(this.gameObject);
        }));

    }

    public override void Move(Transform target)
    {
        this.target = target;
        this.targetPosition = new Vector3(target.position.x, target.position.y, target.position.z);

        gameObject.SetActive(true);
        AudioManager.Instance.PlayClipWithState(this.gameObject, StateType.ATTACKING);
    }

    public override void Move(Vector3 targetPos)
    {
        this.targetPosition = targetPos;
        gameObject.SetActive(true);
        AudioManager.Instance.PlayClipWithState(this.gameObject, StateType.ATTACKING);
    }

    public override void Attack() {}

    /*
     * Followings are Attributes
     */
    public new const string TAG = "[Meteor]";


}
