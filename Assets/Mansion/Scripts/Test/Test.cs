using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour {

	public UILabel label;
	private long mCount;

	void Start(){

	}

	void Update(){
		mCount += 1000;
		label.text =   " " + mCount + " ";
	}
}
