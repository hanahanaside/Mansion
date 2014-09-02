using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HomePanelInitializer : MonoBehaviour {
	public UIGrid grid;
	public UIScrollView scrollView;
	public GameObject pitPrefab;
	public UICenterOnChild centerOnChild;

	public void Init () {
		if(centerOnChild.enabled){
			centerOnChild.enabled = false;
		}
		List<Transform> childList = grid.GetChildList ();
		List<RoomData> roomDataList = RoomDataDao.Instance.GetRoomDataList ();
		roomDataList.Reverse ();
		for (int i = 10; i >= 0; i--) {
			GameObject childObject = childList [i].gameObject;
			RoomData roomData = roomDataList [i];
			childObject.BroadcastMessage ("Init", roomData);
		}
		GameObject pitObject = childList [11].gameObject;
		ShopItemData pitData = ShopItemDataDao.Instance.GetPitData ();
		pitObject.BroadcastMessage ("Init", pitData);
		for(int i = 0; i < roomDataList.Count;i++){
			RoomData roomData = roomDataList[i];
			if(roomData.ItemCount != 0){
				Transform child = childList[i];
				centerOnChild.CenterOn (child);
				return;
			}
		}
		Transform cartonHouse = childList[10];
		centerOnChild.CenterOn (cartonHouse);
	}
}
