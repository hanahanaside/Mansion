using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

public class Test : MonoBehaviour {

	void Start(){
		NendAdInterstitial.Instance.Load ("c2539644080634070e6910163eb6f5176f509342","219435");
	}

	public void OnNendClicked(){
		Debug.Log("nend");
		NendAdInterstitial.Instance.Show ();
	}

	public void OnTwitterClicked(){
		Debug.Log ("twitter");
		TwitterBinding.showTweetComposer ("test");
	}
}
