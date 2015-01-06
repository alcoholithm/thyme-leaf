using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlameThrower : MonoBehaviour
{
    private NGUISpriteAnimation anim;
    private UISprite sprite;

    private string animName = "FlameThrower_";

    [SerializeField]
    private float _activeTime = 1f;

    [SerializeField]
    private int _attackDamage = 1;

    private List<GameEntity> enemies;

    /*
     * Followings are unity callback methods.
     */ 
    void Awake()
    {
        Initialize();
    }

    void Start()
    {
        this.anim.namePrefix = animName;
        this.anim.framesPerSecond = (int)(anim.frames / _activeTime + 0.5f);

        //Debug.Log(this.anim.frames);
        //Debug.Log(this.anim.framesPerSecond);
    }

    public void Repaint()
    {
        if (!transform.parent.GetComponent<AutomatTower>().Model.CurrentTarget)
            return;

        GameObject target = transform.parent.GetComponent<AutomatTower>().Model.CurrentTarget.gameObject;

        Vector3 dir = target.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Define.RadianToAngle();
        transform.localRotation = Quaternion.Euler(0, 0, angle);

        anim.ResetToBeginning();
        //anim.PlayOneShot(animName, new VoidFunction(() => this.gameObject.SetActive(false)));
        anim.PlayOneShot(animName);
    }

    void Update()
    {
        if (sprite.spriteName == "FlameThrower_04")
        {
            Attack();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tag.TagProjectile)
            || other.CompareTag(Tag.TagArcherAutomart())
            || other.CompareTag(Tag.TagBarrierAutomart())
            || other.CompareTag(Tag.TagHealerAutomart())
            || other.CompareTag(Tag.TagSupporterAutomart())
            || other.CompareTag(Tag.TagWarriorAutomart())
            || other.CompareTag(Tag.TagCommandCenter)
            || other.CompareTag(Tag.TagTower)
        )
            return;

        enemies.Add(other.GetComponent<GameEntity>());
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(Tag.TagProjectile)
            || other.CompareTag(Tag.TagArcherAutomart())
            || other.CompareTag(Tag.TagBarrierAutomart())
            || other.CompareTag(Tag.TagHealerAutomart())
            || other.CompareTag(Tag.TagSupporterAutomart())
            || other.CompareTag(Tag.TagWarriorAutomart())
            || other.CompareTag(Tag.TagCommandCenter)
            || other.CompareTag(Tag.TagTower)
        )
            return;

        enemies.Remove(other.GetComponent<GameEntity>());
    }

    /*
     * Followings are member functions
     */
    private void Initialize()
    {
        this.sprite = GetComponent<UISprite>();
        this.anim = GetComponent<NGUISpriteAnimation>();
        this.enemies = new List<GameEntity>();

        this.sprite.spriteName = "FlameThrower_0";
        this.sprite.MakePixelPerfect();

        this.anim.Pause();
    }

    private void Attack()
    {
        enemies.ForEach(e => { if (!e.gameObject.activeInHierarchy) enemies.Remove(e); });
        enemies.ForEach(e => { e.DispatchMessage(e.ObtainMessage(MessageTypes.MSG_BURN_DAMAGE, _attackDamage)); });
    }

    /*
     * Followings are Attributes
     */
    public const string TAG = "[FlameThrower]";
}
