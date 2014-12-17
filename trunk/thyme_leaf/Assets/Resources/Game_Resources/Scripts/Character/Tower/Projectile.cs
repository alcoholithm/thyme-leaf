using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    UI2DSpriteAnimation _fx;

    private GameEntity owner; // temp
    private GameEntity target;
    private int power = 10;
    private float movingSpeed = 0.7f;
    private float rotationSpeed = 1f;

    void Awake()
    {
        gameObject.SetActive(false);
    }

    void Start()
    {
        // Z축을 움직여야 하는데 잘 안된다.
        //transform.LookAt(target.transform.position);
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

        Debug.Log("Fire");

        //Message msg = target.ObtainMessage(MessageTypes.MSG_DAMAGE, power);
        //target.DispatchMessage(msg);

        Message msg = owner.ObtainMessage(MessageTypes.MSG_DAMAGE, power);
        owner.DispatchMessage(msg);

        //_fx.

        ProjectileSpawner.Instance.Free(this.gameObject);
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
