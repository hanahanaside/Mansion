﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

public class Test : MonoBehaviour {

	private const string URL = "http://ad.graasb.com/shakky/money/tutorial/";


	public WebViewObject webView;

	void Start(){
		Debug.Log ("" + System.DateTime.Now.ToString("yyyy/mm/dd hh:mm:ss"));
	}

	public void OnButtonClicked(){
		Debug.Log("click");
		Binding2.SplashView ();
	}
}
