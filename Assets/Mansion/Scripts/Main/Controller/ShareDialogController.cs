using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class ShareDialogController : DialogController {

	#if UNITY_IPHONE
	void OnEnable () {
		TwitterManager.tweetSheetCompletedEvent += tweetSheetCompletedEvent;
	}

	void OnDisable () {
		TwitterManager.tweetSheetCompletedEvent -= tweetSheetCompletedEvent;
	}

	void tweetSheetCompletedEvent (bool didSucceed) {
		Debug.Log ("tweetSheetCompletedEvent " + didSucceed);
		if (didSucceed) {
			ShareBoostTimeKeeper.Instance.StartBoost ();
			CountManager.Instance.StartBoost ();
		}
	}
	#endif

	public void OnShareButtonClicked () {
		SoundManager.Instance.PlaySE (AudioClipID.SE_BUTTON);

		#if UNITY_IPHONE
		StringBuilder sb = new StringBuilder();
		sb.Append("text\n");
		sb.Append("https://itunes.apple.com/jp/app/pazuru-doragonzu/id493470467?mt=8\n");
		sb.Append("#ウハウハ");
		string imagePath = Application.streamingAssetsPath + "/share_image_1.png";
		TwitterBinding.showTweetComposer (sb.ToString(), imagePath);
		#endif

		#if UNITY_ANDROID
		StartCoroutine(ShareAndroid());
		#endif


	}

	public override void OnCloseButonClicked () {
		base.OnCloseButonClicked ();
		Destroy (transform.parent.gameObject);
	}
	#if UNITY_ANDROID
	private IEnumerator ShareAndroid () {
		string imagePath =  Application.streamingAssetsPath + "/share_image_1.png";
		WWW www = new WWW (imagePath);
		yield return www;
		Debug.Log ("url = " + www.url);
		string path = Application.persistentDataPath + "/share_image_1.png";
		string text = "text";
		string url = "https://play.google.com/store/apps/details?id=jp.gungho.pad";
		SocialConnector.Share (text, url, www.url);
	}
	#endif
}
