using UnityEngine;
using System.Collections;

public class FloatingText : MonoBehaviour
{
    [SerializeField]
    float step = 0.7f;
    
    private UILabel temp;
    private int plusFlag = 0;

    void Awake()
    {
        temp = gameObject.GetComponent<UILabel>();
    }

    void Update()
    {
        BlinkLabel();
    }

    void BlinkLabel()
    {
        if (temp.alpha < 0)
            plusFlag = 0;
        else if (temp.alpha > 1)
            plusFlag = 1;

        if (plusFlag == 0)
            temp.alpha += step * Time.deltaTime;
        else if (plusFlag == 1)
            temp.alpha -= step * Time.deltaTime;

    }
}
