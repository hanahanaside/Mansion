using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class ShareManager : MonoBehaviour {
	public string[] shareFileNameArray;
	private static ShareManager sInstance;
	#if UNITY_ANDROID
	private bool mSharebuttonClicked = false;
	#endif

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
	void Start () {
		sInstance = this;
		DontDestroyOnLoad (gameObject);
		#if UNITY_ANDROID && !UNITY_EDITOR
		StartCoroutine (WriteBytes ());
		#endif
	}

	void OnApplicationPause (bool pauseStatus) {
		#if UNITY_ANDROID
		if (!pauseStatus) {
			if(mSharebuttonClicked){
				ShareBoostTimeKeeper.Instance.StartBoost ();
				CountManager.Instance.StartBoost ();
				mSharebuttonClicked = false;
			}
		}
		#endif
	}

	public static ShareManager Instance {
		get {
			return sInstance;
		}
	}

	public void Share () {
		string fileName = shareFileNameArray [0];
		decimal moneyCout = StatusDataKeeper.Instance.StatusData.MaxKeepCount;
		string moneyString = MoneyCountConverter.Convert (moneyCout);
		StringBuilder sb = new StringBuilder ();
		sb.Append ("[" + moneyString + "]\n");
		sb.Append ("のお金を稼ぎました！\n");
		sb.Append ("「ウハマン～人生逆転ゲーム〜」\n");
		sb.Append ("#ウハマン\n");
		#if UNITY_IPHONE
		sb.Append ("http://bit.ly/ZicIhZ");
		string imagePath = Application.streamingAssetsPath + "/" + fileName;
		TwitterBinding.showTweetComposer (sb.ToString(), imagePath);
		#endif

		#if UNITY_ANDROID
		mSharebuttonClicked = true;
		string path = Application.persistentDataPath + "/" + fileName;
		sb.Append("http://bit.ly/1vp0U88");
		SocialConnector.Share (sb.ToString(), "", path);
		#endif
	}
	#if UNITY_ANDROID
	private IEnumerator WriteBytes () {
		for (int i = 0; i < shareFileNameArray.Length; i++) {
			string fileName = shareFileNameArray [i];
			string baseFilePath = Application.streamingAssetsPath + "/" + fileName;
			WWW www = new WWW (baseFilePath);
			yield return www;
			string filePath = Application.persistentDataPath + "/" + fileName;
			File.WriteAllBytes (filePath, www.bytes);
		}
	}
	#endif
}
