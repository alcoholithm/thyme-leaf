using UnityEngine;
using System.Collections;

public interface ILoginModel
{
    IEnumerator Login(string id, string pwd);
}
