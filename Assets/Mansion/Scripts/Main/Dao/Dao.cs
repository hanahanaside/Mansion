using UnityEngine;
using System.Collections;

public abstract class Dao {

	protected string ROOM_DATA_LIST_TABLE = "room_data_list";
	protected string ENEMY_DATA_LIST_TABLE = "enemy_data_list";
	protected string SHOP_ITEM_DATA_LIST_TABLE = "shop_item_data_list";
	protected string HISTORY_DATA_LIST_TABLE = "history_data_list";

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

	protected class EnemyDataField{
		public const string ID = "id";
		public const string ATACK = "atack";
		public const string NAME = "name";
		public const string DESCRIPTION = "description";
	}

	protected class ShopItemDataField{
		public const string ID = "id";
		public const string NAME = "name";
		public const string DESCRIPTION = "description";
		public const string TAG = "tag";
		public const string PRICE = "price";
		public const string UNLOCK_LEVEL = "unlock_level";
		public const string UNLOCK_CONDITION = "unlock_condition";
		public const string TARGET_ROOM_ID = "target_room_id";
		public const string EFFECT = "effect";
	}

	protected class HistoryDataField{
		public const string IDictionary = "id";
		public const string ENEMY_ID = "enemy_id";
		public const string DAMAGE = "damage";
		public const string DATE = "date";
	}
}
