using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour {

	public WebViewObject webViewObject;

	void Start(){
			string url = "http://ad.graasb.com/shakky/money/ios/rectangle/";
		//	string url = "http://www.yahoo.co.jp/";
		webViewObject.Init (); //初期化
		webViewObject.LoadURL (url); //ページの読み込み
		webViewObject.SetMargins (0, 300, 0, 300); //下に100pxマージンを取る
		webViewObject.SetVisibility (true);

	}
}
