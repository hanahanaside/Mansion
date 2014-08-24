using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HistoryDataDao {

	private static HistoryDataDao sInstance;

	public static HistoryDataDao Instance {
		get {
			if (sInstance == null) {
				sInstance = new HistoryDataDao ();
			}
			return sInstance;
		}
	}

	public List<HistoryData> GetHistoryDataList () {
		List<HistoryData> historyDataList = new List<HistoryData> ();
		for (int i = 1; i<=4; i++) {
			HistoryData historyData = new HistoryData ();
			System.DateTime dateTimeNow = System.DateTime.Now;
			System.Text.StringBuilder sb = new System.Text.StringBuilder ();
			sb.Append (dateTimeNow.Month + "/");
			sb.Append (dateTimeNow.Day + " ");
			sb.Append(dateTimeNow.Hour+":");
			sb.Append(dateTimeNow.Minute);
			historyData.Id = i;
			historyData.EnemyId = i;
			historyData.Result = "result";
			historyData.Date = sb.ToString ();
			historyDataList.Add (historyData);
		}
		return historyDataList;
	}
}
