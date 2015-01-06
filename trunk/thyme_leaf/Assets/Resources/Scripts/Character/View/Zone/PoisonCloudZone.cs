using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoisonCloudZone : MonoBehaviour
{
    private NGUISpriteAnimation anim;
    private UISprite sprite;

    private string animName = "PoisonCloud_";

    [SerializeField]
    private float _activeTime = 7f;

    [SerializeField]
    private int _attackDamage = 5;

    private List<GameEntity> enemies;

    /*
     * Followings are unity callback methods.
     */ 
    void Awake()
    {
        this.sprite = GetComponent<UISprite>();
        this.anim = GetComponent<NGUISpriteAnimation>();
        this.enemies = new List<GameEntity>();
    }

    void OnEnable()
    {
        sprite.spriteName = "PoisonCloud_0";
        sprite.MakePixelPerfect();

        StartCoroutine("HideDelayed");
        StartCoroutine("AttackProcess");
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
    private IEnumerator HideDelayed()
    {
        yield return new WaitForSeconds(_activeTime);
        Spawner.Instance.PerfectFree(this.gameObject);
    }

    private IEnumerator AttackProcess()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.6f);
            enemies.ForEach(e => { if (!e.gameObject.activeInHierarchy) enemies.Remove(e); });
            enemies.ForEach(e => { e.DispatchMessage(e.ObtainMessage(MessageTypes.MSG_POISON_DAMAGE, _attackDamage)); });
        }
    }

    /*
     * Followings are Attributes
     */
    public const string TAG = "[PoisonCloudZone]";
}
