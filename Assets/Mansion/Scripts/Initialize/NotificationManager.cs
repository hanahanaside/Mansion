using UnityEngine;
using System.Collections;
using System;

public class NotificationManager : MonoBehaviour {
	private static NotificationManager sInstance;

	void Start () {
		sInstance = this;
		DontDestroyOnLoad (gameObject);
	}

	void OnApplicationPause (bool pauseStatus) {
		if (pauseStatus) {
			ScheduleLocalNotification ();
		} else {
			ClearNotifications ();
		}
	}

	public static NotificationManager Instance {
		get {
			return sInstance;
		}
	}

	public void ScheduleLocalNotification () {
		Debug.Log ("ScheduleLocalNotification");

		#if UNITY_IPHONE
		NotificationServices.CancelAllLocalNotifications ();
		_ScheduleLocalNotification ();
		#endif

		#if UNITY_ANDROID
		long secondsFromNow =  60L;
		string title = "ウハウハ";
		string subTitle = "泥棒に襲われています";
		string tickerText = "泥棒に襲われています";
		string extraData = "extraData";
		EtceteraAndroid.scheduleNotification(secondsFromNow,title,subTitle,tickerText,extraData);
		#endif
	}

	private void _ScheduleLocalNotification () {
		int pushCount = 4;
		long secomCount = PrefsManager.Instance.GetSecomData ().Count;
		DateTime lastFireDate = System.DateTime.Now;
		for (int i = 0; i < pushCount; i++) {
			string title = "";
			if (secomCount >= i + 1) {
				title = "アルツックがドロボーを撃退しました！";
			} else {
				title = "ドロボーに襲われてるよ！早く退治して！";
			}
			double addSeconds = (double)UnityEngine.Random.Range (3, 12);
			Debug.Log ("add seconds = " + addSeconds);
			#if UNITY_IPHONE
			LocalNotification localNotification = new LocalNotification ();
			localNotification.applicationIconBadgeNumber = 1;
			localNotification.alertBody = title;
			localNotification.soundName = LocalNotification.defaultSoundName;
			localNotification.hasAction = true;
			lastFireDate = lastFireDate.AddSeconds (addSeconds);
			localNotification.fireDate = lastFireDate;
			NotificationServices.ScheduleLocalNotification (localNotification);
			#endif
		}
	}

	private void ClearNotifications () {
		#if UNITY_IPHONE
		LocalNotification localNtification = new LocalNotification ();
		localNtification.applicationIconBadgeNumber = -1;
		NotificationServices.PresentLocalNotificationNow (localNtification);
		NotificationServices.CancelAllLocalNotifications ();
		#endif

		#if UNITY_ANDROID
		EtceteraAndroid.cancelAllNotifications();
		#endif


	}
}
