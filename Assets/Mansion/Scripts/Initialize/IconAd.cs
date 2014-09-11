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

	void OnApplicationPause(bool pauseStatus){
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

	public void ShowIconAd(){
		#if !UNITY_EDITOR
		webViewObject.SetVisibility (true);
		#endif
	}

	public void HideIconAd(){
		#if !UNITY_EDITOR
		webViewObject.SetVisibility (false);
		#endif
	}

	private void InitAd(){
		webViewObject.Init (); //初期化
		#if UNITY_IPHONE
		webViewObject.LoadURL ("http://ad.graasb.com/shakky/money/ios/icon/"); //ページの読み込み
		#endif

		webViewObject.SetMargins (-260, 120, 520, 900); //上に100pxマージンを取る
	}

}
