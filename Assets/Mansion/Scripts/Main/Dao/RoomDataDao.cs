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
		Debug.Log ("sql = " + sql);
		SQLiteQuery sqliteQuery = new SQLiteQuery (sqliteDB, sql);
		List<RoomData> roomDataList = new List<RoomData> ();
		while (sqliteQuery.Step ()) {
			RoomData roomData = GetRoomDataFromQuery (sqliteQuery);
			roomDataList.Add (roomData);
		}
		CloseDatabase (sqliteDB, sqliteQuery);
		return roomDataList;
	}

	public List<RoomData> GetUnLockRoomDataList () {
		List<RoomData> roomDataList = GetRoomDataList ();
		List<RoomData> unlockRoomDataList = new List<RoomData> ();
		foreach (RoomData roomData in roomDataList) {
			if (roomData.ItemCount >= 1) {
				unlockRoomDataList.Add (roomData);
			}
		}
		return unlockRoomDataList;
	}

	public decimal GetTotalGenerateSpeed () {
		decimal totalGenerateSpeed = 0.0m;
		List<RoomData> roomDataList = GetRoomDataList ();
		foreach(RoomData roomData in roomDataList){
			int itemCount = roomData.ItemCount;
			decimal generateSpeed = roomData.GenerateSpeed;
			int effect = ShopItemDataDao.Instance.GetEffectByRoomId (roomData.Id);
			totalGenerateSpeed += (generateSpeed * itemCount) * effect;
			Debug.Log ("(" + generateSpeed  + " * " + itemCount + ") * " + effect);
		}
		return totalGenerateSpeed;
	}

	public RoomData GetRoomDataById (int id) {
		SQLiteDB sqliteDB = OpenDatabase ();
		StringBuilder sb = new StringBuilder ();
		sb.Append ("select * from " + ROOM_DATA_LIST_TABLE + " ");
		sb.Append ("where " + RoomDataField.ID + " = " + id + ";");
		Debug.Log ("sql = " + sb.ToString());
		SQLiteQuery sqliteQuery = new SQLiteQuery (sqliteDB, sb.ToString ());
		RoomData roomData = null;
		while(sqliteQuery.Step()){
			roomData = GetRoomDataFromQuery (sqliteQuery);
		}
		CloseDatabase (sqliteDB, sqliteQuery);
		return roomData;
	}

	public void UpdateItemCount (RoomData roomData) {
		SQLiteDB sqliteDB = OpenDatabase ();
		StringBuilder sb = new StringBuilder ();
		sb.Append ("update " + ROOM_DATA_LIST_TABLE + " ");
		sb.Append ("set " + RoomDataField.ITEM_COUNT + " = " + roomData.ItemCount + " ");
		sb.Append ("where " + RoomDataField.ID + " = " + roomData.Id + ";");
		Debug.Log ("sql = " + sb.ToString ());
		SQLiteQuery sqliteQuery = new SQLiteQuery (sqliteDB, sb.ToString ());
		sqliteQuery.Step ();
		CloseDatabase (sqliteDB, sqliteQuery);
	}

	private RoomData GetRoomDataFromQuery (SQLiteQuery sqliteQuery) {
		RoomData roomData = new RoomData ();
		roomData.Id = sqliteQuery.GetInteger (RoomDataField.ID);
		roomData.ItemCount = sqliteQuery.GetInteger (RoomDataField.ITEM_COUNT);
		string price = sqliteQuery.GetString (RoomDataField.PRICE);
		roomData.ItemPrice = System.Convert.ToInt64 (price);
		roomData.GenerateSpeed = (decimal)sqliteQuery.GetDouble (RoomDataField.GENERATE_SPEED);
		roomData.ItemName = sqliteQuery.GetString (RoomDataField.NAME);
		roomData.ItemDescription = sqliteQuery.GetString (RoomDataField.DESCRIPTION);
		return roomData;
	}
}
