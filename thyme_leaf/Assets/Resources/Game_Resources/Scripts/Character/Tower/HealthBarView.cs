using UnityEngine;
using System.Collections;

public class HealthBarView : MonoBehaviour, IView, IObserver
{
    private UISlider slider;

    // model
    [SerializeField]
    Tower model;

    //controller


    /*
     * followings are unity callback methods
     */ 
    void Awake()
    {
        this.slider = GetComponent<UISlider>();
        //model.RegisterObserver(this, ObserverTypes.Health);
    }

    /*
     * followings are member functions
     */ 
    void UpdateHealthBar()
    {
        float ratio = model.CurrentHP / model.MaxHP;
        Color color = Color.Lerp(Color.red, Color.green, ratio);

        this.slider.value = ratio;
        this.slider.foregroundWidget.color = color;
    }


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

    public void Refresh(ObserverTypes field)
    {
        if (field == ObserverTypes.Health)
        {
            UpdateUI();
        }
    }
}
