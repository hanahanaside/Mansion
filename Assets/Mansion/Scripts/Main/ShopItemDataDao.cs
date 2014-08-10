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
		for (int i = 0; i<44; i++) {
			ShopItemData shopItemData = new ShopItemData ();
			shopItemDataList.Add (shopItemData);
		}
		return shopItemDataList;
	}
}
