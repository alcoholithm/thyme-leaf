using UnityEngine;
using System.Collections;

public interface IProjectile : IAttackable, IMovable
{
    void Explode();
}

public abstract class Projectile : MonoBehaviour, IProjectile
{
    protected NGUISpriteAnimation anim;
    protected UISprite sprite;

    [SerializeField]
    protected Transform target;
    protected Vector3 targetPosition;

    [SerializeField]
    private int attackDamage;
    //[SerializeField]
    //private int attackRange;

    [SerializeField]
    protected float initMovingSpeed;
    [SerializeField]
    protected float movingSpeed;
    //private float rotationSpeed = 1f;

    protected string _animName;
    protected bool isTouched;

    protected float deltaThreshold = 0.133f;

    /*
     * Followings are unity callback methods
     */
    protected virtual void Awake()
    {
        anim = GetComponent<NGUISpriteAnimation>();
        sprite = GetComponent<UISprite>();
    }

    protected virtual void OnEnable()
    {
        //transform.LookAt(target.transform.position);
        isTouched = false;
        movingSpeed = initMovingSpeed;
        anim.Pause();
    }

    protected virtual void Update()
    {
        MoveToTarget();
    }

    //protected virtual void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (!target.collider2D.Equals(other))
    //        return;

    //    Stop();
    //    Explode();
    //    Attack();
    //}

    /*
     * Followings are member functions
     */
    protected virtual void MoveToTarget()
    {
        if (isTouched)
            return;

        if (target == null)
            return;

        if (!target.gameObject.activeInHierarchy)
            Spawner.Instance.Free(this.gameObject);

        Vector3 direction = target.position - transform.position;
        //Vector3 direction = targetPosition - transform.position;

        if (direction.magnitude < deltaThreshold)
        {
            //Debug.Log(target.position + " " + transform.position);
            //Debug.Log(direction.magnitude);
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

    protected void Stop()
    {
        movingSpeed = 0;
        isTouched = true;
    }

    /*
    * Followings are implemented methods of "IProjectile"
    */
    public virtual void Move(Transform target)
    {
        this.target = target;
        gameObject.SetActive(true);
        AudioManager.Instance.PlayClipWithState(this.gameObject, StateType.ATTACKING);
    }

    public virtual void Move(Vector3 targetPos) { }

    public virtual void Attack()
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
