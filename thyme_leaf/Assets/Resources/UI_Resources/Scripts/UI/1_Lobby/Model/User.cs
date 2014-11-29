using UnityEngine;
using System.Collections;

public class User
{
    //private bool loginStatus = false;
    private string name;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public User(string name)
    {
        this.name = name;
    }
}