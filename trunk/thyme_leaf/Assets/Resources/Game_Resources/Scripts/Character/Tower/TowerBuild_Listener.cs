using UnityEngine;
using System.Collections;

public class TowerBuild_Listener : MonoBehaviour
{
    public Transform towers;
    public Transform towerSpot;

    void OnClick()
    {
        Tower tower = TowerSpawner.Instance.Allocate();
        tower.transform.parent = towers;
        tower.transform.localScale = Vector3.one;
        tower.transform.position = towerSpot.position;

        Message msg = tower.ObtainMessage(MessageTypes.MSG_BUILD_TOWER, new TowerBuildCommand(tower));
        tower.DispatchMessage(msg);
    }
}
