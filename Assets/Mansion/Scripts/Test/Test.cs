using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

public class Test : MonoBehaviour {
	void Start () {
		AndroidJavaClass player = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");   
		AndroidJavaObject activity = player.GetStatic<AndroidJavaObject> ("currentActivity");
		activity.Call ("runOnUiThread", new AndroidJavaRunnable (() => {
			AndroidJavaClass pushman = new AndroidJavaClass ("jp.pushman.android.PushmanSDK");
			pushman.CallStatic ("receiveOnCreate", activity);
		}));

	
	}
}
