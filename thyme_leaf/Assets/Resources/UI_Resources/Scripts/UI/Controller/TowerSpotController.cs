using UnityEngine;
using System.Collections;

public class TowerSpotController
{
    private TowerSpawner model;
    private TowerSpotView view;

    public TowerSpotController(TowerSpotView view, TowerSpawner model)
    {
        this.model = model;
        this.view = view;
    }

    /*
    * followings are public functions
    */

    // 지금 모델에 대한 정의가 완벽한 것이 아니기 때문에 그냥 컨트롤러에서 모든 것을 처리한다.
    // 나중에 완벽하게 잡히면 모델로 빼야됨.
    public void BuildTower()
    {
        Tower tower = TowerSpawner.Instance.Allocate();
        tower.transform.parent = view.transform;
        tower.transform.localScale = Vector3.one;
        tower.transform.position = view.transform.position;
        tower.StateMachine.ChangeState(TowerState_Building.Instance);

        //Message msg = tower.ObtainMessage(MessageTypes.MSG_BUILD_TOWER, new TowerBuildCommand(tower));
        //tower.DispatchMessage(msg);
    }

    public const string TAG = "[TowerSpotController]";
}
