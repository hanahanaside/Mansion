using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyDataDao : Dao {

	private static EnemyDataDao sInstance;

	public static EnemyDataDao Instance {
		get {
			if (sInstance == null) {
				sInstance = new EnemyDataDao ();
			}
			return sInstance;
		}
	}

	public EnemyData QueryEnemyData (int id) {
		SQLiteDB sqliteDB = OpenDatabase ();
		string sql = "select * from " + ENEMY_DATA_LIST_TABLE + " where " + EnemyDataField.ID + " = " + id + ";";
		Debug.Log("sql = "+ sql);
		SQLiteQuery sqliteQuery = new SQLiteQuery (sqliteDB, sql);
		EnemyData enemyData = new EnemyData ();
		while (sqliteQuery.Step()) {
			enemyData.Id = sqliteQuery.GetInteger (EnemyDataField.ID);
			enemyData.Name = sqliteQuery.GetString (EnemyDataField.NAME);
			enemyData.Description = sqliteQuery.GetString (EnemyDataField.DESCRIPTION);
			enemyData.Atack = sqliteQuery.GetInteger (EnemyDataField.ATACK);
		}
		return enemyData;
	}


}
