using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : Manager<WaveManager> 
{
	public StageCenterSetStruct[] stage_set;  //stage num

	public bool WaveSystemTrigger = false;

	protected override void Awake()
	{
        base.Awake();

		//wave system setting value input...
		Define.stage_wave_sys_setting = stage_set;

		//file system... consider...
	}

	public void WaveSystemEnable()
	{
		if(!WaveSystemTrigger) return;

		Debug.Log ("wave system setting");
		Debug.Log (Define.THouse_list.Count);
		int stage_num = Define.current_stage_number >= 99999 ? 1 : Define.current_stage_number;
		Debug.Log (stage_num);
		for(int i=0,k=0;i<Define.THouse_list.Count;i++)
		{
			THouse t_data = Define.THouse_list[i].GetComponent<THouse>(); //only trovant center...
			if(t_data == null || t_data.gameObject.layer != (int)Layer.Trovant) continue;
			CenterWaveStruct data = Define.stage_wave_sys_setting[stage_num - 1].center_set[k];
			t_data.WaveSystemStart(data);
		}
	}

}

[System.Serializable]
public struct StageCenterSetStruct
{
	public CenterWaveStruct[] center_set;  //center num
}

[System.Serializable]
public struct CenterWaveStruct
{
	[HideInInspector]
	public WChat center_obj;  //or id? or name?
	public WaveSetStruct[] wave_setting_value_set; //center waver num
}

[System.Serializable]
public struct WaveSetStruct
{
	public float first_delay_time;
	public int unit_num;
	public TrovantType unit_type;
	public float unit_delay_time;
}
