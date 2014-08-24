using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShopItemDataDao : Dao{

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
		Debug.Log("sql = "+ sql);
		SQLiteQuery sqliteQuery = new SQLiteQuery (sqliteDB, sql);
		List<ShopItemData> shopItemDataList = new List<ShopItemData> ();
		while(sqliteQuery.Step()){
			ShopItemData shopItemData = new ShopItemData();
			shopItemData.Id = sqliteQuery.GetInteger(ShopItemDataField.ID);
			shopItemData.Name = sqliteQuery.GetString(ShopItemDataField.NAME);
			shopItemData.Description = sqliteQuery.GetString(ShopItemDataField.DESCRIPTION);
			shopItemData.Tag = sqliteQuery.GetString(ShopItemDataField.TAG);
			shopItemData.Price = sqliteQuery.GetInteger(ShopItemDataField.PRICE);
			shopItemData.UnlockLevel = sqliteQuery.GetInteger(ShopItemDataField.UNLOCK_LEVEL);
			shopItemData.UnLockCondition = sqliteQuery.GetInteger(ShopItemDataField.UNLOCK_CONDITION);
			shopItemData.TargetRoomId = sqliteQuery.GetInteger(ShopItemDataField.TARGET_ROOM_ID);
			shopItemData.Effect = sqliteQuery.GetInteger(ShopItemDataField.EFFECT);
			shopItemDataList.Add(shopItemData);
		}
		CloseDatabase (sqliteDB, sqliteQuery);
		return shopItemDataList;
	}

	public int GetPitLevel(){
		int pitLevel = Random.Range(1,6);
		return pitLevel;
	}
}
