using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    private NGUISpriteAnimation fx;
    private UISprite sprite;
    string animName = "Weapon_PoisonGas_";

    private GameEntity owner; // temp
    private GameEntity target;

    //attack... IAttackable
    private int attackDamage = 10;
    private int attackRange;

    // IMovable
    private float movingSpeed = 0.7f;
    //private float rotationSpeed = 1f;

    void Awake()
    {
        gameObject.SetActive(false);
        fx = GetComponent<NGUISpriteAnimation>();
        sprite = GetComponent<UISprite>();

    }

    void OnEnable()
    {
        //transform.LookAt(target.transform.position);

        fx.Pause();

        movingSpeed = 0.7f;
        sprite.spriteName = "Comma_Attacking_Downwards_0";
        sprite.MakePixelPerfect();
    }

    void Update()
    {
        Vector3 direction = target.transform.position - transform.position;

        direction.z = 0;

        transform.Translate(direction.normalized * movingSpeed * Time.deltaTime);

        //transform.Rotate(new Vector3(0, 0, 1), Mathf.Atan2(direction.y, direction.x));
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction.normalized), rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!target.collider2D.Equals(other))
            return;

        movingSpeed = 0;

        //fx.PlayOneShot(animName, new VoidFunction(() => ProjectileSpawner.Instance.Free(this.gameObject)));
        fx.PlayOneShot(animName, new VoidFunction(() => gameObject.SetActive(false)));

        Message msg = owner.ObtainMessage(MessageTypes.MSG_DAMAGE, attackDamage);
        owner.DispatchMessage(msg);
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
