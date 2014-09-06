using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour {

	private string url = "http://www.yahoo.co.jp/";

	void Start(){

	}

	public void Open(){
		Debug.Log ("open");
		EtceteraBinding.inlineWebViewShow( 50, 10, 260, 300 );
		EtceteraBinding.inlineWebViewSetUrl( "http://google.com" );

	}

	public void Close(){
		Debug.Log ("close");
		EtceteraBinding.inlineWebViewClose ();
	}

	public void Open2(){
		EtceteraBinding.inlineWebViewShow( 50, 300, 260, 300 );
		EtceteraBinding.inlineWebViewSetUrl(url );
	}
}
