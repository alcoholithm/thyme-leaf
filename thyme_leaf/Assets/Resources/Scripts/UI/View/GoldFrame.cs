using UnityEngine;
using System.Collections;

public class GoldFrameController
{
    private GoldFrame view;
    private UserAdministrator model;

    public GoldFrameController(GoldFrame view, UserAdministrator model)
    {
        this.view = view;
        this.model = model;
    }
}

public class GoldFrame : View
{
    // mvc
    private UserAdministrator model;


    //private GoldFrameController controller;

    // children
    private UILabel label;


    /*
     * Followings are unity callback methods
     */ 
    void Awake()
    {
        // mvc
        model = UserAdministrator.Instance;
        label = GetComponentInChildren<UILabel>();
    }


    /*
     * Followings are member functions
     */ 
    void UpdateGold()
    {
        label.text = model.CurrentUser.Gold + "";
    }

    /*
     * Followings are implemented methods of "View"
     */ 
    public override void UpdateUI()
    {
        UpdateGold();
    }


    /*
     * Followings are attributes
     */ 
    public UserAdministrator Model
    {
        get { return model; }
        set { model = value; }
    }
    public const string TAG = "[GoldFrame]";
}
