using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyDataDao : Dao {

	private static EnemyDataDao sInstance;

	public static EnemyDataDao Instance{
		get{
			if(sInstance == null){
				sInstance = new EnemyDataDao();
			}
			return sInstance;
		}
	}

	public List<EnemyData> GetEnemyDataList(){
		SQLiteDB sqliteDB = OpenDatabase ();
		string sql = "select * from " + ENEMY_DATA_LIST_TABLE + ";";
		SQLiteQuery sqliteQuery = new SQLiteQuery (sqliteDB, sql);
		List<EnemyData> enemyDataList = new List<EnemyData> ();
		while(sqliteQuery.Step()){
			EnemyData enemyData = new EnemyData();
			enemyData.Id = sqliteQuery.GetInteger(EnemyDataField.ID);
			enemyData.Name = sqliteQuery.GetString(EnemyDataField.NAME);
			enemyData.Description = sqliteQuery.GetString(EnemyDataField.DESCRIPTION);
			enemyData.Atack = sqliteQuery.GetInteger(EnemyDataField.ATACK);
			enemyDataList.Add(enemyData);
		}
		CloseDatabase (sqliteDB, sqliteQuery);
		return enemyDataList;
	}
}
