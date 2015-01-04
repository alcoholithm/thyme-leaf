using UnityEngine;
using System.Collections;

public class HealthBar : View
{
    private UISlider slider;

    [SerializeField]
    private float displayTime;

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
    private void Initialize()
    {
        this.slider = GetComponent<UISlider>();
    }

    private void UpdateHealthBar()
    {
        float ratio = (float)model.HP / model.MaxHP;
        Color color = Color.Lerp(Color.red, Color.green, ratio);

        this.slider.value = ratio;
        this.slider.foregroundWidget.color = color;
    }

    private void Paint()
    {
        UpdateHealthBar();
    }

    private IEnumerator HideDelayed()
    {
        yield return new WaitForSeconds(displayTime);
        gameObject.SetActive(false);
    }

    public UISlider getSlider() { return slider; }

    /*
     * followings are overrided methods of "View"
     */
    public override void UpdateUI()
    {
		Debug.Log ("health bar update ui");
        gameObject.SetActive(true);
        StopCoroutine("HideDelayed");
        Paint();
        if (gameObject.activeInHierarchy)
            StartCoroutine("HideDelayed");
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
