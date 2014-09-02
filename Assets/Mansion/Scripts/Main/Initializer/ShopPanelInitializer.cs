using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShopPanelInitializer : MonoBehaviour {

	public GameObject shopItemButtonPrefab;
	public GameObject spaceSprite;
	public UIGrid grid;
	public UILabel secomCountLabel;
	
	// Use this for initialization
	void OnEnable () {
		List<ShopItemData> shopItemDataList = ShopItemDataDao.Instance.GetShopItemDataList ();
		List<Transform> childList = grid.GetChildList ();
		if (childList.Count == 0) {
			CreateShopItemCells (shopItemDataList);
		} else {
			InitShopItemCells (childList, shopItemDataList);
		}
		SecomData secomData = PrefsManager.Instance.GetSecomData ();
		secomCountLabel.text = "×" + secomData.Count;
	}

	private void CreateShopItemCells (List<ShopItemData> shopItemDataList) {
		for (int i = 0; i<shopItemDataList.Count; i++) {
			GameObject shopButton = Instantiate (shopItemButtonPrefab) as GameObject;
			shopButton.name = "shopButton" + i;
			grid.AddChild (shopButton.transform);
			shopButton.transform.localScale = new Vector3 (1, 1, 1);
			ShopItemData shopItemData = shopItemDataList [i];
			shopButton.BroadcastMessage ("Init", shopItemData);
		}
		List<Transform> childList = grid.GetChildList ();
		int count = childList.Count;
		Transform finalChild = childList [count - 1];
		spaceSprite.transform.localPosition = new Vector3 (0, finalChild.localPosition.y - 350.0f, 0);
	}

	private void InitShopItemCells (List<Transform> childList, List<ShopItemData> shopItemDataList) {
		for (int i = 0; i<shopItemDataList.Count; i++) {
			GameObject child = childList [i].gameObject;
			ShopItemData shopItemData = shopItemDataList [i];
			child.BroadcastMessage ("Init", shopItemData);
		}
	}
}
