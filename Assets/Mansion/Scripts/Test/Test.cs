using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

public class Test : MonoBehaviour {

	public UITexture texture;

	void Start(){
		Debug.Log("aa");
		WWWClient wwwClient = new WWWClient (this,"https://dl.dropboxusercontent.com/u/32529846/logo.png");
		wwwClient.OnSuccess = (WWW response) => {
			texture.mainTexture  = response.texture;

		};
		wwwClient.Request ();
	}

	public void OnButtonClicked(){
		Debug.Log("click");
	}
}
