using UnityEngine;
using System.Collections;

public class WorldMapView : MonoBehaviour, IView
{
    private WorldMapController controller;
    private IUserAdministrator model;

    [SerializeField]
    GameObject _getToTheFight;

    [SerializeField]
    GameObject _backBtn;

    [SerializeField]
    GameObject _inventoryBtn;

    [SerializeField]
    GameObject _towerSettingBtn;

    [SerializeField]
    GameObject _storyBtn;

    

    public void ActionPerformed(string actionCommand)
    {
        if (actionCommand.Equals(_getToTheFight.name))
        {
            SceneManager.Instance.CurrentScene = SceneManager.BATTLE;
        }
        else if(actionCommand.Equals(_backBtn.name))
        {
            SceneManager.Instance.CurrentScene = SceneManager.LOBBY;
        }
        else if(actionCommand.Equals(_inventoryBtn.name))
        {
            SceneManager.Instance.CurrentScene = SceneManager.TOWER;
        }
        else if(actionCommand.Equals(_towerSettingBtn.name))
        {
            SceneManager.Instance.CurrentScene = SceneManager.AUTO;
        }
    }

    public void UpdateUI()
    {
        throw new System.NotImplementedException();
    }


    /*
     * 
    */
    public GameObject BackButton
    {
        get { return _backBtn; }
        set { _backBtn = value; }
    }

    public GameObject InventoryButton
    {
        get { return _inventoryBtn; }
        set { _inventoryBtn = value; }
    }

    public GameObject TowerSettingButton
    {
        get { return _towerSettingBtn; }
        set { _towerSettingBtn = value; }
    }

    public GameObject StoryButton
    {
        get { return _storyBtn; }
        set { _storyBtn = value; }
    }

    public GameObject GetToTheFight
    {
        get { return _getToTheFight; }
        set { _getToTheFight = value; }
    }
}
