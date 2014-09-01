using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

public class PrefsManager {
	private const string SECOM_Data = "secomData";
	private const string STATUS_DATA = "statusData";
	private const string KEEP_MONEY_COUNT = "keepMoneyCount";
	private const string EXIT_DATE = "exitDate";
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

	public void SaveMoneyCount (long keepMoneyCount) {
		PlayerPrefs.SetString (KEEP_MONEY_COUNT, "" + keepMoneyCount);
		PlayerPrefs.Save ();
	}

	public long GetMoneyCount () {
		string keepMoneyCountString = PlayerPrefs.GetString (KEEP_MONEY_COUNT);
		long keepMoneyCount = System.Convert.ToInt64 (keepMoneyCountString);
		return keepMoneyCount;
	}

	public void SaveExitDate (string exitDate) {
		PlayerPrefs.SetString (EXIT_DATE, exitDate);
		PlayerPrefs.Save ();
	}

	public string GetExitDate () {
		return PlayerPrefs.GetString (EXIT_DATE, "");
	}
}
