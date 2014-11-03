using UnityEngine;
using System.Collections;

public class MoleListener : MonoBehaviour
{
    void OnClick()
    {
        transform.parent.parent.GetComponent<MoleView>().HitMole(this.transform);
    }
}
