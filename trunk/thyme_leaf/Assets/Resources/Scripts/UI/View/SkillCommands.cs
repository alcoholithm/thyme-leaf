using UnityEngine;
using System.Collections;


public class SkillCommandsController
{
    // mvc
    private SkillLauncher model;
    private SkillCommands view;

    public SkillCommandsController(SkillCommands view, SkillLauncher model)
    {
        this.view = view;
        this.model = model;
    }

    public void SpawnSkill()
    {
        if (UserAdministrator.Instance.CurrentUser.Gold >= 300)
        {
            view.SpawnSkillButton.GetComponent<UIButton>().normalSprite = "BtnAutomat";
            SkillLauncher.Instance.Prepare();
        }
    }
}

public class SkillCommands : View, IActionListener, IObserver
{
    [SerializeField]
    GameObject _spawnSkillButton;


    private SkillCommandsController controller;
    private SkillLauncher model;

    /*
     * followings are unity callback methods
     */
    void Awake()
    {
        this.model = SkillLauncher.Instance;
        this.controller = new SkillCommandsController(this, this.model);
    }

    void Start()
    {
        model.RegisterObserver(this, ObserverTypes.Skill);
    }

    /*
     * followings are implemented methods of interface
     */
    public void ActionPerformed(GameObject source)
    {
        if (source.Equals(_spawnSkillButton))
        {
            controller.SpawnSkill();
        }
    }

    /*
     * Followings are implemented methods of "IObserver"
     */
    public void Refresh(ObserverTypes field)
    {
        if (field == ObserverTypes.Skill)
        {
            _spawnSkillButton.GetComponent<UIButton>().normalSprite = "BtnTower";
            UserAdministrator.Instance.CurrentUser.Gold -= 300;
        }
    }


    /*
     * Followings are attributes
     */
    public GameObject SpawnSkillButton
    {
        get { return _spawnSkillButton; }
        set { _spawnSkillButton = value; }
    }
}
