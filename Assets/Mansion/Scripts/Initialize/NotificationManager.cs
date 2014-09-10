using UnityEngine;
using System.Collections;

public class NotificationManager : MonoBehaviour {
	private static NotificationManager sInstance;

	void Start () {
		sInstance = this;
		DontDestroyOnLoad (gameObject);
	}

	void OnApplicationPause (bool pauseStatus) {
		if (pauseStatus) {
			ScheduleLocalNotification ();
//			GameObject enemyObject = GameObject.FindWithTag ("Enemy");
//			if (enemyObject == null) {
//				ScheduleLocalNotification ();
//			}
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
		_ScheduleLocalNotification ("ドロボーにおそわれました1");
		_ScheduleLocalNotification ("ドロボーにおそわれました2");
		_ScheduleLocalNotification ("ドロボーにおそわれました3");
		_ScheduleLocalNotification ("ドロボーにおそわれました4");
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

	private void _ScheduleLocalNotification (string title) {
		#if UNITY_IPHONE
		LocalNotification localNotification = new LocalNotification ();
		localNotification.applicationIconBadgeNumber = 4;
		localNotification.alertBody = title;
		localNotification.fireDate = System.DateTime.Now.AddSeconds (60);
		NotificationServices.ScheduleLocalNotification (localNotification);
		#endif
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
