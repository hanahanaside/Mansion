using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour {

	public UIGrid grid;
	public UICenterOnChild center;

	// Use this for initialization
	void Start () {
		float f = 2000000.5f;
		double d = 20000000000.5 + 0.1;
		Debug.Log ("ff = " + f);
		Debug.Log ("dd = " + d);
	}

	public void OnButtonClick(){
		List<Transform> childList = grid.GetChildList ();
		center.CenterOn (childList[2]);
	}
}
