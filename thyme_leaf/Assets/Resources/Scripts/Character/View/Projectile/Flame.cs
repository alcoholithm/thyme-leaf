using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Flame : MonoBehaviour, IAttackable// Projectile
{
    [SerializeField]
    private int _attackDamage = 1;

    private List<GameEntity> enemies;

    /*
     * Followings are unity callback methods.
     */
    void Awake()
    {
        //base.Awake();
        Initialize();
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
        this.enemies = new List<GameEntity>();
    }

    /*
    * Followings are overrided methods of "IAttackable"
    */
    public void Attack()
    {
        enemies.ForEach(e => { if (e == null || !e.gameObject.activeInHierarchy) enemies.Remove(e); });
        enemies.ForEach(e => { e.DispatchMessage(e.ObtainMessage(MessageTypes.MSG_BURN_DAMAGE, _attackDamage)); });
    }

    /*
     * Followings are Attributes
     */
    public const string TAG = "[FlameThrower]";
}
