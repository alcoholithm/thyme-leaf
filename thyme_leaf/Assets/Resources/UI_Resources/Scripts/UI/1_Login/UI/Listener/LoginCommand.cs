using UnityEngine;
using System.Collections;

public class LoginCommand : MonoBehaviour, ICommand
{
    public void Execute()
    {
        Debug.Log("sdf");
    }
}
        //transform.parent.GetComponent<LoginView>().Login();
