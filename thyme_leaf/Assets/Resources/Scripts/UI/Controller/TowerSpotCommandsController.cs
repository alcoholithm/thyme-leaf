using UnityEngine;
using System.Collections;

public class TowerSpotCommandsController
{
    private BattleModel model; // 타워 스포너가 아니라 타워모델이 모델이 되어야한다.
    private TowerSpotCommands view;

    public TowerSpotCommandsController(TowerSpotCommands view, BattleModel model)
    {
        this.model = model;
        this.view = view;
    }

    /*
    * followings are public functions
    */

    // 지금 모델에 대한 정의가 완벽한 것이 아니기 때문에 그냥 컨트롤러에서 모든 것을 처리한다.
    // 나중에 완벽하게 잡히면 모델로 빼야됨.
    // 일단 지금 단계에서는 모델이 필요하지 않은 것 같다.
    // 타워의 관리자가 필요한 상황이 생기면 그때 정의하도록 하고 지금은 인스턴트코드로 갈음
    public void BuildTower()
    {
        // 모델에 대한 조작 // AddTower

        model.SelectedObject.tag = Tag.TagTower;

        // view에 대한 조작
        Agt_Type1 tower = Spawner.Instance.GetTower();
        tower.transform.parent = model.SelectedObject.transform;
        tower.transform.localScale = Vector3.one;
        tower.transform.position = model.SelectedObject.transform.position;

        tower.StateMachine.ChangeState(TowerState_Building.Instance);

        view.gameObject.SetActive(false);
    }

    public void Cancel()
    {
        model.SelectedObject = null;
        view.gameObject.SetActive(false);
    }

    public const string TAG = "[TowerSpotController]";
}
