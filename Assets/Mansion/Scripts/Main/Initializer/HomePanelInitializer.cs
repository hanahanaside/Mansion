using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HomePanelInitializer : MonoBehaviour {
	public UIGrid grid;
	public UIScrollView scrollView;
	public GameObject pitPrefab;
	public UICenterOnChild center;

	void OnEnable () {
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
//				Debug.Log ("transform = " + child.localPosition);
//				float y = -child.localPosition.y;
//				if(y > 2700){
//					y = 2700;
//				}
//				Debug.Log ("y = " + y);
//				scrollView.transform.localPosition = new Vector3 (0,y,0);
				center.CenterOn (child);
				Debug.Log ("ddddddddddddddd");
			}
		}
	}
}
