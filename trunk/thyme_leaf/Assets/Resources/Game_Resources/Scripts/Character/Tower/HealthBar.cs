using UnityEngine;
using System.Collections;

public class HealthBar : View
{
    private UISlider slider;

    //------------------ MVC
    private Unit model;
    //------------------

    /*
     * followings are unity callback methods
     */
    void Awake()
    {
        Initialize();
    }

    /*
     * followings are member functions
     */
    void Initialize()
    {
        this.slider = GetComponent<UISlider>();
    }

    void UpdateHealthBar()
    {
        float ratio = (float)model.HP / model.MaxHP;
        Color color = Color.Lerp(Color.red, Color.green, ratio);

        this.slider.value = ratio;
        this.slider.foregroundWidget.color = color;
    }

    public UISlider getSlider() { return slider; }

    /*
     * followings are overrided methods of "View"
     */
    public override void UpdateUI()
    {
        UpdateHealthBar();
    }

    /*
     * followings are attributes
     */ 
    public Unit Model
    {
        get { return model; }
        set { model = value; }
    }

    public const string TAG = "[HealthBar]";
}
