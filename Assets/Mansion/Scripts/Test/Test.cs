using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

public class Test : MonoBehaviour {

	public UITexture texture;

	void Start(){
		Binding.SplashViewInitialize ();
	}

	public void OnButtonClicked(){
		Debug.Log("click");
		Binding2.SplashView ();
	}
}
