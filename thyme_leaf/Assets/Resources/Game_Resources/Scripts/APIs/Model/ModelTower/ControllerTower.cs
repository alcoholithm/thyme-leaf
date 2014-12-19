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

	//model...
	
	public void setMaxHp(int v) { _model.setMaxHp (v); }
	
	public void addHp(int v) { _model.addHp (v); }
	public void setHp(int v) { _model.setHp (v); }
	
	public void setName(string v) { _model.setName (v); }
	
	public void setType(UnitType option) { _model.setType (option); }
}
