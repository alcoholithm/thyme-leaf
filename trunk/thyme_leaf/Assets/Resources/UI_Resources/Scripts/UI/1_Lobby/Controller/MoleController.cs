using UnityEngine;
using System.Collections;

public class MoleController {

    //protected static MoleController instance = new MoleController();
    //public static MoleController Instance { get { return instance; } }

    private MoleModel model;
    private MoleView view;

    public MoleController(MoleView view)
    {
        model = MoleModel.Instance;
        this.view = view;
    }

    public void HitMole()
    {
        model.incrementScore();
    }

    internal void Renew()
    {
        model.RenewMole();
    }
}
