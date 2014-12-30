using UnityEngine;
using System.Collections;

public class VictoryFrameController
{
    private VictoryFrame view;

    public VictoryFrameController(VictoryFrame view)
    {
        this.view = view;
    }

    public void Okay()
    {
        view.gameObject.SetActive(false);
        SceneManager.Instance.CurrentScene = SceneManager.WORLD_MAP;
    }
}

public class VictoryFrame : View, IActionListener
{
    // mvc
    private VictoryFrameController controller;

    [SerializeField]
    private GameObject _okButton;


    /*
     * Followings are unity callback methods.
     */ 
    void Awake()
    {
        controller = new VictoryFrameController(this);
    }

    /*
    * Followings are implemented methods of "IActionListener";
    */
    public void ActionPerformed(GameObject source)
    {
        if (source.Equals(_okButton))
        {
            controller.Okay();
        }
    }

    /*
     * Followings are attributes.
     */ 
    public GameObject OkButton
    {
        get { return _okButton; }
        set { _okButton = value; }
    }

    public const string TAG = "[VictoryFrame]";
}
