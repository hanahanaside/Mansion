using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

public class PrefsManager {

	private const string SECOM_Data = "secomData";
	private const string SECOM_COUNT = "secomCount";
	private const string SECOM_MAX_COUNT = "secomMaxCount";
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
		Dictionary<string,object> dictionary = new Dictionary<string, object> ();
		dictionary.Add (SECOM_COUNT, secomData.Count);
		dictionary.Add (SECOM_MAX_COUNT, secomData.MacxCount);
		string json = Json.Serialize (dictionary);
		Debug.Log ("json = " + json);
	}

	public SecomData GetSecomData () {
		string json = PlayerPrefs.GetString (SECOM_Data, "");
		Debug.Log ("json = " + json);
		SecomData secomData = new SecomData ();
		if (json == "") {
			secomData.Count = 0;
			secomData.MacxCount = 0;
		} else {
			IList secomDataList = (IList)Json.Deserialize (json);
			IDictionary secomDataDictionary = (IDictionary)secomDataList [0];
			secomData.Count = (int)secomDataDictionary [SECOM_COUNT];
			secomData.MacxCount = (int)secomDataDictionary [SECOM_MAX_COUNT];
		}
		return secomData;
	}
}
