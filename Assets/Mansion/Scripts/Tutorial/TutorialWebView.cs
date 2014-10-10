using UnityEngine;
using System.Collections;

public class TutorialWebView : MonoBehaviour {
	private const string URL = "http://ad.graasb.com/shakky/money/tutorial/";
	public WebViewObject webViewObject;

	void Start () {
		#if UNITY_IPHONE
		EtceteraBinding.showBezelActivityViewWithLabel("Loading");
		#endif
		LoadURL();
	}
		
	private void LoadURL () {
		webViewObject.Init ((msg) => {
			Debug.Log ("webViewObjectCallBack = " + msg);

			if (msg == "jswfEndFrame") {
				// SWF の変換終了時に送信されます。
				Debug.Log ("jswfEndFrame");
				#if UNITY_IPHONE
				EtceteraBinding.hideActivityView();
				#endif
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
				#if UNITY_EDITOR
				Application.LoadLevel("Main");
				#else
				ShowTutorialBonusDialog();
				#endif
			}
		});

		webViewObject.LoadURL (URL);
		webViewObject.SetMargins (0, 0, 0, 0);
		webViewObject.SetVisibility (true);
		webViewObject.EvaluateJS (
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

	private void ShowTutorialBonusDialog () {
		string title = "チュートリアル完了ボーナス！";
		string message = "お金を3000円プレゼント！";
		AlertDialog alertDialog = new AlertDialog ();
		alertDialog.OnPositiveButtonClicked = () => {
			PrefsManager.Instance.FlagTutorialFinished = 1;
			Application.LoadLevel ("Main");
		};
		alertDialog.Show (title, message, "OK");
	}

}
