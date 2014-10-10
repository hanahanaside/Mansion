using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

public class Test : MonoBehaviour {

	public WebViewObject webView;

	void Start(){
		webView.Init ((msg) => {

		});
		webView.LoadURL ("http://www.yahoo.co.jp/");
		webView.SetVisibility (true);
	}

	public void OnButtonClicked(){
		Debug.Log("click");
		Binding2.SplashView ();
	}
}
