using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour {

	public WebViewObject webViewObject;



	void Start(){
		string url = "http://www.yahoo.co.jp/";
		//	string url = "http://ad.graasb.com/shakky/money/android/top/";
		webViewObject.Init (); //初期化
		webViewObject.LoadURL (url); //ページの読み込み
		webViewObject.SetMargins (0, 0, 0, 0); //下に100pxマージンを取る
		webViewObject.SetVisibility (true);
	}
}
