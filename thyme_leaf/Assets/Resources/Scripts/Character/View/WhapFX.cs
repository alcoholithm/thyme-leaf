using UnityEngine;
using System.Collections;

public class WhapFX : View
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

    private void Reset()
    {
        transform.localScale = Vector3.zero;
        gameObject.GetComponent<TweenScale>().enabled = false;
    }

    private void Play()
    {
        gameObject.GetComponent<TweenScale>().enabled = true;
        gameObject.GetComponent<TweenScale>().ResetToBeginning();
        gameObject.GetComponent<TweenScale>().PlayForward();
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
