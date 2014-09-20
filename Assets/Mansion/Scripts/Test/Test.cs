using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour {

	void  OnTriggerEnter (Collider collider) {
		Debug.Log ("enter");
	}

	void OnTriggerExit(Collider collider){
		Debug.Log ("exit");
	}
}
