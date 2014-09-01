using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour {

	public UIGrid grid;
	public UICenterOnChild center;

	// Use this for initialization
	void Start () {
		Debug.Log ("start");
	}

	void OnApplicationPause(bool state){
		Debug.Log ("pause");
	}

	public void OnButtonClick(){
		List<Transform> childList = grid.GetChildList ();
		center.CenterOn (childList[2]);
	}
}
