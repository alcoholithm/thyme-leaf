using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlameThrower : Weapon, ILauncher
{
    private NGUISpriteAnimation anim;
    private UISprite sprite;

    private string animName = "FlameThrower_";

    [SerializeField]
    private float _activeTime = 1f;

    [SerializeField]
    private GameObject _flame;

    /*
     * Followings are unity callback methods.
     */ 
    void Awake()
    {
        base.Awake();
        Initialize();
    }

    void Start()
    {
        this.anim.namePrefix = animName;
        this.anim.framesPerSecond = (int)(anim.frames / _activeTime + 0.5f);

        //Debug.Log(this.anim.frames);
        //Debug.Log(this.anim.framesPerSecond);
    }

    void Update()
    {
        if (sprite.spriteName == "FlameThrower_04")
        {
            _flame.GetComponent<Flame>().Attack();
        }
    }

    /*
     * Followings are member functions
     */
    private void Initialize()
    {
        this.sprite = GetComponent<UISprite>();
        this.anim = GetComponent<NGUISpriteAnimation>();

        this.sprite.spriteName = "FlameThrower_0";
        this.sprite.MakePixelPerfect();

        this.anim.Pause();
    }

    /*
    * Followings are implemented methods of "ILauncher"
    */
    public void Fire(GameEntity target)
    {
        if (target == null)
            return;

        Vector3 dir = target.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Define.RadianToAngle();
        transform.localRotation = Quaternion.Euler(0, 0, angle);

        anim.ResetToBeginning();
        anim.PlayOneShot(animName);
    }

    /*
    * Followings are overrided methods of "View"
    */
    public override void PrepareUI()
    {
    }

    public override void UpdateUI()
    {
        Fire((Owner as AutomatTower).Model.CurrentTarget);
    }


    /*
     * Followings are Attributes
     */
    public const string TAG = "[FlameThrower]";
}
