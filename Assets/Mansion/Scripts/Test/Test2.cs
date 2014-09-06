using UnityEngine;
using System.Collections;

public class Test2 : MonoBehaviour {

	private string url = "http://ad.graasb.com/shakky/money/ios/top/";
	WebViewObject webViewObject;
	void Start() {
		webViewObject = GetComponent<WebViewObject>();
		webViewObject.Init(); //初期化
		webViewObject.LoadURL(url); //ページの読み込み
		webViewObject.SetVisibility(true); //WebViewを表示する
		webViewObject.SetMargins(0, 1000, 0, 0); //下に100pxマージンを取る

	}

}
