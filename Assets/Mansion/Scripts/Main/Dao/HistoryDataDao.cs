using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class HistoryDataDao : Dao {
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
		SQLiteDB sqliteDB = OpenDatabase ();
		string sql = "select * from " + HISTORY_DATA_LIST_TABLE + ";";
		SQLiteQuery sqliteQuery = new SQLiteQuery (sqliteDB, sql);
		List<HistoryData> historyDataList = new List<HistoryData> ();
		while (sqliteQuery.Step ()) {
			HistoryData historyData = new HistoryData ();
			historyData.EnemyId = sqliteQuery.GetInteger (HistoryDataField.ENEMY_ID);
			historyData.Damage = sqliteQuery.GetString (HistoryDataField.DAMAGE);
			historyData.Date = sqliteQuery.GetString (HistoryDataField.DATE);
			historyDataList.Add (historyData);
			Debug.Log ("id = " + historyData.EnemyId);
			Debug.Log ("damage = " + historyData.Damage);
			Debug.Log ("date = " + historyData.Date);
		}
		CloseDatabase (sqliteDB, sqliteQuery);
		return historyDataList;
	}

	public void InsertHistoryData (HistoryData historyData) {
		SQLiteDB sqliteDB = OpenDatabase ();
		StringBuilder sb = new StringBuilder ();
		sb.Append ("insert into " + HISTORY_DATA_LIST_TABLE + " values(");
		sb.Append ("null,");
		sb.Append (historyData.EnemyId + ",");
		sb.Append ("'" + historyData.Damage + "',");
		sb.Append ("'" + historyData.Date + "'");
		sb.Append (");");
		SQLiteQuery sqliteQuery = new SQLiteQuery (sqliteDB, sb.ToString ());
		sqliteQuery.Step ();
		CloseDatabase (sqliteDB, sqliteQuery);
	}
}
