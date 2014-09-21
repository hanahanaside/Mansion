using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour {

	void Start(){
		Debug.Log ("start");
	}

	void OnApplicationPause(bool pauseStatus){
		Debug.Log ("pause status " + pauseStatus);
	}

}
