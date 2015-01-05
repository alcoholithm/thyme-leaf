using UnityEngine;
using System.Collections;

public class WhapFX : MonoBehaviour
{
    public void Play()
    {
        Debug.Log("asdf");
        gameObject.GetComponent<TweenScale>().ResetToBeginning();
        gameObject.GetComponent<TweenScale>().PlayForward();
    }
}
