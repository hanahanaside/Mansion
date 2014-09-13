using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour {

	public GameObject effect;

	public void OnButtonClick(){
		Instantiate (effect);
	}
}
