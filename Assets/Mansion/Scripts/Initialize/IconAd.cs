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
		webViewObject.SetMargins (0, 170, 0, 850);
		#endif
	}

	public void SetDefaultMargins () {
		#if UNITY_IPHONE
		//iPhone4
		int height = Screen.height;
		if (height == 960) {
			webViewObject.SetMargins (50, 170, 0, 850); 
		} else {
			webViewObject.SetMargins (0, 120, 0, 900); 
		}
		#endif
	}

	private void InitAd () {
		webViewObject.Init (); //初期化
		#if UNITY_IPHONE
		webViewObject.LoadURL ("http://ad.graasb.com/shakky/money/ios/icon/"); //ページの読み込み
		#endif
		SetDefaultMargins ();
	}
}
