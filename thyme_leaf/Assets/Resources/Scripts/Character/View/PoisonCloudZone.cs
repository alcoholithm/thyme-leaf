using UnityEngine;
using System.Collections;

public class PoisonCloudZone : MonoBehaviour
{
    private NGUISpriteAnimation anim;
    private UISprite sprite;
    private string animName;

    [SerializeField]
    private int attackDamage;
    [SerializeField]
    private int attackRange;

    void Awake()
    {
        this.sprite = GetComponent<UISprite>();
        this.anim = GetComponent<NGUISpriteAnimation>();
    }

    void OnEnable()
    {
        anim.Pause();

        sprite.spriteName = "PoisonCloud_0";
        sprite.MakePixelPerfect();

        this.animName = "PoisonDrop_";
        anim.Play(this.animName);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        //this.animName = "PoisonGas_";
        //anim.PlayOneShot(animName, new VoidFunction(() => Spawner.Instance.Free(this.gameObject)));

        //GameEntity entity = target.GetComponent<GameEntity>();
        //Message msg = entity.ObtainMessage(MessageTypes.MSG_DAMAGE, attackDamage);

        //entity.DispatchMessage(msg);
    }

    //public void FireProcess(GameEntity owner, GameEntity target)
    //{
    //    this.owner = owner;
    //    this.target = target;
    //    gameObject.SetActive(true);
    //    AudioManager.Instance.PlayClipWithState(owner.gameObject, StateType.ATTACKING);
    //}

    /*
     * Attributes
     */
    public const string TAG = "[PoisonCloudZone]";
}
