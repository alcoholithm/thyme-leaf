using UnityEngine;
using System.Collections;

public class asdfff : MonoBehaviour
{
    public THouse ss;

    void OnClick()
    {
        ss.ChangeState(THouseState_Dying.Instance);
    }
}