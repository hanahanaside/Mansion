using UnityEngine;
using System.Collections;

public class BannerAd : MonoBehaviour {
	public WebViewObject webViewObject;
	private static BannerAd sInstance;

	void Start () {
		sInstance = this;
		#if !UNITY_EDITOR
		DontDestroyOnLoad (gameObject);
		webViewObject.Init (); //初期化
		webViewObject.LoadURL ("http://ad.graasb.com/shakky/money/ios/top/"); //ページの読み込み
		webViewObject.SetMargins (0, 890, 0, 146); //下に100pxマージンを取る
		#endif
	}

	public static BannerAd Instance {
		get {
			return sInstance;
		}
	}

	public void ShowBannerAd () {
		#if !UNITY_EDITOR
		webViewObject.SetVisibility (true); //WebViewを表示する
		#endif
	}

	public void HideBannerAd () {
		#if !UNITY_EDITOR
		webViewObject.SetVisibility (false);
		#endif
	}
}
