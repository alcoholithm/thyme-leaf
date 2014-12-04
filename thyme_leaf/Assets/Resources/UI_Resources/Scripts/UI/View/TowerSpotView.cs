using UnityEngine;
using System.Collections;

public class TowerSpotView : MonoBehaviour, IView
{
    [SerializeField]
    GameObject _buildButton;
    [SerializeField]
    GameObject _sellButton;
    [SerializeField]
    GameObject _cancelButton;

    private TowerSpotController controller;
    private TowerSpawner model;

    /*
     * followings are unity callback methods
     */
    void Awake()
    {
        this.model = TowerSpawner.Instance;
        this.controller = new TowerSpotController(this, TowerSpawner.Instance);
        //this.model.RegisterObserver(this);
    }

    /*
     * followings are implemented methods of interface
     */
    public void ActionPerformed(string actionCommand)
    {
        if (actionCommand.Equals(_buildButton.name))
        {
            controller.BuildTower();
        }
    }

    public void UpdateUI()
    {
        throw new System.NotImplementedException();
    }
}
