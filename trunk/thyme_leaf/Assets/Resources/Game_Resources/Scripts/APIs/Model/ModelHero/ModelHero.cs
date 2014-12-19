using UnityEngine;
using System.Collections;

public class MHero : Unit
{
    private Weapon weapon;
    private Helper _helper;

    //IMovable
    private float movingSpeed;

    //extra... IGroupable
    private float radian;
    private string musterID;
    private Vector2 node_offset;

    public MHero(Helper helper)
    {
        radian = 0;
        _helper = helper;
    }

    /*
     * 
     */
    public float MovingSpeed
    {
        get { return movingSpeed; }
        set { movingSpeed = value; }
    }
    public float Radian
    {
        get { return radian * Define.RadianToAngle(); }
        set { radian = Define.AngleToRadian() * value; }
    }

    public string MusterID
    {
        get { return musterID; }
        set { musterID = value; }
    }

    public Vector2 Node_offset
    {
        get { return node_offset; }
        set { node_offset = value; }
    }

    public new const string TAG = "[MHero]";
}
