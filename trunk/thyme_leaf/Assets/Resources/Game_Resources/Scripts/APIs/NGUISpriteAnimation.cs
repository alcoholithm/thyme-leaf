using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NGUISpriteAnimation : MonoBehaviour
{
    [SerializeField]
    int _framerates = 30;
    [SerializeField]
    string _namePrefix = "";
    [SerializeField]
    bool _loop = true;
    [SerializeField]
    bool _pixelSnap = true;

    private UISprite currentSprite;
    private float delta = 0f;
    private int currentIndex = 0;
    private bool isPlaying = true;
    private List<string> spriteNames = new List<string>();



    /*
     * followings are public member functions.
     */ 
    public void Play()
    {
        //StartCoroutine();
    }

    public void Pause()
    {

    }

    public void ResetToBeginning()
    {

    }

    public void RebuildSpriteList()
    {
    }

    /*
    * followings are public member functions.
    */
    void Awake()
    {
        this.currentSprite = GetComponent<UISprite>();
    }

    void Start()
    {
        RebuildSpriteList();
    }

    void Update()
    {

    }
}
