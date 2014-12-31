using UnityEngine;
using System.Collections;

public class HeroState_Listener : MonoBehaviour
{
    [RPC]
    void NetworkOnClick()
    {

    }

    void OnClick()
    {
        Spawner.Instance.GetHero(AutomatType.FRANSIS_TYPE1);
    }

}
