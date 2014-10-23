using UnityEngine;
using System.Collections;

public class IconAd : MonoBehaviour {
	public WebViewObject webViewObject;
	private static IconAd sInstance;

	void Start () {
		sInstance = this;
		DontDestroyOnLoad (gameObject);
		#if !UNITY_EDITOR
			InitAd();
		#endif
	}

	void OnApplicationPause (bool pauseStatus) {
		#if UNITY_EDITOR

		#elif UNITY_IPHONE
		if(!pauseStatus){
		webViewObject.LoadURL ("http://ad.graasb.com/shakky/money/ios/icon/"); //ページの読み込み
		}

		#elif UNITY_ANDROID

		#endif
	}

	public static IconAd Instance {
		get {
			return sInstance;
		}
	}

	public void ShowIconAd () {
		#if !UNITY_EDITOR
		webViewObject.SetVisibility (true);
		#endif
	}

	public void HideIconAd () {
		#if !UNITY_EDITOR
		webViewObject.SetVisibility (false);
		#endif
	}

	public void SetDownMargins () {
		#if UNITY_IPHONE
		int height = Screen.height;
		if (height == 960) {
			webViewObject.SetMargins (50, 270, 450, 800); 
		} else {
			webViewObject.SetMargins (0, 170, 500, 850);
		}
		#endif
	}

	public void SetDefaultMargins () {
		int height = Screen.height;
		#if UNITY_IPHONE
		//iPhone4
		if (height == 960) {
			webViewObject.SetMargins (50, 220, 450, 850); 
		} else {
			webViewObject.SetMargins (0, 120, 500, 900); 
		}
		#endif

		#if UNITY_ANDROID
		//	webViewObject.SetMargins (50, 150, 600, 1020); 
		webViewObject.SetMargins (0, 150, 550, 1010); 
		#endif
	}

	private void InitAd () {
		webViewObject.Init (); //初期化
		#if UNITY_IPHONE
		webViewObject.LoadURL ("http://ad.graasb.com/shakky/money/ios/icon/"); //ページの読み込み
		#endif
		#if UNITY_ANDROID
		webViewObject.LoadURL("http://ad.graasb.com/shakky/money/android/icon/");
		#endif
		SetDefaultMargins ();
	}
}
