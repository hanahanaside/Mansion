using UnityEngine;
using System.Collections;

public class TutorialWebView : MonoBehaviour {
	//private const string URL = "http://ad.graasb.com/shakky/money/tutorial/";
	private const string URL = "http://www.yahoo.co.jp/";
	public WebViewObject webViewObject;


	void Start () {
		#if !UNITY_EDITOR
		LoadURL();
		#endif
	}

	public void Hide(){
		webViewObject.SetVisibility (false);
	}

	private void LoadURL(){
		webViewObject.Init ();
		webViewObject.LoadURL (URL);
		webViewObject.SetMargins (0,0,0,0);
		webViewObject.SetVisibility (true);
	}
}
