using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour {

	void Start(){
		string dm = "10.5";
		decimal d = Convert.ToDecimal(dm);
		Debug.Log ("d = " + d);
	}
}
