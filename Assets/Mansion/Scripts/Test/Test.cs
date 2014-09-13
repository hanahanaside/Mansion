using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour {

	public void OnButtonClicked(){
		Debug.Log("aaa");
		NendAdInterstitial.Instance.Show ();
	}
}
