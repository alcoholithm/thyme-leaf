using UnityEngine;
using System.Collections;

public class MHero : Unit
{
	private Weapon weapon;
	private Helper _helper;

	public Helper Helper {
		get {
			return _helper;
		}
		set {
			_helper = value;
		}
	}

	//IMovable
	[SerializeField]
	private float movingSpeed;
	
	//extra... IGroupable
	private float angle;

	[SerializeField]
	private string musterID;

	private bool muster_leader;
	private OffsetStruct offset_struct;

	[SerializeField]
	private float attack_delay;
	private float attack_range;
	[SerializeField]
	private float attack_damage;
	[SerializeField]
	private string state_name;

	protected override void Awake()
	{
        base.Awake();

		angle = 0;
		muster_leader = false;
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

	public bool MusterLeader
	{
		get { return muster_leader; }
		set { muster_leader = value; }
	}

	public string StateName
	{
		get { return state_name; }
		set { state_name = value; }
	}

	public float AttackDelay
	{
		get { return attack_delay; }
		set { attack_delay = value; }
	}

	public float AttackDamage
	{
		get { return attack_damage; }
		set { attack_damage = value; }
	}

	public float AttackRange
	{
		get { return attack_range; }
		set { attack_range = value; }
	}

	public OffsetStruct NodeOffsetStruct
	{
		get { return offset_struct; }
		set { offset_struct = value; }
	}

	public new const string TAG = "[MHero]";
}
