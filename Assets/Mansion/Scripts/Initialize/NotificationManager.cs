using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
		_ScheduleLocalNotification ();
		#endif
	}

	private void _ScheduleLocalNotification () {
		int pushCount = 4;
		string[] notificationDateArray = new string[pushCount];
		long secomCount = PrefsManager.Instance.GetSecomData ().Count;
		DateTime fireDate = System.DateTime.Now;
		#if UNITY_ANDROID
		long secondsFromNow = 0L;
		int[] androidNotificationIdArray = new int[4];
		#endif
		for (int i = 0; i < pushCount; i++) {
			string title = "";
			if (secomCount >= i + 1) {
				title = "アルツックがドロボーを撃退しました！";
			} else {
				title = "ドロボーに襲われたよ！";
			}
			double addMinutes = (double)UnityEngine.Random.Range (30, 121);
			//double addMinutes = (double)UnityEngine.Random.Range (5, 10);
			#if UNITY_IPHONE
			Debug.Log ("add Minutes = " + addMinutes);
			LocalNotification localNotification = new LocalNotification ();
			localNotification.applicationIconBadgeNumber = 1;
			localNotification.alertBody = title;
			localNotification.soundName = LocalNotification.defaultSoundName;
			localNotification.hasAction = true;
			//lastFireDate = fireDate.AddSeconds (addMinutes);
			fireDate = fireDate.AddMinutes(addMinutes);
			localNotification.fireDate = fireDate;
			NotificationServices.ScheduleLocalNotification (localNotification);
			#endif

			#if UNITY_ANDROID
			double addSeconds = addMinutes * 60;
			fireDate = fireDate.AddSeconds(addSeconds);
			secondsFromNow += (long)addSeconds;
			Debug.Log("seconds from now = " + secondsFromNow);
			int notificationId = EtceteraAndroid.scheduleNotification (secondsFromNow, "ウハマン", title, title, "");
			androidNotificationIdArray [i] = notificationId;
			#endif
			notificationDateArray[i] = fireDate.ToString();

		}

		PrefsManager.Instance.NotificationDateArray = notificationDateArray;
		#if UNITY_ANDROID
		PrefsManager.Instance.AndroidNotificationIdArray = androidNotificationIdArray;
		#endif
	}

	private void ClearNotifications () {
		Debug.Log ("ClearNotifications");
		#if UNITY_IPHONE
		LocalNotification localNtification = new LocalNotification ();
		localNtification.applicationIconBadgeNumber = -1;
		NotificationServices.PresentLocalNotificationNow (localNtification);
		NotificationServices.CancelAllLocalNotifications ();
		#endif

		#if UNITY_ANDROID
		//お知らせに残っている通知を全て削除
		EtceteraAndroid.cancelAllNotifications();
		//スケジュールされている通知を全て削除
		int[] androidNotificationIdArray = PrefsManager.Instance.AndroidNotificationIdArray;
		foreach (int notificationId in androidNotificationIdArray) {
			EtceteraAndroid.cancelNotification (notificationId);
		}
		#endif


	}
}
