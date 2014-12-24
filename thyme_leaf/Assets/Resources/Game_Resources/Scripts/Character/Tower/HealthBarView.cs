using UnityEngine;
using System.Collections;

public class HealthBarView : MonoBehaviour, IView
{
    private UISlider slider;

    // model
    Unit model;

    public Unit Model
    {
        get { return model; }
        set { model = value; }
    }

    //controller


    /*
     * followings are unity callback methods
     */ 
    void Awake()
    {
        this.slider = GetComponent<UISlider>();
    }

    /*
     * followings are member functions
     */ 
    void UpdateHealthBar()
    {
        float ratio = (float)model.HP / model.MaxHP;
        Color color = Color.Lerp(Color.red, Color.green, ratio);

        this.slider.value = ratio;
        this.slider.foregroundWidget.color = color;
    }

	public UISlider getSlider() { return slider; }

    /*
     * followings are overrided methods
     */ 
    public void ActionPerformed(string actionCommand)
    {
        throw new System.NotImplementedException();
    }

    public void UpdateUI()
    {
        UpdateHealthBar();
    }
}
