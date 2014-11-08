using UnityEngine;
using System.Collections;
using System;

public class TutorialWebView : MonoBehaviour {
	private const string URL = "http://ad.graasb.com/shakky/money/tutorial/";
	public WebViewObject webViewObject;

	void Start () {
		#if UNITY_IPHONE
			ShowProgressDialog();
			LoadURL();
		#endif

	}

	private void ShowProgressDialog(){
		#if UNITY_IPHONE
		EtceteraBinding.showBezelActivityViewWithLabel("Loading");
		#endif

		#if UNITY_ANDROID
		EtceteraAndroid.showProgressDialog("Loading","please Wait");
		#endif
	}
		
	private void LoadURL () {
		webViewObject.Init ((msg) => {
			if (msg == "jswfEndFrame") {
				// SWF の変換終了時に送信されます。
				#if UNITY_IPHONE
				EtceteraBinding.hideActivityView();
				#endif
				#if UNITY_ANDROID
				EtceteraAndroid.hideProgressDialog();
				#endif
			} else if (msg == "jswfBeginFrame") {
				// フレームの処理を開始する直前に送信されます。
			} else if (msg == "jswfPreFrameActions") {
				// フレームアクションを実行する直前に送信されます。
			} else if (msg == "jswfPreRenderFrame") {
				// フレームの描画を行う直前に送信されます。
			} else if (msg == "jswfEndFrame") {
				// フレームの処理が終了した直後に送信されます。
			} else if (msg == "close") {
				// チュートリアル終了
				#if !UNITY_EDITOR
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
			InsertHistoryData();
			PrefsManager.Instance.FlagTutorialFinished = 1;
			Application.LoadLevel ("Main");
		};
		alertDialog.Show (title, message, "OK");
	}

	private void InsertHistoryData(){
		DateTime dtNow = DateTime.Now;
		string date = dtNow.ToString ("MM/dd HH:mm");
		HistoryData historyData = new HistoryData ();
		historyData.EnemyId = 2;
		historyData.Damage = "31";
		historyData.Date = date;
		historyData.FlagSecom = 0;
		HistoryDataDao.Instance.InsertHistoryData (historyData);
	}
}
