using UnityEngine;
using System.Collections;

public class IconAd : MonoBehaviour {
	public WebViewObject webViewObject;
	private static IconAd sInstance;

	
	void Start () {
		sInstance = this;
		#if !UNITY_EDITOR
		DontDestroyOnLoad (gameObject);
		webViewObject.Init (); //初期化
		webViewObject.LoadURL ("http://ad.graasb.com/shakky/money/ios/icon/"); //ページの読み込み
		webViewObject.SetMargins (-260, 120, 520, 900); //上に100pxマージンを取る
		#endif
	}

	void OnApplicationPause(bool pauseStatus){
		#if !UNITY_EDITOR
		if(!pauseStatus){
			webViewObject.LoadURL ("http://ad.graasb.com/shakky/money/ios/icon/"); //ページの読み込み
		}
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

}
