using UnityEngine;
using System.Collections;

public class MHero : Unit
{
	private Weapon weapon;
	private Helper _helper;
	
	//IMovable
	private float movingSpeed;
	
	//extra... IGroupable
	private float angle;
	private string musterID;
	private bool muster_leader;
	private Vector3 node_offset;
	
	public MHero(Helper helper)
	{
		angle = 0;
		muster_leader = false;
		_helper = helper;
		musterID = "null";
	}
	
	/*
     * 
     */
	public float MovingSpeed
	{
		get { return movingSpeed; }
		set { movingSpeed = value; }
	}
	public float Angle
	{
		get { return angle; }
		set { angle = value; }
	}
	
	public string MusterID
	{
		get { return musterID; }
		set { musterID = value; }
	}
	
	public Vector3 Node_offset
	{
		get { return node_offset; }
		set { node_offset = value; }
	}
	public bool MusterLeader
	{
		get { return muster_leader; }
		set { muster_leader = value; }
	}
	
	public new const string TAG = "[MHero]";
}
