using UnityEngine;
using System.Collections;

public class FXPoisoning : View, IAttackable
{
    private UISprite sprite;
    private NGUISpriteAnimation anim;

    [SerializeField]
    private float _displayTime;

    [SerializeField]
    private int _attackDamage;

    [SerializeField]
    private float _attackDelay;


    /*
     * followings are unity callback methods
     */
    void Awake()
    {
        Initialize();
    }

    /*
    * followings are member functions
    */
    private void Initialize()
    {
        this.sprite = GetComponent<UISprite>();
        this.anim = GetComponent<NGUISpriteAnimation>();
    }

    private void Reset()
    {
        Initialize();
        this.sprite.spriteName = "Poisoning_0";
        this.anim.Pause();
        gameObject.SetActive(false);
    }

    private void Play()
    {
        anim.ResetToBeginning();
        anim.Play();
    }

    private void Paint()
    {
        Play();
    }

    private IEnumerator HideDelayed()
    {
        yield return new WaitForSeconds(_displayTime);
        StopAllCoroutines();

        gameObject.SetActive(false);
    }

    private IEnumerator AttackProcess()
    {
        while (true)
        {
            yield return new WaitForSeconds(_attackDelay);
            Attack();
        }
    }

    /*
     * followings are overrided methods of "View"
     */
    public override void PrepareUI()
    {
        Reset();
    }

    public override void UpdateUI()
    {
        Parent.Remove(this);

        gameObject.SetActive(true);
        StopAllCoroutines();
        Paint();
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine("HideDelayed");
            StartCoroutine("AttackProcess");
        }
    }

    /*
     * followings are overrided methods of "IAttackable"
     */
    public void Attack()
    {
        (Parent as GameEntity).DispatchMessage((Parent as GameEntity).ObtainMessage(MessageTypes.MSG_NORMAL_DAMAGE, _attackDamage));
    }

    public const string TAG = "[FXPoisoning]";
}