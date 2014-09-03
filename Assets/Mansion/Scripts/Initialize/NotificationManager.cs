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
			GameObject enemyObject = GameObject.FindWithTag ("Enemy");
			if (enemyObject == null) {
				ScheduleLocalNotification ();
			}
		}else {
			ClearNotifications();
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
		LocalNotification localNotification = new LocalNotification ();
		localNotification.applicationIconBadgeNumber = 1;
		localNotification.fireDate = System.DateTime.Now.AddSeconds (10);
		localNotification.alertBody = "泥棒に襲われています";
		NotificationServices.CancelAllLocalNotifications ();
		NotificationServices.ScheduleLocalNotification (localNotification);
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


	private void ClearNotifications(){
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
