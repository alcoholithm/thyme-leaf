using UnityEngine;
using System.Collections;

public class ControllerTower
{
	ModelTower _model;

	public ControllerTower(ModelTower model)
	{
		_model = model;
	}

	public void setAttackDelay(int v) { _model.setAttackDelay (v); }
	
	public void setAttackDamage(int v) { _model.setAttackDamage (v); }
	
	public void setAttackRange(int v) { _model.setAttackRange (v); }
}
