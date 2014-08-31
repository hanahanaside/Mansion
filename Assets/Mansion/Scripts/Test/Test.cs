using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour {

	public UIGrid grid;
	public UICenterOnChild center;

	// Use this for initialization
	void Start () {
		DateTime dt = DateTime.Now;
		string date = dt.ToString ("MM/dd HH:mm");
		Debug.Log (date);
	}

	public void OnButtonClick(){
		List<Transform> childList = grid.GetChildList ();
		center.CenterOn (childList[2]);
	}
}
