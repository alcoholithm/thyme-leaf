using UnityEngine;
using System.Collections;

public class MoleView : MonoBehaviour, IView, IScoreObserver
{
    public GameObject _score;
    public GameObject[] _slots;

    private MoleController controller;
    private MoleModel model;

    void Awake()
    {
        this.model = MoleModel.Instance;
        this.controller = new MoleController(this);

        model.RegisterObserver(this as IScoreObserver);
        //model.RegisterObserver(this as IRenewalObserver);
    }

    public void Renew()
    {
        controller.Renew();
    }

    public void HitMole(Transform target)
    {
        if (target.FindChild("2_Background").gameObject.activeSelf)
        {
            target.FindChild("2_Background").gameObject.SetActive(false);
            controller.HitMole();
        }
    }

    void Exit()
    {
        Application.Quit();
    }

    public void Refresh<T>()
    {
        if (typeof(T) is IScoreObserver)
        {

        }
        //else if (typeof(T) is IRenewalObserver)
        //{

        //}
    }

    public void UpdateUI()
    {
    }

    public void ActionPerformed(string ActionCommand)
    {
        throw new System.NotImplementedException();
    }

    public void SetVisible(GameObject gameObject, bool active)
    {
        throw new System.NotImplementedException();
    }
}
