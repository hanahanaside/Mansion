﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatusPanelInitializer : MonoBehaviour {
	public GameObject work;
	public UIGrid gotItemGrid;
	public UIGrid historyGrid;
	public GameObject historyButtonPrefab;
	public GameObject shopItemButtonPrefab;
	private static StatusPanelInitializer sInstance;

	void OnEnable () {
		InitHistoryGrid ();
		InitGotItemGrid ();
		InitWork ();
	}

	void Awake(){
		sInstance = this;
	}

	public static StatusPanelInitializer Instance{
		get{
			return sInstance;
		}
	}

	public void InitHistoryGrid () {
		List<HistoryData> historyDataList = HistoryDataDao.Instance.GetHistoryDataList ();
		historyDataList.Reverse ();
		List<Transform> historyChildList = historyGrid.GetChildList ();
		for (int i = 0; i < 4; i++) {
			Debug.Log ("i = " + i);
			//履歴が４つに満たない場合は終了
			if (i >= historyDataList.Count) {
				break;
			}

			GameObject historyChild = null;
			if (i >= historyChildList.Count) {
				historyChild = Instantiate (historyButtonPrefab) as GameObject;
				historyGrid.AddChild (historyChild.transform);
				historyChild.transform.localScale = new Vector3 (1, 1, 1);
			} else {
				historyChild = historyChildList [i].gameObject;
			}
			HistoryData historyData = historyDataList [i];
			historyChild.BroadcastMessage ("Init", historyData);
		}
	}

	private void InitGotItemGrid () {
		List<ShopItemData> boughtItemList = ShopItemDataDao.Instance.GetBoughtItemList ();
		while (gotItemGrid.GetChildList ().Count < boughtItemList.Count) {
			GameObject shopButton = Instantiate (shopItemButtonPrefab) as GameObject;
			gotItemGrid.AddChild (shopButton.transform);
			shopButton.transform.localScale = new Vector3 (1, 1, 1);
		}
		List<Transform> childList = gotItemGrid.GetChildList ();
		for (int i = 0; i < childList.Count; i++) {
			GameObject childObject = childList [i].gameObject;
			ShopItemData shopItemData = boughtItemList [i];
			shopItemData.UnlockLevel = ShopItemData.UNLOCK_LEVEL_STATUS;
			childObject.BroadcastMessage ("Init", shopItemData);
		}
	}

	private void InitWork () {
		List<Transform> childList = gotItemGrid.GetChildList ();
		int count = childList.Count;
		if (count == 0) {

		} else {
			Transform finalChild = childList [count - 1];
			work.transform.localPosition = new Vector3 (0, finalChild.localPosition.y - 650.0f, 0);
		}
	}
}
