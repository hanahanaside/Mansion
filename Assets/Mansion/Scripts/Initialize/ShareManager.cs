using UnityEngine;
using System.Collections;
using System.IO;

public class ShareManager : MonoBehaviour {
	public string[] shareFileNameArray;
	private static ShareManager sInstance;

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
		#if UNITY_ANDROID
		StartCoroutine (WriteBytes ());
		#endif
	}

	public static ShareManager Instance {
		get {
			return sInstance;
		}
	}

	public void Share () {
		string fileName = shareFileNameArray [0];
		#if UNITY_IPHONE
		StringBuilder sb = new StringBuilder();
		sb.Append("text\n");
		sb.Append("https://itunes.apple.com/jp/app/pazuru-doragonzu/id493470467?mt=8\n");
		sb.Append("#ウハウハ");
		string imagePath = Application.streamingAssetsPath + "/" + fileName;
		TwitterBinding.showTweetComposer (sb.ToString(), imagePath);
		#endif

		#if UNITY_ANDROID
		string path = Application.persistentDataPath + "/" + fileName;
		string text = "text";
		string url = "https://play.google.com/store/apps/details?id=jp.gungho.pad";
		SocialConnector.Share (text, url, path);
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
