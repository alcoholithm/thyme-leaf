using UnityEngine;
using System.Collections;

public class HeroState_Listener : MonoBehaviour
{
    [RPC]
    void NetworkOnClick()
    {

    }

	/*
	public void StartPointSetting(GameObject start_point)
	{
		nodeStock = start_point;
		nodeInfor = nodeStock.GetComponent<scriptPathNode>();
		
		setPos (nodeInfor.getPos (PosParamOption.CURRENT));
		
		if(nodeInfor.Prev != null) SetMoveMode(MoveModeState.BACKWARD);
		else if(nodeInfor.Next != null) SetMoveMode(MoveModeState.FORWARD);
	}
	*/

    void OnClick()
    {
        Spawner.Instance.GetHero(AutomatType.FRANSIS_TYPE1);
    }

}
