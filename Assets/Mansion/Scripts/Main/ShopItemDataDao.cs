using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShopItemDataDao {

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
		List<ShopItemData> shopItemDataList = new List<ShopItemData> ();
		for (int i = 1; i<=44; i++) {
			ShopItemData shopItemData = new ShopItemData ();
			shopItemData.Id = i;
			shopItemData.Name = "shopItem_" + i;
			shopItemData.Price = i * 100;
			shopItemData.Level = ShopItemData.LEVEL_UNLOCK;
			shopItemDataList.Add (shopItemData);
			shopItemData.LockDescription = "lockDescription_" + i;
			shopItemData.UnLockDescription = "unLockDescription_" + i;
		}
		return shopItemDataList;
	}
}
