using UnityEngine;
using System.Collections;

public class ExitListener : MonoBehaviour {

    void OnClick()
    {
        transform.parent.GetComponent<MoleView>().Exit();
    }
}
