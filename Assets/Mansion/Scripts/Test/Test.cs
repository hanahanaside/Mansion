using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour {

	private float mTime = 1.0f;

	void Update(){
		mTime -= Time.deltaTime;
		Debug.Log ("time = " + mTime);
	}
}
