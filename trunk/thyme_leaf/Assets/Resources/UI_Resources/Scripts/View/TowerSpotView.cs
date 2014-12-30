using UnityEngine;
using System.Collections;

public class TowerSpotView : View, IActionListener
{
    [SerializeField]
    GameObject _upgradeButton;
    [SerializeField]
    GameObject _sellButton;
    [SerializeField]
    GameObject _cancelButton;
    [SerializeField]
    GameObject _parent;

    private TowerSpotController controller;
    private TowerSpawner model;

    /*
     * followings are unity callback methods
     */
    void Awake()
    {
        this.model = TowerSpawner.Instance;
        this.controller = new TowerSpotController(this, this.model);
    }

    /*
     * followings are implemented methods of interface
     */
    public void ActionPerformed(string actionCommand)
    {
        if (actionCommand.Equals(_upgradeButton.name))
        {
            //controller.BuildTower(_parent);
        }
    }
}
