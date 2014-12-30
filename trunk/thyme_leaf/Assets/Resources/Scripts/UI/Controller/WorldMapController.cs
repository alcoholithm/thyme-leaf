using UnityEngine;
using System.Collections;

public class WorldMapController : MonoBehaviour {

    private WorldMapView view;
    private IUserAdministrator model;

    public WorldMapController(WorldMapView view, IUserAdministrator model)
    {
        this.view = view;
        this.model = model;
    }


}
