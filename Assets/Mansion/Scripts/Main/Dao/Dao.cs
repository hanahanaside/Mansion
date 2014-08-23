using UnityEngine;
using System.Collections;

public abstract class Dao {

	protected string ROOM_DATA_LIST_TABLE = "room_data_list";

	protected SQLiteDB OpenDatabase(){
		string filePath = Application.persistentDataPath + "/manshon.db";
		SQLiteDB sqliteDB = new SQLiteDB();
		sqliteDB.Open(filePath);
		return sqliteDB;
	}

	protected void CloseDatabase(SQLiteDB sqliteDB,SQLiteQuery sqliteQuery){
		sqliteQuery.Release();
		sqliteDB.Close();
	}

	protected class RoomDataField{
		public const string ID = "id";
		public const string ITEM_COUNT = "item_count";
		public const string PRICE = "price";
		public const string GENERATE_SPEED = "generate_speed";
		public const string NAME = "name";
		public const string DESCRIPTION = "description";
	}
}
