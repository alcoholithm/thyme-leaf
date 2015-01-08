using UnityEngine;
using System.Collections;

public class GoToBackScene : MonoBehaviour
{
    void OnClick()
    {
        SceneManager.Instance.CurrentScene = SceneManager.WORLD_MAP;
    }
}
