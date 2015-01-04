using UnityEngine;
using System.Collections;

public class GGG : MonoBehaviour
{
    private NGUISpriteAnimation anim;

    void Awake()
    {
        this.anim = GetComponent<NGUISpriteAnimation>();
        Debug.Log(this.anim);
    }

}
