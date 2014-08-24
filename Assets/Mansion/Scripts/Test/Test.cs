using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		List<ShopItemData> shopItemDataList = ShopItemDataDao.Instance.GetShopItemDataList();
		Debug.Log("id = " + shopItemDataList[0].Id);
		Debug.Log(shopItemDataList[0].Name);
	}
	
}
