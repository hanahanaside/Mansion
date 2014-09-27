using UnityEngine;
using System.Collections;

public class RectangleAd : MonoBehaviour {
	public WebViewObject webViewObject;
	private static RectangleAd sInstance;

	void Start () {
		sInstance = this;
		DontDestroyOnLoad (gameObject);
		#if !UNITY_EDITOR
		Init();
		#endif
	}

	void OnApplicationPause (bool pauseStatus) {
		#if !UNITY_EDITOR
		if (!pauseStatus) {
			LoadURL();
		}
		#endif
	}

	public static RectangleAd Instance {
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
		DontDestroyOnLoad (gameObject);
		webViewObject.Init (); //初期化
		LoadURL ();
		int height = Screen.height;
		if (height == 960) {
			webViewObject.SetMargins (0, 150, 0, 300); 
		} else {
			webViewObject.SetMargins (0, 300, 0, 300); 
		}
	}

	private void LoadURL () {
		string url = "";
		#if UNITY_IPHONE
		url = "http://ad.graasb.com/shakky/money/ios/rectangle/";
		#endif
		#if UNITY_ANDROID
		url = "http://ad.graasb.com/shakky/money/android/top/";
		#endif
		webViewObject.LoadURL (url); //ページの読み込み
	}

}