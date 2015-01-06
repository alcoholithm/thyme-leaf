using UnityEngine;
using System.Collections;

public class FXWhap : View
{
    private TweenScale tweenScale;

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
        this.tweenScale = GetComponent<TweenScale>();
    }

    public void Reset()
    {
        transform.localScale = Vector3.zero;
        gameObject.SetActive(false);
    }

    private void Play()
    {
        gameObject.SetActive(true);
        tweenScale.ResetToBeginning();
        tweenScale.PlayForward();
    }

    private void Paint()
    {
        Play();
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
        Paint();
    }
}
