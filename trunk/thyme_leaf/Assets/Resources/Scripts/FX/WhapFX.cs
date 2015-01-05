using UnityEngine;
using System.Collections;

public class WhapFX : MonoBehaviour
{
    public void Reset()
    {
        transform.localScale = Vector3.zero;
        //gameObject.GetComponent<TweenScale>().ResetToBeginning();
        gameObject.GetComponent<TweenScale>().enabled = false;
    }

    public void Play()
    {
        gameObject.GetComponent<TweenScale>().enabled = true;
        gameObject.GetComponent<TweenScale>().ResetToBeginning();
        gameObject.GetComponent<TweenScale>().PlayForward();
    }
}
