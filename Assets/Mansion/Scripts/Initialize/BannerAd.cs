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

	public void Show () {
		#if !UNITY_EDITOR
		webViewObject.SetVisibility (true); //WebViewを表示する
		#endif
	}

	public void Hide () {
		#if !UNITY_EDITOR
		webViewObject.SetVisibility (false);
		#endif
	}

	private void Init () {
		webViewObject.Init (); //初期化
		LoadURL ();
		int height = Screen.height;
		#if UNITY_IPHONE

		//iPhone4
		if(height == 960){
			webViewObject.SetMargins (0, 735, 0, 125); 
		}else {
			webViewObject.SetMargins (0, 890, 0, 146); 
		}
		#endif

		#if UNITY_ANDROID
		webViewObject.SetMargins (0,990, 0, 165); 
		#endif
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
