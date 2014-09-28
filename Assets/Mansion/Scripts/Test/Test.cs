using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour {

	public WebViewObject w;
	private const string URL = "http://www.yahoo.co.jp/";


	void Start(){
		Debug.Log ("start");
		w.Init ();
		w.LoadURL (URL);
		w.SetMargins (0, 890, 0, 146); 
	}

}
