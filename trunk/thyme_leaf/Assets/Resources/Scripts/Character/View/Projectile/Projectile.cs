using UnityEngine;
using System.Collections;

public interface IProjectile : IAttackable, IMovable
{
    void Explode();
}

public abstract class Projectile : MonoBehaviour, IProjectile
{
    protected string _animName;

    protected NGUISpriteAnimation anim;
    protected UISprite sprite;
    protected GameEntity target;

    [SerializeField]
    private int attackDamage;
    //[SerializeField]
    //private int attackRange;

    [SerializeField]
    protected float movingSpeed = 0.7f;
    //private float rotationSpeed = 1f;


    /*
     * Followings are unity callback methods
     */
    protected virtual void Awake()
    {
        anim = GetComponent<NGUISpriteAnimation>();
        sprite = GetComponent<UISprite>();
    }

    protected virtual void Update()
    {
        MoveToTarget();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (!target.collider2D.Equals(other))
            return;

        movingSpeed = 0;

        Explode();
        Attack();
    }

    /*
     * Followings are member functions
     */
    protected virtual void MoveToTarget()
    {
        //if (flag)
        //    return;

        if (!target.gameObject.activeInHierarchy)
            Spawner.Instance.Free(this.gameObject);

        //Vector3 direction = targetPosition - transform.position;
        Vector3 direction = target.transform.position - transform.position;
        direction.z = 0;

        //Debug.Log(direction.magnitude);
        //if (direction.magnitude <= 0.05f)
        //{
        //    movingSpeed = 0;
        //    flag = true;

        //    if (target.gameObject.activeInHierarchy)
        //    {
        //        Message msg = target.GetComponent<Hero>().ObtainMessage(MessageTypes.MSG_DAMAGE, attackDamage);
        //        owner.DispatchMessage(msg);
        //    }


        //    fx.PlayOneShot(animName, new VoidFunction(() => Spawner.Instance.Free(this.gameObject)));
        //}

        transform.Translate(direction.normalized * movingSpeed * Time.deltaTime);
    }


    /*
    * Followings are implemented methods of "IProjectile"
    */
    public void Move(GameEntity target)
    {
        this.target = target;
        gameObject.SetActive(true);
        AudioManager.Instance.PlayClipWithState(this.gameObject, StateType.ATTACKING);
    }

    public void Attack()
    {
        GameEntity entity = target.GetComponent<GameEntity>();
        entity.DispatchMessage(entity.ObtainMessage(MessageTypes.MSG_NORMAL_DAMAGE, attackDamage));
    }

    public abstract void Explode();

    /*
     * Followings are Attributes
     */
    public const string TAG = "[Projectile]";


}
