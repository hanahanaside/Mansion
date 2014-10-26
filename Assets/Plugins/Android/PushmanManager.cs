using UnityEngine;
using System.Collections;

public class PushmanManager : MonoBehaviour {
	private AndroidJavaObject mActivity;
	private AndroidJavaClass mPushman;

	void Start () {
		AndroidJavaClass player = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");   
		mActivity = player.GetStatic<AndroidJavaObject> ("currentActivity");
		mActivity.Call ("runOnUiThread", new AndroidJavaRunnable (() => {
			mPushman = new AndroidJavaClass ("jp.pushman.android.PushmanSDK");
			mPushman.CallStatic ("receiveOnCreate", mActivity);
		}));
	}

	void OnApplicationPause (bool pauseStatus) {
		Debug.Log ("pause");
		if (pauseStatus) {
			mPushman.CallStatic ("stop", mActivity);
		} else {
			mPushman.CallStatic ("start", mActivity);
		}
	}
}
