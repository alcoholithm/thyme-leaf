using UnityEngine;
using System.Collections;

public class FXBurn : View
{
    [SerializeField]
    private float _displayTime = 1f;

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
        Parent.Remove(this);
        gameObject.SetActive(false);
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
        gameObject.SetActive(true);
        StopCoroutine("HideDelayed");
        Paint();
        if (gameObject.activeInHierarchy)
            StartCoroutine("HideDelayed");
    }
}
