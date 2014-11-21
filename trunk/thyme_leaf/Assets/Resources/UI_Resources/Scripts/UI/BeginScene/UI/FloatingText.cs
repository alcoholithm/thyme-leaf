using UnityEngine;
using System.Collections;

public class FloatingText : MonoBehaviour {

	private int flag;
	public UILabel temp;

	void Start () {
		flag = 0;
		temp = gameObject.GetComponent<UILabel>();
		Debug.Log(temp.alpha);
	}

	void Update () {
		// LabelUI 깜빡 깜빡 
		BlinkLabel();
	}

	void BlinkLabel() {
		if( flag ==0)
		{
			temp.alpha -= 0.4f * Time.deltaTime;
		}else {
			temp.alpha += 0.4f * Time.deltaTime;
		}
		
		if( temp.alpha < 0.2f)
		{
			flag = 1;
		}else if( temp.alpha > 0.9f) {
			flag = 0;
		}
	}
}
