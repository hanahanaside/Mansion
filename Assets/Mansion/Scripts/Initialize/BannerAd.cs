using UnityEngine;
using System.Collections;

public class BannerAd : MonoBehaviour {
	public WebViewObject webViewObject;

	void Start () {
		#if !UNITY_EDITOR
		DontDestroyOnLoad (gameObject);
		ShowBannerAd();
		#endif
	}

	private void ShowBannerAd(){
		#if UNITY_IPHONE
		webViewObject.Init (); //初期化
		webViewObject.LoadURL ("http://ad.graasb.com/shakky/money/ios/top/"); //ページの読み込み
		webViewObject.SetVisibility (true); //WebViewを表示する
		webViewObject.SetMargins (0, 890, 0, 146); //下に100pxマージンを取る
		#endif
	}
}
