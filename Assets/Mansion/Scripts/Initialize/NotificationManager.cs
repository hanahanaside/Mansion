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
			#if UNITY_IPHONE
			ClearBadgeNumber();
			#endif
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
		localNotification.fireDate = System.DateTime.Now.AddSeconds (60);
		localNotification.alertBody = "泥棒に襲われています";
		NotificationServices.CancelAllLocalNotifications ();
		NotificationServices.ScheduleLocalNotification (localNotification);
		#endif

		#if UNITY_ANDROID

		#endif
	}

	#if UNITY_IPHONE
	private void ClearBadgeNumber(){
		LocalNotification localNtification = new LocalNotification ();
		localNtification.applicationIconBadgeNumber = -1;
		NotificationServices.PresentLocalNotificationNow (localNtification);
	}
	#endif
}
