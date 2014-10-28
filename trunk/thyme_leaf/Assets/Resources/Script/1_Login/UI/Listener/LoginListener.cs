using UnityEngine;
using System.Collections;

public class LoginListener : MonoBehaviour {
    void OnClick()
    {
        transform.parent.GetComponent<LoginView>().Login();
    }
}
