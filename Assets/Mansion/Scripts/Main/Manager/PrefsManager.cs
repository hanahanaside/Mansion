using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

public class PrefsManager {
	private const string SECOM_Data = "secomData";
	private const string STATUS_DATA = "statusData";
	private const string KEEP_MONEY_COUNT = "keepMoneyCount";
	private const string EXIT_DATE = "exitDate";
	private const string FLAG_REVIEW = "flagReview";
	private const string BOOST_TIME = "boostTime";
	private const string OPENING_FINISHED = "openingFinished";
	private const string NOTIFICATION_DATE_ARRAY = "notificationDateArray";
	private const string ANDROID_NOTIFICATION_ID_ARRAY = "androidNotificationIdArray";
	private static PrefsManager sInstance;

	public static PrefsManager Instance {
		get {
			if (sInstance == null) {
				sInstance = new PrefsManager ();
			}
			return sInstance;
		}
	}

	public void SaveSecomData (SecomData secomData) {
		string json = JsonParser.SerializeSecomData (secomData);
		PlayerPrefs.SetString (SECOM_Data, json);
		PlayerPrefs.Save ();
	}

	public SecomData GetSecomData () {
		string json = PlayerPrefs.GetString (SECOM_Data, "");
		Debug.Log ("json = " + json);
		SecomData secomData;
		if (json == "") {
			secomData = new SecomData ();
		} else {
			secomData = JsonParser.DeserializeSecomData (json);
		}
		return secomData;
	}

	public void SaveStatusData (StatusData statusData) {
		Debug.Log ("SaveStatusData");
		string json = JsonParser.SerializeStatusData (statusData);
		PlayerPrefs.SetString (STATUS_DATA, json);
		PlayerPrefs.Save ();
	}

	public StatusData GetStatusData () {
		string json = PlayerPrefs.GetString (STATUS_DATA, "");
		StatusData statusData;
		if (json == "") {
			statusData = new StatusData ();
		} else {
			statusData = JsonParser.DeserializeStatusData (json);
		}
		return statusData;
	}

	public void SaveMoneyCount (decimal keepMoneyCount) {
		PlayerPrefs.SetString (KEEP_MONEY_COUNT, "" + keepMoneyCount);
		PlayerPrefs.Save ();
	}

	public decimal GetMoneyCount () {
		Debug.Log ("GetMoneyCount");
		string keepMoneyCountString = PlayerPrefs.GetString (KEEP_MONEY_COUNT, "3000");
		//keepMoneyCountString = "1000000000000000000";
		decimal keepMoneyCount = 0;
		if (!string.IsNullOrEmpty (keepMoneyCountString)) {
			keepMoneyCount = decimal.Parse (keepMoneyCountString);
		}
		return keepMoneyCount;
	}

	public string ExitDate {
		set {
			PlayerPrefs.SetString (EXIT_DATE, value);
			PlayerPrefs.Save ();
		}
		get {
			return PlayerPrefs.GetString (EXIT_DATE);
		}
	}

	public void SaveReviewed () {
		PlayerPrefs.SetInt (FLAG_REVIEW, 1);
		PlayerPrefs.Save ();
	}

	public int FlagReview {
		get {
			return PlayerPrefs.GetInt (FLAG_REVIEW, 0);
		}
	}

	public int FlagTutorialFinished {
		get {
			return PlayerPrefs.GetInt (OPENING_FINISHED, 0);
		}
		set {
			PlayerPrefs.SetInt (OPENING_FINISHED, value);
			PlayerPrefs.Save ();
		}
	}

	public float BoostTime {
		set {
			PlayerPrefs.SetFloat (BOOST_TIME, value);
			PlayerPrefs.Save ();
		}
		get {
			return PlayerPrefs.GetFloat (BOOST_TIME, 0);
		}
	}

	public string[] NotificationDateArray {
		set {
			PlayerPrefsX.SetStringArray (NOTIFICATION_DATE_ARRAY, value);
		}
		get {
			return PlayerPrefsX.GetStringArray (NOTIFICATION_DATE_ARRAY);
		}
	}

	public int[] AndroidNotificationIdArray{
		set{
			PlayerPrefsX.SetIntArray (ANDROID_NOTIFICATION_ID_ARRAY,value);
		}
		get{
			return PlayerPrefsX.GetIntArray (ANDROID_NOTIFICATION_ID_ARRAY);
		}
	}
}
