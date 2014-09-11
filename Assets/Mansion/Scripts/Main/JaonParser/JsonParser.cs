using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using System;

public class JsonParser : AbstractJsonParser {
	public static string SerializeStatusData (StatusData statusData) {
		Dictionary<string,object> dictionary = new Dictionary<string, object> ();
		dictionary.Add (StatusDataKies.TOTAL_GENERATE_COUNT, statusData.TotalGenerateCount);
		dictionary.Add (StatusDataKies.MAX_KEEP_COUNT, statusData.MaxKeepCount);
		dictionary.Add (StatusDataKies.TOTAL_PIT_GENERATE_COUNT, statusData.TotalPitGenerateCount);
		dictionary.Add (StatusDataKies.TOTAL_TAP_PIT_COUNT, statusData.TotalTapPitCount);
		dictionary.Add (StatusDataKies.TOTAL_CAME_ENEMY_COUNT, statusData.TotalCameEnemyCount);
		dictionary.Add (StatusDataKies.TOTAL_ATACK_ENEMY_COUNT, statusData.TotalAtackEnemyCount);
		dictionary.Add (StatusDataKies.TOTAL_USED_SECOM_COUNT, statusData.TotalUsedSecomCount);
		dictionary.Add (StatusDataKies.TOTAL_DAMEGED_COUNT, statusData.TotalDamegedCount);
		dictionary.Add (StatusDataKies.FIRST_GENERATE_DATE, statusData.FirstGenerateDate);
		string json = Json.Serialize (dictionary);
		return json;
	}

	public static StatusData DeserializeStatusData (string json) {
		Debug.Log ("json = " + json);
		Dictionary<string,object> dictionary = (Dictionary<string,object>)Json.Deserialize (json);
		StatusData statusData = new StatusData ();
		statusData.TotalGenerateCount = Convert.ToDecimal (dictionary [StatusDataKies.TOTAL_GENERATE_COUNT]);
		statusData.MaxKeepCount = Convert.ToDecimal (dictionary [StatusDataKies.MAX_KEEP_COUNT]); 
		statusData.TotalPitGenerateCount = Convert.ToInt64 (dictionary [StatusDataKies.TOTAL_PIT_GENERATE_COUNT]);
		statusData.TotalTapPitCount = Convert.ToInt64 (dictionary [StatusDataKies.TOTAL_TAP_PIT_COUNT]); 
		statusData.TotalCameEnemyCount = Convert.ToInt64 (dictionary [StatusDataKies.TOTAL_CAME_ENEMY_COUNT]); 
		statusData.TotalAtackEnemyCount = Convert.ToInt64 (dictionary [StatusDataKies.TOTAL_ATACK_ENEMY_COUNT]); 
		statusData.TotalUsedSecomCount = Convert.ToInt64 (dictionary [StatusDataKies.TOTAL_USED_SECOM_COUNT]); 
		statusData.TotalDamegedCount = Convert.ToDecimal (dictionary [StatusDataKies.TOTAL_DAMEGED_COUNT]); 
		statusData.FirstGenerateDate = Convert.ToString (dictionary [StatusDataKies.FIRST_GENERATE_DATE]); 
		return statusData;
	}

	public static string SerializeSecomData (SecomData secomData) {
		Dictionary<string,object> dictionary = new Dictionary<string, object> ();
		dictionary.Add (SecomDataKies.SECOM_COUNT, secomData.Count);
		dictionary.Add (SecomDataKies.SECOM_MAX_COUNT, secomData.MacxCount);
		string json = Json.Serialize (dictionary);
		return json;
	}

	public static SecomData DeserializeSecomData (string json) {
		IDictionary dictionary = (IDictionary)Json.Deserialize (json);
		SecomData secomData = new SecomData ();
		secomData.Count = (long)dictionary [SecomDataKies.SECOM_COUNT];
		secomData.MacxCount = (long)dictionary [SecomDataKies.SECOM_MAX_COUNT];
		return secomData;
	}
}
