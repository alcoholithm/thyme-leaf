using UnityEngine;
using System.Collections;

public class User
{
    private bool loginStatus = false;
    private string name;

    public User(string name)
    {
        this.name = name;
    }
}