using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

public class Test : MonoBehaviour {

	private const string URL = "http://ad.graasb.com/shakky/money/tutorial/";


	public WebViewObject webView;

	void Start(){
		webView.Init ((msg) => {
			Debug.Log ("webViewObjectCallBack = " + msg);

			if (msg == "jswfEndFrame") {
				// SWF の変換終了時に送信されます。
				Debug.Log ("jswfEndFrame");
			} else if (msg == "jswfBeginFrame") {
				// フレームの処理を開始する直前に送信されます。
				Debug.Log ("jswfBeginFrame");
			} else if (msg == "jswfPreFrameActions") {
				// フレームアクションを実行する直前に送信されます。
				Debug.Log ("jswfPreFrameActions");
			} else if (msg == "jswfPreRenderFrame") {
				// フレームの描画を行う直前に送信されます。
				Debug.Log ("jswfPreRenderFrame");
			} else if (msg == "jswfEndFrame") {
				// フレームの処理が終了した直後に送信されます。
				Debug.Log ("jswfEndFrame");
			} else if (msg == "close") {
				// チュートリアル終了
				Debug.Log("finish");
			}
		});

		webView.LoadURL (URL);
		webView.SetMargins (0, 0, 0, 0);
		webView.SetVisibility (true);
		webView.EvaluateJS (
			"window.addEventListener('load', function() {" +
			"	window.Unity = {" +
			"		call:function(msg) {" +
			"			var iframe = document.createElement('IFRAME');" +
			"			iframe.setAttribute('src', 'unity:' + msg);" +
			"			document.documentElement.appendChild(iframe);" +
			"			iframe.parentNode.removeChild(iframe);" +
			"			iframe = null;" +
			"		}" +
			"	}" +
			"}, false);"
		);

	}

	public void OnButtonClicked(){
		Debug.Log("click");
		Binding2.SplashView ();
	}
}
