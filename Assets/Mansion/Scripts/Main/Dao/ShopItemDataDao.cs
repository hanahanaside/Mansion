using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class ShopItemDataDao : Dao {
	private static ShopItemDataDao sInstance;

	public static ShopItemDataDao Instance {
		get {
			if (sInstance == null) {
				sInstance = new ShopItemDataDao ();
			}
			return sInstance;
		}
	}

	public List<ShopItemData> GetShopItemDataList () {
		SQLiteDB sqliteDB = OpenDatabase ();
		string sql = "select * from " + SHOP_ITEM_DATA_LIST_TABLE + ";";
		Debug.Log ("sql = " + sql);
		SQLiteQuery sqliteQuery = new SQLiteQuery (sqliteDB, sql);
		List<ShopItemData> shopItemDataList = new List<ShopItemData> ();
		while (sqliteQuery.Step ()) {
			ShopItemData shopItemData = GetShopItemDataFromQuery (sqliteQuery);
			shopItemDataList.Add (shopItemData);
		}
		CloseDatabase (sqliteDB, sqliteQuery);
		return shopItemDataList;
	}

	public List<ShopItemData> QueryByTargetRoomId (int roomId) {
		SQLiteDB sqliteDB = OpenDatabase ();
		StringBuilder sb = new StringBuilder ();
		sb.Append ("select * from " + SHOP_ITEM_DATA_LIST_TABLE + " ");
		sb.Append ("where " + ShopItemDataField.TARGET_ROOM_ID + " = " + roomId);
		Debug.Log ("sql = " + sb.ToString ());
		SQLiteQuery sqliteQuery = new SQLiteQuery (sqliteDB, sb.ToString ());
		List<ShopItemData> shopItemDataList = new List<ShopItemData> ();
		while (sqliteQuery.Step ()) {
			ShopItemData shopItemData = GetShopItemDataFromQuery (sqliteQuery);
			shopItemDataList.Add (shopItemData);
		}
		CloseDatabase (sqliteDB, sqliteQuery);
		return shopItemDataList;
	}

	public ShopItemData GetShopItemDataById(int id){
		SQLiteDB sqliteDB = OpenDatabase ();
		StringBuilder sb = new StringBuilder ();
		sb.Append ("select * from " + SHOP_ITEM_DATA_LIST_TABLE + " ");
		sb.Append ("where " + ShopItemDataField.ID + " = " + id + ";");
		SQLiteQuery sqliteQuery = new SQLiteQuery (sqliteDB, sb.ToString ());
		ShopItemData shopItemData = null;
		while(sqliteQuery.Step()){
			shopItemData = GetShopItemDataFromQuery (sqliteQuery);
		}
		CloseDatabase (sqliteDB, sqliteQuery);
		return shopItemData;
	}

	public void UpdateUnLockLevel (int id, int unlockLevel) {
		SQLiteDB sqliteDB = OpenDatabase ();
		StringBuilder sb = new StringBuilder ();
		sb.Append ("update " + SHOP_ITEM_DATA_LIST_TABLE + " ");
		sb.Append ("set " + ShopItemDataField.UNLOCK_LEVEL + " = " + unlockLevel + " ");
		sb.Append ("where " + ShopItemDataField.ID + " = " + id + ";");
		Debug.Log ("sql = " + sb.ToString ());
		SQLiteQuery sqliteQuery = new SQLiteQuery (sqliteDB, sb.ToString ());
		sqliteQuery.Step ();
		CloseDatabase (sqliteDB, sqliteQuery);
	}

	public int GetEffectByRoomId (int roomId) {
		SQLiteDB sqliteDB = OpenDatabase ();
		StringBuilder sb = new StringBuilder ();
		sb.Append ("select " + ShopItemDataField.EFFECT + "," + ShopItemDataField.UNLOCK_LEVEL + " ");
		sb.Append ("from " + SHOP_ITEM_DATA_LIST_TABLE + " ");
		sb.Append ("where " + ShopItemDataField.TARGET_ROOM_ID + " = " + roomId + ";");
		SQLiteQuery sqliteQuery = new SQLiteQuery (sqliteDB, sb.ToString ());
		int effect = 1;
		while (sqliteQuery.Step ()) {
			int unlockLevel = sqliteQuery.GetInteger (ShopItemDataField.UNLOCK_LEVEL);
			if (unlockLevel == ShopItemData.UNLOCK_LEVEL_BOUGHT) {
				effect = sqliteQuery.GetInteger (ShopItemDataField.EFFECT);
			}
		}
		CloseDatabase (sqliteDB, sqliteQuery);
		return effect;
	}

	public ShopItemData GetPitData () {
		ShopItemData pitData = null;
		SQLiteDB sqliteDB = OpenDatabase ();
		StringBuilder sb = new StringBuilder ();
		sb.Append ("select * from " + SHOP_ITEM_DATA_LIST_TABLE + " ");
		sb.Append ("where " + ShopItemDataField.TAG + " = '" + ShopItemData.TAG_PIT + "' ");
		sb.Append ("and " + ShopItemDataField.UNLOCK_LEVEL + " = " + ShopItemData.UNLOCK_LEVEL_BOUGHT + ";");
		SQLiteQuery sqliteQuery = new SQLiteQuery (sqliteDB, sb.ToString ());
		while (sqliteQuery.Step ()) {
			ShopItemData shopItemData = GetShopItemDataFromQuery (sqliteQuery);
			pitData = shopItemData;
		}
		CloseDatabase (sqliteDB, sqliteQuery);
		return pitData;
	}

	private ShopItemData GetShopItemDataFromQuery (SQLiteQuery sqliteQuery) {
		ShopItemData shopItemData = new ShopItemData ();
		shopItemData.Id = sqliteQuery.GetInteger (ShopItemDataField.ID);
		shopItemData.Name = sqliteQuery.GetString (ShopItemDataField.NAME);
		shopItemData.Description = sqliteQuery.GetString (ShopItemDataField.DESCRIPTION);
		shopItemData.Tag = sqliteQuery.GetString (ShopItemDataField.TAG);
		shopItemData.Price = sqliteQuery.GetInteger (ShopItemDataField.PRICE);
		shopItemData.UnlockLevel = sqliteQuery.GetInteger (ShopItemDataField.UNLOCK_LEVEL);
		shopItemData.UnLockCondition = sqliteQuery.GetInteger (ShopItemDataField.UNLOCK_CONDITION);
		shopItemData.TargetRoomId = sqliteQuery.GetInteger (ShopItemDataField.TARGET_ROOM_ID);
		shopItemData.Effect = sqliteQuery.GetInteger (ShopItemDataField.EFFECT);
		return shopItemData;
	}
}
