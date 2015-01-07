using UnityEngine;
using System.Collections;

public class FXBurn : View, IAttackable
{
    [SerializeField]
    private float _displayTime = 2f;

    [SerializeField]
    private int _attackDamage = 1;

    [SerializeField]
    private float _attackDelay = 0.7f;

    private UISprite sprite;
    private NGUISpriteAnimation anim;

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
        this.sprite.spriteName = "Fire_0";
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
        while(true)
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

        Debug.Log(Parent.Views.Count);

        gameObject.SetActive(true);
        StopCoroutine("HideDelayed");
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

    public const string TAG = "[FXBurn]";
}
