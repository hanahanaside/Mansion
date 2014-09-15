using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShopPanelInitializer : MonoBehaviour {
	public GameObject shopItemButtonPrefab;
	public UIGrid shopItemGrid;
	public UIGrid stageItemGrid;
	public UILabel secomCountLabel;
	// Use this for initialization
	void OnEnable () {
		List<ShopItemData> shopItemDataList = ShopItemDataDao.Instance.GetShopItemDataList ();
		List<Transform> shopItemChildList = shopItemGrid.GetChildList ();
		if (shopItemChildList.Count == 0) {
			CreateShopItemCells (shopItemDataList);
		} else {
			InitShopItemCells (shopItemChildList, shopItemDataList);
		}
		SecomData secomData = PrefsManager.Instance.GetSecomData ();
		secomCountLabel.text = "×" + secomData.Count;
	}

	private void CreateShopItemCells (List<ShopItemData> shopItemDataList) {
		for (int i = 0; i < 5; i++) {
			GameObject shopButton = Instantiate (shopItemButtonPrefab) as GameObject;
			shopButton.name = "shopButton" + i;
			stageItemGrid.AddChild (shopButton.transform);
			shopButton.transform.localScale = new Vector3 (1, 1, 1);
			ShopItemData shopItemData = shopItemDataList [i];
			shopButton.BroadcastMessage ("Init", shopItemData);
		}
		for (int i = 5; i < shopItemDataList.Count; i++) {
			GameObject shopButton = Instantiate (shopItemButtonPrefab) as GameObject;
			shopButton.name = "shopButton" + i;
			shopItemGrid.AddChild (shopButton.transform);
			shopButton.transform.localScale = new Vector3 (1, 1, 1);
			ShopItemData shopItemData = shopItemDataList [i];
			shopButton.BroadcastMessage ("Init", shopItemData);
		}
		List<Transform> childList = shopItemGrid.GetChildList ();
		int count = childList.Count;
		Transform finalChild = childList [count - 1];
	}

	private void InitShopItemCells (List<Transform> shopItemChildList, List<ShopItemData> shopItemDataList) {
		List<Transform> stageItemChildList = stageItemGrid.GetChildList ();
		for (int i = 0; i < 5; i++) {
			GameObject child = stageItemChildList [i].gameObject;
			ShopItemData shopItemData = shopItemDataList [i];
			child.BroadcastMessage ("Init", shopItemData);
		}
		for (int i = 5; i < shopItemDataList.Count; i++) {
			GameObject child = shopItemChildList [i - 5].gameObject;
			ShopItemData shopItemData = shopItemDataList [i];
			child.BroadcastMessage ("Init", shopItemData);
		}
	}
}
