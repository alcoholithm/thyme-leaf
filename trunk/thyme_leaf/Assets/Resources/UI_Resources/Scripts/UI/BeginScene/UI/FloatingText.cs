using UnityEngine;
using System.Collections;

public class FloatingText : MonoBehaviour
{
    [SerializeField]
    float step = 0.4f;
    private UILabel temp;

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
        if (temp.alpha < 0.2f || temp.alpha > 0.9f)
        {
            step = -step;
        }

        temp.alpha += step * Time.deltaTime;
    }
}
