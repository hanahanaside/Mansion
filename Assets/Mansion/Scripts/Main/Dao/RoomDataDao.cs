using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class RoomDataDao : Dao {

	private static RoomDataDao sInstance;

	public static RoomDataDao Instance {
		get {
			if (sInstance == null) {
				sInstance = new RoomDataDao ();
			}
			return sInstance;
		}
	}

	public List<RoomData> GetRoomDataList () {
		SQLiteDB sqliteDB = OpenDatabase ();
		string sql = "select * from " + ROOM_DATA_LIST_TABLE + ";";
		SQLiteQuery sqliteQuery = new SQLiteQuery (sqliteDB, sql);
		List<RoomData> roomDataList = new List<RoomData> ();
		while (sqliteQuery.Step()) {
			RoomData roomData = new RoomData ();
			roomData.Id = sqliteQuery.GetInteger (RoomDataField.ID);
			roomData.ItemCount = sqliteQuery.GetInteger (RoomDataField.ITEM_COUNT);
			roomData.ItemPrice = sqliteQuery.GetInteger (RoomDataField.PRICE);
			roomData.GenerateSpeed = (float)sqliteQuery.GetDouble (RoomDataField.GENERATE_SPEED);
			roomData.ItemName = sqliteQuery.GetString (RoomDataField.NAME);
			roomData.ItemDescription = sqliteQuery.GetString (RoomDataField.DESCRIPTION);
			roomDataList.Add (roomData);
		}
		CloseDatabase (sqliteDB, sqliteQuery);
		return roomDataList;
	}

	public float GetTotalGenerateSpeed () {
		float totalGenerateSpeed = 0.0f;
		SQLiteDB sqliteDB = OpenDatabase ();
		StringBuilder sb = new StringBuilder ();
		sb.Append ("select " + RoomDataField.ITEM_COUNT + ", " + RoomDataField.GENERATE_SPEED + " ");
		sb.Append ("from " + ROOM_DATA_LIST_TABLE + ";");
		SQLiteQuery sqliteQuery = new SQLiteQuery (sqliteDB, sb.ToString ());
		while (sqliteQuery.Step()) {
			int itemCount = sqliteQuery.GetInteger (RoomDataField.ITEM_COUNT);
			float generateSpeed = (float)sqliteQuery.GetDouble (RoomDataField.GENERATE_SPEED);
			totalGenerateSpeed += generateSpeed * itemCount;
		}
		CloseDatabase (sqliteDB, sqliteQuery);
		return totalGenerateSpeed;
	}

	public void UpdateItemCount (RoomData roomData) {
		SQLiteDB sqliteDB = OpenDatabase ();
		StringBuilder sb = new StringBuilder ();
		sb.Append ("update " + ROOM_DATA_LIST_TABLE + " ");
		sb.Append ("set " + RoomDataField.ITEM_COUNT + " = " + roomData.ItemCount + " ");
		sb.Append ("where " + roomData.Id + " = " + roomData.Id + ";");
		SQLiteQuery sqliteQuery = new SQLiteQuery (sqliteDB, sb.ToString ());
		sqliteQuery.Step ();
		CloseDatabase (sqliteDB, sqliteQuery);
	}
}
