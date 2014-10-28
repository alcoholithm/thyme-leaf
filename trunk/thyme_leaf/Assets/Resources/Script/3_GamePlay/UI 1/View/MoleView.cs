using UnityEngine;
using System.Collections;

public class MoleView : MonoBehaviour, IRenewalObserver, IScoreObserver { // 옵저버를 구상 클래스로 가야 할 듯 전략 객체로 만들고 바꿔 낌

    public GameObject _score;
    public GameObject[] _slots;

    private MoleController controller;
    private MoleModel model;

    void Awake()
    {
        model = MoleModel.Instance;
        model.RegisterObserver(this as IRenewalObserver);
        model.RegisterObserver(this as IScoreObserver);
        controller = new MoleController(this);
        paint();
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

    void paint()
    {
        Debug.Log(model.Score + "");

        _score.GetComponentInChildren<UILabel>().text = model.Score + "";
    }

    void repaint()
    {
        paint();
    }

    public void Refresh()
    {
        repaint();
    }

    internal void Exit()
    {
        Application.Quit();
    }

    public void RefreshRenewal()
    {
        foreach (GameObject go in _slots)
        {
            go.transform.FindChild("2_Background").gameObject.SetActive(false);
        }

        int idx = model.Moles[0];

        _slots[idx].transform.FindChild("2_Background").gameObject.SetActive(true);
    }

    public void RefreshScore()
    {
        Debug.Log(model.Score + "");

        _score.GetComponentInChildren<UILabel>().text = model.Score + "";
    }
}
