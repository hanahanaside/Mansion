using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		DateTime dt = DateTime.Now;
		string date = dt.ToString ("MM/dd HH:mm");
		Debug.Log (date);
	}
	
}
