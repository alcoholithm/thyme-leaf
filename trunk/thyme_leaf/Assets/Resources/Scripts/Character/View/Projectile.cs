using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    protected NGUISpriteAnimation fx;
    private UISprite sprite;
    protected string animName;

    private GameEntity owner; // temp
    protected GameEntity target;
    //private Vector3 targetPosition;

    //private bool flag = false;

    [SerializeField]
    private int attackDamage;
    [SerializeField]
    private int attackRange;

    [SerializeField]
    protected float movingSpeed = 0.7f;
    //private float rotationSpeed = 1f;

    protected virtual void Awake()
    {
        fx = GetComponent<NGUISpriteAnimation>();
        sprite = GetComponent<UISprite>();
    }

    protected virtual void OnEnable()
    {
        //transform.LookAt(target.transform.position);

        //fx.Pause();

        movingSpeed = 0.7f;

        sprite.spriteName = "PoisonDrop_0";
        sprite.MakePixelPerfect();

        this.animName = "PoisonDrop_";
        fx.Play(this.animName);
    }

    protected virtual void Update()
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

        //transform.Rotate(new Vector3(0, 0, 1), Mathf.Atan2(direction.y, direction.x));
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction.normalized), rotationSpeed * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (!target.collider2D.Equals(other))
            return;

        movingSpeed = 0;

        this.animName = "PoisonGas_";
        fx.PlayOneShot(animName, new VoidFunction(() => Spawner.Instance.Free(this.gameObject)));

        GameEntity entity = target.GetComponent<GameEntity>();
        Message msg = entity.ObtainMessage(MessageTypes.MSG_NORMAL_DAMAGE, attackDamage);

        entity.DispatchMessage(msg);
    }

    public void FireProcess(GameEntity owner, GameEntity target)
    {
        this.owner = owner;
        this.target = target;
        gameObject.SetActive(true);
        AudioManager.Instance.PlayClipWithState(owner.gameObject, StateType.ATTACKING);
    }

    /*
     * Attributes
     */
    public const string TAG = "[Projectile]";
}
