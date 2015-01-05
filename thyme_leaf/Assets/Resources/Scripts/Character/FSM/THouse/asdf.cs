using UnityEngine;
using System.Collections;

public class asdf : MonoBehaviour
{
    public THouse ss;

    void OnClick()
    {
        ss.ChangeState(THouseState_Idling.Instance);
    }
}