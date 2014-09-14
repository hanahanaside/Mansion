using UnityEngine;
using System.Collections;

public class BannerAd : MonoBehaviour {
	public WebViewObject webViewObject;
	private static BannerAd sInstance;

	void Start () {
		sInstance = this;
		DontDestroyOnLoad (gameObject);
		#if !UNITY_EDITOR
		Init();
		#endif
	}

	void OnApplicationPause (bool pauseStatus) {
		#if !UNITY_EDITOR
		if(!pauseStatus){
			LoadURL();
		}
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

	private void Init () {
		webViewObject.Init (); //初期化
		LoadURL ();
		webViewObject.SetMargins (0, 890, 0, 146); //下に100pxマージンを取る
	}

	private void LoadURL () {
		string url = "";
		#if UNITY_IPHONE
		url = "http://ad.graasb.com/shakky/money/ios/top/";
		#endif
		#if UNITY_ANDROID
		url = "http://ad.graasb.com/shakky/money/android/top/";
		#endif
		webViewObject.LoadURL (url); //ページの読み込み
	}
}
