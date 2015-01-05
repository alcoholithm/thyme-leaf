using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    private NGUISpriteAnimation fx;
    private UISprite sprite;
    private string animName;

    private GameEntity owner; // temp
    private GameEntity target;
    private Vector3 targetPosition;

    private bool flag = false;

    [SerializeField]
    private int attackDamage;
    [SerializeField]
    private int attackRange;

    [SerializeField]
    private float movingSpeed = 0.7f;
    //private float rotationSpeed = 1f;

    void Awake()
    {
        fx = GetComponent<NGUISpriteAnimation>();
        sprite = GetComponent<UISprite>();
    }

    void OnEnable()
    {
        //transform.LookAt(target.transform.position);

        //fx.Pause();

        movingSpeed = 0.7f;

        sprite.spriteName = "APT_Poison_0";
        sprite.MakePixelPerfect();

        this.animName = "APT_Poison_";
        fx.Play(this.animName);
    }

    void Update()
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!target.collider2D.Equals(other))
            return;

        movingSpeed = 0;

        this.animName = "APT_PoisonFX_";
        fx.PlayOneShot(animName, new VoidFunction(() => Spawner.Instance.Free(this.gameObject)));

        GameEntity entity = target.GetComponent<GameEntity>();
        Message msg = entity.ObtainMessage(MessageTypes.MSG_DAMAGE, attackDamage);

        entity.DispatchMessage(msg);
    }

    public void FireProcess(GameEntity owner, GameEntity target)
    {
        this.owner = owner;
        this.target = target;
        gameObject.SetActive(true);
    }

    /*
     * Attributes
     */
    public const string TAG = "[Projectile]";
}
