using UnityEngine;
using System.Collections;

public class WorldMapView : MonoBehaviour, IView
{
    [SerializeField]
    GameObject _getToTheFight;

    public void ActionPerformed(string actionCommand)
    {
        if (actionCommand.Equals(_getToTheFight.name))
        {
            SceneManager.Instance.CurrentScene = SceneManager.BATTLE;
        }
    }

    public void UpdateUI()
    {
        throw new System.NotImplementedException();
    }
}
